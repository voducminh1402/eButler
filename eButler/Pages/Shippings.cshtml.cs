using BusinessLogic.Models;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace eButler.Pages
{
    [Authorize()]
    public class ShippingsModel : PageModel
    {
        private readonly IShippingRepository shippingRepository;
        public ShippingsModel(IShippingRepository shippingRepository)
        {
            this.shippingRepository = shippingRepository;
        }
        public IList<Shipping> Shippings { get; set; }

        public void OnGet(string id)
        {
            Shippings = shippingRepository.GetShippingsForHouseKeeper(id).ToList();
        }
    }
}
