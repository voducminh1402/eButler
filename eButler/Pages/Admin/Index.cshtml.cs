using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eButler.Pages.Admin
{
    //[Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
