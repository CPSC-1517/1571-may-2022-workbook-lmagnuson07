﻿using System;
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
        private string _Title;
        private double _Years;

        // Properties - these are access techniques to retrieve or set data in your class without directly touching the storage data field.

        // A property is associated with a single instance of data
        // A property is public so it can be accessed by an oustide user of the class
        // A property MUST have a get, and MAY have a set. 
        // If no set, the property is not changable by the user; readonly - Commonly used for calculated data of the class
        // The set can be either public or private
        // Public: the user can alter the contents of the data.
        // Private: only code within the class can alter the contents of the data.

        // Fully implemented property
        // A) A declared storage area (data field) 
        // B) A declared property signature (access rdt proprtyname)
        // C) A coded accessor (get) coding block : public (no definition of access in front of it)
        // D) An optional coded mutator (set) coding block : public or private
        //      if the set is private the only way to place data into this property is via the constructor or       a behavior 
        // When:
        //  a) If you are storing the associate data in an explicity declare data field
        //  b) If you are doing validation on incoming data
        //  c) Creating a property that generates output from other data sources within the class (readonly property); this property would ONLY have a accessor (get)
        public string Title
        {
            // a property is associated with a single piece of data
            get
            {
                // accessor
                // the get "coding block" will return the contents of a data field(s)
                // the return has syntax of "return expression"
                return _Title;
            }
            set
            {
                // mutator 
                // the set "coding block" recieves an incoming value and places it into the associated data field 
                // during the setting, you might wish to validate the incoming data 
                // during the setting, you might wish to do some type of logical processing using the data to set another field
                // the incoming piece of data is referred to using the keyord "value"

                // ensure that the incoming data is not null or empty or just whitespace
                // IsNullOrEmpty only uses the first two of the list above, IsNullOrWhiteSpace does all 3
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title is a required piece of data");
                }
                // data is considered valid
                _Title = value;
            }
        }
        // auto inplemented property

        // these properties differ only in syntax, each property is responsible for a single piece of data
        // these properties do NOT reference a declared private data member
        // the system generates an internal storage area of the return data type 
        // the system manages the internal storage for the accessor and mutator 
        // THERE IS NO ADDITIONAL LOGIC APPLIED TO THE DATA VALUE

        // using an enum for this field will automatically restrict the data values this property can contain

        // syntax access rdt (return data type) propertyname {get; [private] set;}

        public SupervisoryLevel Level { get; set; }

        public double Years
        {
            get { return _Years; }
            set 
            { 
                /*if(value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Years of {value} is invalid. Must be 0 or greater");
                }*/
                if (!Utilities.IsZeropositive(value))
                {
                    throw new ArgumentOutOfRangeException($"Years of {value} is invalid. Must be 0 or greater");
                }
                _Years = value;
            }
        }

        // constructor

        // this is used to initialize the physical object (instance) during its creation
        // the result of creation is to ensure that the coder gets an instance in a "known state"
        // if your class definition has no constructor coded, then the data members and/or auto implemented properties are set to the C# default data type value
        // you can code one or more constructors in your class definition
        // IF YOU CODE A CONSTRUCTOR FOR THE CLASS, YOU ARE RESPONSIBLE FOR ALL CONSTRUCTORS USED BY THE CLASS
        // generally, if you are going to code your own constructor(s) you code two types
        // Default: this constructor does NOT take in any parameters
        //          this constructor mimics the default system constructor
        // Greedy:  this constructor has a list of parameters, one for each property, declared for incoming data
        // (), (a),(b),(c),(d),(a,b),(a,c),(a,d)... 2 raise power of 4 = 16 constructors
        // (), (a,b,c,d)

        // syntax: accesstype classname([list of parameters]) {constructor code block}
        // IMPORTANT: The constructor DOES NOT have a return data type
        //            You DO NOT call a constructor directly, it is called using the new keyword 
        public Employment()
        {
            // constructor body
            // a) empty: values will set to C# defaults
            // b) you COULD assign literal values to your properties with this constructor

            // the values the you give your class data members/properties CAN be assigned directly to a data member
            // however: if you have validated properties, you SHOULD consider saving your data values via the property

            // You CAN code you validation logic within your constructors BECAUSE objects run your constructor when it is created.
            // Placing your logic in the constructor could be done if your property has a private set OR if your public data member is a readonly data member
            // Private sets and readonly datamembers CANNOT have their data altered directly 

            Level = SupervisoryLevel.TeamMember;
            Title = "Unknown";
        }
        // Greedy Constructor
        public Employment(string title, SupervisoryLevel level, double years = 0.0)
        {
            // Constructor body 
            // a) a parameter for each property 
            // b) You COULD have your validation in this constructor 
            // c) validation for public readonly data members MUST be doner here 
            // d) validation for properties with a private set CAN be done here if not done in the property

            // default parameters

            // WHY? it allows the programmer to use your constructor/method without having to specify all arguments in the call to your constructor/method 

            // location: end of parameter list
            // How many: As many as you wish
            // Values for your default parameters must be a valid value
            // position and order of spceified default parameters are important when the programmer uses the constructor/method.
            // Default parameters CAN be skipped, HOWEVER, you still must account for the skipped parameter in your argument call list using commas 
            // by giving the default parameter an argument value on the call, the constructor/method default value is overridden

            // Syntax: datatype parametername = default value 
            // Example: years on this constructor is a default parameter

            // Example: skipped defaults (3 default parameters, second one skipped.
            //      ...(string requiredparam, int requiredparam, int default = 0, int default2 = 0, int default3 = 1)
            // call: ...("required string", 25, 10, , 5) default2 was skipped 
            Title = title;
            Level = level;
            Years = years; // eventually the data will be placed in _Years
        }

        // Behaviours (aka methods) 
        // a behaviour is any method in your class
        // behaviours can be private (for use by the class only); public (for use by the outside user)
        // all rules about methods are in effect 

        // a special method may be placed may be placed in your class to reflect the data stored by the instance (object) based on this class definiton
        // this method is part of the system software and can be overriden by your own version of the method

        public override string ToString()
        {
            // this string is known as a "comma separated values (csv)" string
            // this string uses the get; of the property
            return $"{Title},{Level},{Years}";
        }

        public void SetEmplyeeResponsibilityLevel(SupervisoryLevel level)
        {
            // this method, in this example would not be necessary as the access directly to Level (property) is public (set; )
            // HOWEVER: If the Level property had a private mutator, the outside user would NOT have direct access to changing the property
            // THEREFORE: a method (besides the constructor) would need to be supplied to allow the outside user the ability to alter the property value (if they wanted)
            
            // this assignment uses the set; of the property
            Level = level; 
        }
    }
}