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
    public class IndexModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public IndexModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = userRepository.GetUsersWithRole().ToList();  
        }
    }
}
