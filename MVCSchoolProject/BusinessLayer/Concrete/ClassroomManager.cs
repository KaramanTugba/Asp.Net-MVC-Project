using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
   public class ClassroomManager
    {
        GenericRepository<Classroom> repo = new GenericRepository<Classroom>();
        public List<Classroom> GetAll()
        {
            return repo.List();
        }
        public void  ClassroomAddBL(Classroom p)
        {
            if (p.ClassroomName == "" || p.ClassroomName.Length < 3 )
            {
                //hata mesajı
            }
            else { repo.Insert(p); }
        }

    }
}
