using ECommerceLiteEntity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.IdentityModels
{
    public class ApplicationUser:IdentityUser
    {
        //identityUser 'dan kalıtım alında. Identity user microsoft un identity şemasına ait bir classtır.
        //identity user class ı ile bize sunulan aspNetUser tablosundaki koplonları genişletmek için kalıtım aldık
        //aşağıya ihtiyacımız olan kolonları aldık
        [Required]
        [Display(Name="Ad")]
        [StringLength(maximumLength:30,MinimumLength =2,ErrorMessage ="İsminizin uzunluğu 2 ile 30 karakter aralığında olmalıdır.")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Soyad")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Soyisminizin uzunluğu 2 ile 30 karakter aralığında olmalıdır.")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Kayıt Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        //ToDo: Guid in kaç haneli olduğuna bakıp buraya string lenght ile attribute tanımlanacaktır.
        public string ActivationCode { get; set; }
        //isteyen birtdate gibi bir alan da ekleyebilir.

        public virtual List<Admin> AdminList { get; set; }
        public virtual List<Customer> CustomerList { get; set; }
        public virtual List<PassiveUser> PassiveUserList { get; set; }

        
    }
}
