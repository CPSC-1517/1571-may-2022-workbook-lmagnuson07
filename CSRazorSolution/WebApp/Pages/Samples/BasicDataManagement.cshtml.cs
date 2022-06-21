using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Samples
{
    public class BasicDataManagementModel : PageModel
    {
        // BindProperty is an annotation that handles two-way data transfer
        // two-way? means output to form for display AND input from form for processing 
        // Handles the output here 
        [BindProperty]
        public int Num { get; set; }
        [BindProperty]
        public string MassText { get; set; }
        public string Feedback { get; set; }
        public void OnGet()
        {
            if (Num == 0)
            {
                Feedback = "executing the OnGet method for the Get request";
            }
            else
            {
                Feedback = $"You entered the value {Num} displayed by OnGet";
            }
        }
        public void OnPost()
        {
            Feedback = $"You entered the value {Num} displayed by OnPost\n" + 
                $"your mass text is {MassText}";
        }
    }
}
