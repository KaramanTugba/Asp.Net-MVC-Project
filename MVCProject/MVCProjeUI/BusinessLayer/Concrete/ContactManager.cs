using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContactManager
    {
        GenericRepository<Contact> repo = new GenericRepository<Contact>();

        public List<Contact> GetAll()
        {
            return repo.List();
        }
        public void ContactAddBL(Contact p)
        {
            if (p.UserName== "" || p.UserName.Length<3|| p.Subject==""||p.Message=="")
            {
                //hata mesajı
            }
            else { repo.Insert(p); }
        }
    }
}
