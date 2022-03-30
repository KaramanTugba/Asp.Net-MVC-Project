using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager:ICategoryService
    {
        ICategoryDAL _categoryDal;

        public void CategoryAddBL(Category category)
        {
            _categoryDal.Insert(category); 
        }

        public List<Category> GetList()
        {
            return _categoryDal.List();
        }
        //GenericRepository<Category> repo = new GenericRepository<Category>();

        //public List<Category> GetAll()
        //{
        //    return repo.List();
        //}
        //public void CategoryAddBL(Category p)
        //{
        //    if (p.CategoryName == "" || p.CategoryName.Length < 3 || p.CategoryDescription == "")
        //    {
        //        //hata mesajı
        //    }
        //    else { repo.Insert(p); }
        //}
    }
}
