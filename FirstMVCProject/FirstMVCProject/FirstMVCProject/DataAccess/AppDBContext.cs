using FirstMVCProject.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace FirstMVCProject.DataAccess
{
    public class AppDBContext : DbContext
    {
        // Your context has been configured to use a 'AppDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FirstMVCProject.DataAccess.AppDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AppDBContext' 
        // connection string in the application configuration file.
        public AppDBContext()
            : base("name=AppDBContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Blog> Blogs { get; set; }
    }

}