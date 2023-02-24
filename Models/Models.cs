using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class Models
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brands ModelsBrand { get; set; }
    }
}
