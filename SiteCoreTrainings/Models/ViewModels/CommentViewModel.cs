using System.ComponentModel.DataAnnotations;

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