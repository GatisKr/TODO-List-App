using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}
