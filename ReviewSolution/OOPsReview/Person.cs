using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Text.Json.Serialization;
#endregion

namespace OOPsReview.Data
{
    public class Person
    {
        // example of a composite class
        // a composite class uses other classes/structs in its definition
        // a composite class is recognized with the phrase "has a" class
        // this class of person "has a" resident address
        // this class has a List<T> where <T> represents a datatype. In this class <T> is a collection of Employment instances

        // review video on inheritance and compsition 
        // this class will define the following characteristics of a person
        // First Name, Last Name, the current resident address, list of Employment positions

        private string _FirstName;
        private string _LastName;

        public string FirstName
        {
            get { return _FirstName; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("First name is required");
                }
                _FirstName = value; 
            }
        }
        public string LastName
        {
            get { return _LastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Last name is required");
                }
                _LastName = value;
            }
        }
        // composition actually uses the other struct/class as a property/field within the definition of the class being specified (created)
        // in this example, Address is a field (data member) 

        // this field is NOT a property
        // the data type is a developer defined datatype (struct)

        // JSON 
        // JSON Serialization has no problem in creating the named pairs for this field, due to the option IncludeFields on the write/aread calls 
        // however, the deserializer does have a problem 
        // solution: use an annotation to indicate that the field is included for use by JSON 
        // to use this annotation you will need to add a namespace (see above) in resolving the conflict
        [JsonInclude]
        public ResidentAddress Address;

        public List<Employment> EmploymentPositions { get; private set; }

        // this property will compile cleanly 
        // this property will return a value IF EmploymentPositions has an instance of List<t>
        // this property WILL ABORT IF EmploymentPositions has NOT been set to an instance of List<t> 
        public int NumberOfPositions { get { return EmploymentPositions.Count; } }

        //public Person()
        //{
        //    // the system will automatically assigne default system values to our datamembers accordingly to there datatypes 
        //    // strings -> null
        //    // objects -> null
        //    // firstname and lastname has validation voiding a null value 
        //    FirstName = "Unknown";
        //    LastName = "Unknown";
        //    // if one tried to reference an instance's data and the instance is null, then one would get a null exception
        //    // Even though you have no instances to store, you will at least have some place to put data ONCE it is supplied
        //    EmploymentPositions = new List<Employment>();
        //}

        // option 2 
        // DO not code a "Default" constructor 
        // Code only the "Greedy" constructor 
        // If only a greedy constructor exists for the class, the ONLY way to possibly create an instance for the class within the program would be to use the constuctor when the class instance is created 

        // for JSON deserialization requires proper matching constructor parameter names 
        // your constructor parameter names MUST match your property variable names 
        // the parameter names are NOT case sensitive
        // the order of the parameter on the constructor does not affect the JSON reading (deserialization)
        public Person(string firstname, string lastname, ResidentAddress address, List<Employment> employmentpositions)
        {
            FirstName = firstname;
            LastName = lastname;
            Address = address;
            if (employmentpositions != null)
                EmploymentPositions = employmentpositions;
            else
                // allow a null parameter value and the class to have an empty List<T>
                EmploymentPositions = new List<Employment>();
        }
        public void ChangeName(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
        public void AddEmployment(Employment employment)
        {
            if (employment == null)
            {
                throw new ArgumentNullException("You must supply an employment record for it to be added to this person");
            }
            EmploymentPositions.Add(employment);    
        }
    }
}
