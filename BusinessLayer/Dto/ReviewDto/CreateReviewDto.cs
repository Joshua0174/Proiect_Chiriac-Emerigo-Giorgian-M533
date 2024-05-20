using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.ReviewDto
{
    public class CreateReviewDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(25, ErrorMessage = "Title cannot be over 25 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Content must be 10 characters")]
        [MaxLength(250, ErrorMessage = "Content cannot be over 250 characters")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Range(0, 5, ErrorMessage = "Number must be between 0 and 5")]
        public int Rating { get; set; }

    }
}
