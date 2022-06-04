// using the "using statement" means that one does NOT need to fully qualify on EACH usage of the class
using OOPsReview.Data;
//this class is by default in the namespace of the project: OOPsReview 

// fully qualified data member
// OOPsReview.Data.SupervisoryLevel item = new SupervisoryLevel();

// Console is a static class
// You cannot create your own instance of a static class
// static classes are meant to be shared



// an instance class needs to be created using the new command and the class constructor
// one needs to declare a variable of class datatype: ex Employment 

//Employment myEmp = new Employment(); // default constructor

// fully qualified reference to Employment
// consists of the namespace.classname
// OOPsReview.Data.Employment myEmp1 = new Employment("Level 5 programmer", SupervisoryLevel.Supervisor, -15.9);
Employment myEmp1 = new Employment("Level 5 programmer", SupervisoryLevel.Supervisor, 15.9);
Console.WriteLine(myEmp1.ToString());
Console.WriteLine($"{myEmp1.Title},{myEmp1.Level},{myEmp1.Years}");
//Console.WriteLine(myEmp1.ToString()); // use the instance name to reference items within your class

//// invalid
//// Console.WriteLine(Employment.ToString());

myEmp1.SetEmplyeeResponsibilityLevel(SupervisoryLevel.DepartmentHead);

Console.WriteLine(myEmp1.ToString());

// SupervisoryLevel item = new SupervisoryLevel();

// testing (simulate a Unit test) 
// Arrange (setup of your test data)
Employment Job = null;

// passing a reference variable to a method 
// a class is a reference data store 
// this passes the actual memory address of the data store to the method 
// ANY changes done to the data store within the method WILL BE reflected 
// in the data store WHEN you return from the method

CreateJob(ref Job);
Console.WriteLine(Job.ToString());

// passing value arguments to a method AND receiving a value result back from the method 
// struct is a value data store 
ResidentAddress Address = CreateAddress();
Console.WriteLine(Address.ToString());

// Act (execution of the test you wish to perform)
// test that we can crate a person (Composite instance)
Person me = null; // a variable capable of holding a Person instance
me = CreatePerson(Job, Address);

// Access (check your results)
Console.WriteLine($"{me.FirstName} {me.LastName} lives at {me.Address.ToString()} having a job count of {me.NumberOfPositions}");
Console.WriteLine("\nJobs:\n");
foreach (var item in me.EmploymentPositions)
{
    Console.WriteLine(item.ToString());
}

// using Employment.Parse

string theRecord = "Boss,Owner,5.5";
Employment theParsedRecord = Employment.Parse(theRecord);
Console.WriteLine(theParsedRecord.ToString());

// using Employment .TryParse
theParsedRecord = null;
if (Employment.TryParse(theRecord, out theParsedRecord))
{
    // do whatever logic you need to do with the valid data
    Console.WriteLine(theParsedRecord.ToString());
}
// if the TryParse failed, you would be handling it via your user friendly error handling code
else
{
    Console.WriteLine("The parsing did not work");
}

void CreateJob(ref Employment job)
{
    // since the class MAY throw exceptions, you should have user friendly error handling
    try
    {
        job = new Employment(); // default constructor; new command takes a constructor as it's reference
        // because my properties have public set (mutators), I can "set" the value of the property directly from the driver program
        job.Title = "Boss";
        job.Level = SupervisoryLevel.Owner;
        job.Years = 25.5;
        // or 

        // use the greedy constructor
        // job = new Employment("Boss", SupervisoryLevel.Owner, 25.5);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    
}

ResidentAddress CreateAddress()
{

    ResidentAddress address = new ResidentAddress(10706, "106 st", "", "", "Edmonton", "AB");
    return address;
}

Person CreatePerson(Employment job, ResidentAddress address)
{
    //Person me = new Person("Don", "Welch", address, null);

    // one could add the job(s) to the instance of Person (me) after the instance is created via the behavior AddEmployment(Employment emp)

    // or 

    // one coule crate a List<T> and add to the list<T> before creating the person instance
    List<Employment> employments = new List<Employment>();
    //me.AddEmployment(job); // add an element to the List<T>
    employments.Add(job);
    Person me = new Person("Don", "Welch", address, employments); // using the greedy constructor 

    // create additonal jobs and load to Person 
    Employment employment = new Employment("New Hire", SupervisoryLevel.Entry, 0.5);
    me.AddEmployment(employment);
    employment = new Employment("Team Head", SupervisoryLevel.TeamLeader, 5.2);
    me.AddEmployment(employment);
    employment = new Employment("Department IT head", SupervisoryLevel.DepartmentHead, 6.8);
    me.AddEmployment(employment);

    return me;
}