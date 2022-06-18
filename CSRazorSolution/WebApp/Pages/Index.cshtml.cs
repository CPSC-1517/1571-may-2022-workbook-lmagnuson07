using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    // this web page Model class inherits from PageModel
    public class IndexModel : PageModel
    {
        // This default page uses a system class called ILogger<T>
        // This is composition
        // This is a local field 
        private readonly ILogger<IndexModel> _logger;

        // Constructor
        // this constructor receives an injection of a service
        // This injection is referred to as Injection Dependencies 
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // This is a local property 
        public string MyName { get; set; }

        // this is a class Behaviour (method)
        // This method, OnGet(), executes for any Get request
        // this method will be the first method executed when the page is first used 
        // Excellent "event" to use to do any initialization to your web page display

        public void OnGet()
        {
            // Once in the request method, you are in control of what is being processed on the web page for the current request 
            // The code within this method is the work that I WISH to be done 
            Random rnd = new Random();
            int value = rnd.Next(0, 100); // 100 is not included
            if (value % 2 == 0)
            {
                MyName = $"Don ({value}) welcome to the wide wild world of Razor pages";
            }
            else
            {
                MyName = null;
            }

            // control is returned to the web server 
        }
    }
}