using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview.Data
{
    public class Employment
    {
        // An instance of this class will hold data about a person's employment 
        // The code of this class is the definition of that data (employment data)
        // The characteristics (data) of the class:
        // Title, SupervisoryLevel, Years of Experience.                                                    
        // There are 4 components of a class definition 
        // Data fields (data members), behaviors (methods), constructors, properties

        // Data fields - Storage areas in your class, treated as variables (may be pubic, private, public readonly)

        // Properties - these are access techniques to retrieve or set data in your class without directly touching the storage data field.

        // A property is associated with a single instance of data
        // A property is public so it can be accessed by an oustide user of the class
        // A property MUST have a get, and MAY have a set. 
        // If no set, the property is not changable by the user; readonly - Commonly used for calculated data of the class
        // The set can be either public or private
        // Public: the user can alter the contents of the data.
        // Private: only code within the class can alter the contents of the data.
        public string Title 
        {
            get; 
            set;
        } 
    }
}
