using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters in length")]
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
