using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Repostiories;
using System.Globalization;

namespace eButler.Pages.Admin.Users
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public EditModel(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUserByIdWithRole(id);
            TempData["password"] = User.Password;

            if (User == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(roleRepository.GetRoles(), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string password)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                TempData["password"] = password;
                // _context.Attach(User).State = EntityState.Modified;
                User.Password = password;
                userRepository.UpdateUser(User);

                return RedirectToPage("./Index");
            } catch(Exception ex)
            {
                return NotFound();
            }
           
        }

       
    }
}
