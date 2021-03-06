// using the "using statement" means that one does NOT need to fully qualify on EACH usage of the class
using OOPsReview.Data;
using System.Text.Json;
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

// File I/O
// Writing a comma separated value file
//string pathname = WriteCSVFile();

// Read a comma separated value file

// we wil be using ReadAllLines(pathname); returns an array of strings. Each array element is one line in your csv file
const string PATHNAME = "../../../Employment.csv";
const string JSONPATHNAME = "../../../Employment.json";
List<Employment> jobs = ReadCSVFile(PATHNAME);
Console.WriteLine($"\nResults of reading the CSV file at : {PATHNAME}");
foreach (var job in jobs)
{
    Console.WriteLine($"Title: {job.Title} Level: {job.Level} Year: {job.Years}");
}

Console.WriteLine(jobs.Count);
// Writing a a JSON file
// me is built above 
SaveAsJson(me, JSONPATHNAME);

// Read a JSON file
Person jsonMe = ReadAsJson(JSONPATHNAME);
Console.WriteLine("\nOutput from reading a json file");

Console.WriteLine($"{jsonMe.FirstName} {jsonMe.LastName} lives at {jsonMe.Address.ToString()} having a job count of {jsonMe.NumberOfPositions}");
Console.WriteLine($"Jobs: output via for loop\n");
foreach (var item in jsonMe.EmploymentPositions)
{
    if(item.Years > 0)
    {
        Console.WriteLine(item.ToString());
    }
}
Console.WriteLine("\nJobs: output via for loop\n");

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

List<Employment> ReadCSVFile(string pathname)
{
    List<Employment> jobs = new();
    // this try/catch error handling is for system I/O errors while reading the file
    try
    {
        //List<string> fileLines = new();
        // use this variable repeatedly to hold a new instance of Employment as I read and Parse the CSV file 
        Employment job = null;
        // read the CSV file using File.ReadAllLines(), this no need to create a StreamReader. ReadAllLines returns an array of strings, each string representing one line in your CSV file
        foreach (string line in File.ReadAllLines(pathname))
        {
            // as you process each line, we will use the TryParse of Employment. 
            // this will return an instance of Employment IF the data is valid 
            // If the data is NOT valid, Employment will throw various errors
            // we DO NOT want to stop processing the strings IF an error is discovered
            // THEREFORE we WILL have a try/catch WITHIN this loop 
            try
            {
                // attempt to convert a comma separated value string into an instance of Employment (parse the data)
                if (Employment.TryParse(line, out job))
                {
                    jobs.Add(job);
                }
                // testing if the parsing was successful, the way this logic is set up, the testing is unnecessary. If the parse fails, an error would have been trown, thus processing will have jumped to a catch in the tryParse
                // Consider that on a successful parse you may have additional (complex) logic to perform. 
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format error: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Argument error: {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Out of range error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown exception error: {ex.Message}");
            }
        }
    }
    catch(IOException ex)
    {
        Console.WriteLine($"Reading CSV file error: {ex.Message}");
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return jobs;
}
string WriteCSVFile()
{
    string pathname = "";
    try
    {
        List<Employment> jobs = new List<Employment>();
        jobs.Add(new Employment("trainee", SupervisoryLevel.Entry, 0.5));
        jobs.Add(new Employment("worker", SupervisoryLevel.TeamMember, 3.5));
        jobs.Add(new Employment("lead", SupervisoryLevel.TeamLeader, 7.4));
        jobs.Add(new Employment("dh new projects", SupervisoryLevel.DepartmentHead, 1.0));

        // create a list of comma separated value strings
        // the contents of each string will be 3 values of Employment
        //List<string> csvlines = new List<string>(); // better way to write in .Net core
        List<string> csvlines = new();

        // place all the instances of Emplyment in the collection of jobs in the csvlines using .ToString() of the Employment class
        foreach (var job in jobs)
        {
            csvlines.Add(job.ToString());
        }

        // write to a text file the csv lines
        // each line represents a Employment instance
        // your could use StreamWriter 
        // However, within the File class there is a method that ouputs a list of strings all within ONE command. There is no need for a StreamWriter instance
        // the pathname is the minimum for the command 
        // the file by default will be created in the same folder as your .exe file
        // you CAN alter the path name using relative addressing 
        
        pathname = "../../../Employment.csv";
        File.WriteAllLines(pathname, csvlines);
        Console.WriteLine($"\nCheck out the CSV file at: {Path.GetFullPath(pathname)}");
    }
    catch (Exception ex) 
    {
        Console.WriteLine(ex.Message);
    }
    return Path.GetFullPath(pathname);
}

void SaveAsJson(Person me, string pathname)
{
    // the term used to read and write Json files is Serialization
    // the classes use are referred to as serializers 
    // with writing we can make the file produce more readable format by using indentaition
    // Json is very good at using objects and properties, however, it needs help/prompting to work better with fields: option IncludeFields
    // the term Serialize is generally used to indicate writing 
    // instance instantiation 
    JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        IncludeFields = true
    };
    try
    {
        // Serialize the instance of Person 
        // Produce a string of serialized data 
        string jsonstring = JsonSerializer.Serialize<Person>(me, options);
        // output the json string to your file indicated in the path
        // use WriteAllText here becuase there is ONLY a SINGLE line of text unlike writing a csv file which has an array of strings (WriteAllLines)
        File.WriteAllText(pathname, jsonstring);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Person ReadAsJson(string pathname)
{
    Person person = null;
    try
    {
        // bring in the text from the file 
        string jsonstring = File.ReadAllText(pathname);
        // use the deserializer to unpack the json string into the expected structure <Person> 
        person = JsonSerializer.Deserialize<Person>(jsonstring);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return person;
}