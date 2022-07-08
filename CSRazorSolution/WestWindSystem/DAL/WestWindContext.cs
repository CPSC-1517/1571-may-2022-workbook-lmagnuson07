using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Aditional Namespaces
using Microsoft.EntityFrameworkCore;
using WestWindSystem.Entities;
#endregion
namespace WestWindSystem.DAL
{
    // leave the access type for this class as internal
    // "internal " access to this class is ONLY possible from within this project.
    // Why, it adds a layer of security to the web application 
    // DBContext is the entityframework software that talks to the database.
    // we inherit the required class 
    internal class WestWindContext : DbContext
    {
        // the constructor will pass the context connection to the DbContext parent for use in setting up the database connection 
        public WestWindContext(DbContextOptions<WestWindContext> options) : base(options)
        {

        }

        // setup properties in this class that can be referenced by other code within your class library.
        // The properties represent a collection of instances of the entity retrieved from or sent to the database 
        // One property per entity in Entities
        public DbSet<BuildVersion> BuildVersions { get; set; } 
        //public DbSet<Employee> Employees { get; set; }
    }
}
