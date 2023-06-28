using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ProductDetailApplication.Models
{
    public partial class ProductDetails
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public long Price { get; set; }
        public int Stock { get; set; }
    }
}
