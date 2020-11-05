using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RDDemoAPP.Pages.Food
{
    public class ListModel : PageModel
    {
        private readonly IFoodData _foodData;

        public List<FoodModel> Food;
        public ListModel(IFoodData foodData)
        {
            _foodData = foodData;
        }

        public async Task OnGet()
        {
            Food = await _foodData.GetFood();
        }
    }
}
