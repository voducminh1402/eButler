using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Models;
using DataAccess.Repostiories;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace eButler.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public HouseKeeper HouseKeeper { get; set; }
        [BindProperty]
        public User Users { get; set; }
        //[BindProperty]
        //public List<Shipping> Shippings { get; set; }
        private string Id { get; set; }
        private IHouseKeeperRepository houseKeeperRepository;
        private IUserRepository userRepository;
        private IShippingRepository shippingRepository;
        public ProfileModel(IHouseKeeperRepository houseKeeperRepository, IUserRepository userRepository, IShippingRepository shippingRepository)
        {
            this.houseKeeperRepository = houseKeeperRepository;
            this.userRepository = userRepository;
            this.shippingRepository = shippingRepository;
            
        }
        public void OnGet()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Users = userRepository.GetUserById(id);
            Id = Users.Id;
            HouseKeeper = houseKeeperRepository.GetHouseKeeperById(Users.Id);
            TempData["gender"] = HouseKeeper.Gender;
            
            if (HouseKeeper == null)
                HouseKeeper =  houseKeeperRepository.CreateHouseKeepper(Users.Id);
            //Shippings = shippingRepository.GetShippingsForHouseKeeper(HouseKeeper.Id).ToList();
        }
        public IActionResult OnPost(string submit, string gender)
        {
            if(submit == "save")
            {
                var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                Users = userRepository.GetUserById(id);
                HouseKeeper.Gender = gender;
                //Shippings = shippingRepository.GetShippingsForHouseKeeper(HouseKeeper.Id).ToList();
                houseKeeperRepository.UpdateHouseKeeper(HouseKeeper, Users, Users.Id);
                TempData["gender"] = gender;
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
            
        }
    }
}
