using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("Carts")]
    public class Cart
    { 
        public Guid ProductId { get; set; }
        public string AppUserId {  get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
