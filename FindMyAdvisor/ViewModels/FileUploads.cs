using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.ViewModels
{
    public class FileUploads
    {
        [Required, FileExtensions(Extensions = "csv", ErrorMessage ="Specify a CSV file. (Comma-separated values)")]
        public HttpPostedFileBase File { get; set; }
    }
}