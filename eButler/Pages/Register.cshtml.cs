using BusinessLogic.Models;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Xml.Linq;

namespace eButler.Pages
{
    public class RegisterModel : PageModel
    {
        public IUserRepository userRepository;
        [BindProperty]
        public User User { get; set; }
        public RegisterModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            try
            {
                userRepository.AddNewUser(User);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "User name is already exist!";
                return Page();
            }           
        }
    }
}
