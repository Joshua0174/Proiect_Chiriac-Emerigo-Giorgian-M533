using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("Stocks")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Symbol {  get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
        
        public List<Cart> Carts { get; set; } = new List<Cart>();
    }
}
