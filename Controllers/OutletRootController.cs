using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Upf_Info_Site.Models;

namespace Upf_Info_Site
{
    public class OutletRootController : RenderController 
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
        private readonly ServiceContext _serviceContext;

        public OutletRootController(ILogger<OutletController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor, ServiceContext context)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _variationContextAccessor = variationContextAccessor;
            _serviceContext = context;
        }

        [NonAction]
        public sealed override IActionResult Index() => throw new NotImplementedException();

        public IActionResult Index(string foodType)
        {
            var foodListingVm = new FoodListingViewModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor), foodType);
            var outletRoot = (OutletRoot)CurrentPage;
            var outlets = outletRoot.Children<Outlet>();
            foreach(var outlet in outlets)
            {
                var matchFound = false;
                var foodList = new List<Food>();

				foreach (var blockItem in outlet.ProductList)
                {					
					var productGroup = (ProductGroup1)blockItem.Content;
					if (productGroup.Heading.Equals(foodType, StringComparison.CurrentCultureIgnoreCase))
					{												
						foreach (var blockItem2 in productGroup.Products)
						{
							var foodDetails = (TextWithLink)blockItem2.Content;
							foodList.Add(new Food { Name = foodDetails.Text, Url = foodDetails.Link });
						}
						matchFound = true;
						break;
					}
                }

                if (matchFound)
                {   
                    foodListingVm.GroupedListings.Add(outlet.Name, foodList);
                }
            }
            return View("Outlet", foodListingVm);
        }
    }
}
