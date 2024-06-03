using Microsoft.AspNetCore.Mvc;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.ViewModels.Vehicles;

namespace XodoApp.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IDealershipService _dealershipService;

        public VehicleController(IVehicleService vehicleService, IDealershipService dealershipService)
        {
            _vehicleService = vehicleService;
            _dealershipService = dealershipService;
        }
        public async Task<IActionResult> Index()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var vm = await _vehicleService.GetAllViewModelWithImages();

            return View(vm);
        }
        public async Task<IActionResult> Create()
        {
            SaveVehicleViewModel vm = new();           
            vm.Dealerships = await _dealershipService.GetAllViewModel();
            return View("Create", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveVehicleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", vm);
            }
            SaveVehicleViewModel vehicleVm = await _vehicleService.Add(vm);

            if (vehicleVm.Id != 0 && vehicleVm != null)
            {
                foreach (var file in vm.Files)
                {
                    string imageUrl = UploadFile(file, vehicleVm.Id);
                    SaveVehicleImageViewModel imageVm = new();

                    imageVm.VehicleId = vehicleVm.Id;
                    imageVm.ImageUrl = imageUrl;
                    await _vehicleService.AddImage(imageVm);
                }
            }
            return RedirectToRoute(new { controller = "Vehicle", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {

            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Vehicles/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }

    }
}
