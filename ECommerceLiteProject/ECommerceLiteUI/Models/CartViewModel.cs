using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerceLiteUI.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public DateTime RegisterDate { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Ürün adı 2 ile 100 karakter aralığında olmalıdır!")]
        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Ürün açıklaması en fazla 500 karakter olmalıdır!")]
        [Display(Name = "Ürün Açıklaması")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Ürün Kodu")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "Ürün kodu en fazla 8 karakter olmalıdır!")]
        
        public string ProductCode { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public int CategoryId { get; set; }
    }
}