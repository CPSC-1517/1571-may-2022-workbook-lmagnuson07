using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region AAdditional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace WestWindSystem.Entities
{
    // we need to point this entity definition to the sql table that it represents. To do this we use an annotation. 
    // annotations are placed immediately before the item in the definition it refers to. 
    [Table("BuildVersion")]
    public class BuildVersion
    {
        // in sql, nvarchar(), varchar(), nchar(), char is declared as a string in your entity definition 
        // sql bit is a bool in C#

        // names of class properties DO NOT need to match the attribute names on your sql table (entity)
        // HOWEVER, if you use different names then ORDER is IMPORTANT in this entity class. It must match the order in the SQL table. 
        // If your property names match then the order within this entity class is not important. However, it is much easier to match your sql table to your entity if you maintain the same order for data.

        // annotation to indicate the primary key / property relationship
        // 3 syntax forms for the Pkey annotation 

        // IDENTITY() pkey in sql
        // a) [Key] the string is optional here

        // Your sql pkey is NOT an IDENTITY() pkey in sql 
        // b) [Key]
        //    [DatabaseGeneratedOption(DatabaseGeneratedOption.None)]

        // Your sql pkey is a compound pkey in sql 
        // You will need to add the annotaion in front of each part of the compound key attribute / property to form the correct pkey structure 
        // c) [Key]
        //    [Column(Order=n)]

        // https://www.entityframeworktutorial.net/code-first/dataannotation-in-code-first.aspx

        // If you have a foreign key and your attribute / property names are the same the system will already know about the Fkey relationship . You do not use the annotation [ForeignKey]
        // Compound key
        //[Key]
        //[Column(Order = 1)]
        //public int Id { get; set; }
        //[Key]
        //[Column(Order = 2)]
        //public int Major { get; set; }
        [Key]
        public int Id { get; set; }
        public int Major { get; set; }

        public int Minor { get; set; }
        public int Build { get; set; }
        public DateTime ReleaseDate { get; set; }
        // You can create a property within your entity that is not a data attribute in your sql table. 
        // If you do, use the [NotMapped] annotation 

        // Example 
        // assume you have 2 properties, FirstName and LastName.
        // You wish to create a string of FullName. 
        // You do not wish to force the programmer to constantly concatenate the properties in their code
        // You wish to make it easier for the programmer by doing the concatenation for them. 
        
        // Create a read-only property (just a get) and return the concatenation 
        // The programmer can then use your read-only property 

        // The system realizes that this is NOT a database field. 

        //[NotMapped] 
        // public string FullName { get {return LastName + ", " + FirstName; } } 
    }
}
