using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("Comments")]
    public class Review : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int Rating { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser{ get; set; }
    }
}
