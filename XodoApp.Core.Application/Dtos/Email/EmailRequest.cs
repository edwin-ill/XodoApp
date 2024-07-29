using System;
using System.Collections.Generic;
using System.Text;

namespace XodoApp.Core.Application.Dtos.Email
{
    public class EmailRequest
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? From { get; set; }
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
        public string? Message { get; set; }
        public string? Car { get; set; }
    }

}
