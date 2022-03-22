using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;


namespace DataAccessLayer.Concrete
{
    public class Context:DbContext
    {
        public DbSet<Classroom> Classrooms { get; set; } 
        public DbSet<Lesson> Lessons { get; set; } 
        public DbSet<Student> Students { get; set; } 
        public DbSet<Teacher> Teachers { get; set; } 
    }
}
