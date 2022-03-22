using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [StringLength(20)]
        public string TeacherName { get; set; }
        [StringLength(30)]
        public string TeacherSurname { get; set; }
        [StringLength(11)]
        public string TeacherPhone { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
