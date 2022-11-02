using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Repostiories;

namespace eButler.Pages.Admin.Users
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public DeleteModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                User = userRepository.GetUserByIdWithRole(id);

                if (User == null)
                {
                    return NotFound();
                }
                return Page();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                userRepository.DeleteUser(id);

            return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
