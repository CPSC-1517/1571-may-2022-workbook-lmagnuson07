using OOPsReview.Data;

// fully qualified data member
// OOPsReview.Data.SupervisoryLevel item = new SupervisoryLevel();

// Console is a static class
// You cannot create your own instance of a static class
// static classes are meant to be shared

Console.WriteLine("Hello World!");

// an instance class needs to be created using the new command and the class constructor
// one needs to declare a variable of datatype Employment 

Employment myEmp = new Employment(); // default constructor
Employment myEmp1 = new Employment("Level 5 programmer", SupervisoryLevel.Supervisor, -15.9);
Console.WriteLine(myEmp1.ToString()); // use the instance name to reference items within your class

// invalid
// Console.WriteLine(Employment.ToString());

myEmp.SetEmplyeeResponsibilityLevel(SupervisoryLevel.DepartmentHead);

Console.WriteLine(myEmp1.ToString());

SupervisoryLevel item = new SupervisoryLevel();