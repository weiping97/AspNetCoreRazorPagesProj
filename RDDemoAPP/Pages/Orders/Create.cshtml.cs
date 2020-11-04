using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RDDemoAPP.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public List<SelectListItem> FoodItems { get; set; }

        [BindProperty]  //use -> write/bind to this model by using POST
        public OrderModel Order { get; set; }
        public CreateModel(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }
        public async Task OnGet()
        {
            var food = await _foodData.GetFood();

            FoodItems = new List<SelectListItem>();

            food.ForEach(rec =>
            {
                FoodItems.Add(new SelectListItem { Value = rec.Id.ToString(), Text = rec.Title });
            });

        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return Page();
            }

            //shortcut
            var food = await _foodData.GetFood();

            Order.Total = Order.Quantity * food.Where(x => x.Id == Order.FoodId).First().Price;

            int id = await _orderData.CreateOrder(Order);

            return RedirectToPage("./Create");
        }
    }
}
