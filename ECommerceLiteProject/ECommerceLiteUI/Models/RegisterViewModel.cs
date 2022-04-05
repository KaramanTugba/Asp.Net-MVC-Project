using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceLiteUI.Models
{
    public class RegisterViewModel
    {
        //kayıt modeli içinde siteye kayıt olmak isteyen kişilerden hangi bilgileri alacağımızı belirleyeceğiz
        //tc, isim soyisim, email - eğer yazdıysak tel cinsiyet - alanlarını tanımlayalımü,

        //not: data annotation ları kullanarak validation kurallarını belirlediğimiz için kapsüllemeye gerek kalmadı.
        [Required]
        [StringLength(11,MinimumLength =11,ErrorMessage ="Tc Kimlik numarası 11 haneli olmalıdır.")]
        [Display(Name="Tc Kimlik")]
        public string TCNumber { get; set; }

        [Required]
        [StringLength(maximumLength:30, MinimumLength = 2, ErrorMessage = "İsiminiz 2 ile 30 karakter arasında olmalıdır.")]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Soyadınız 2 ile 30 karakter arasında olmalıdır.")]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z]\w{4,14}$", ErrorMessage = @"	
                            The password's first character must be a letter, it must contain at least 5 
                                characters and no more than 15 characters and no characters other than 
                                letters, numbers and the underscore may be used")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}