using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteCoreTrainings.Models.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public string CommentAuthor { get; set; }

        [Required]
        public string CommentText { get; set; }
    }
}