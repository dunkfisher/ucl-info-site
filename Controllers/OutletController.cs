﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Upf_Info_Site.Models;

namespace Upf_Info_Site
{
    public class OutletController : RenderController 
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
		private readonly ServiceContext _serviceContext;

        public OutletController(ILogger<OutletController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor, ServiceContext context)
			: base(logger, compositeViewEngine, umbracoContextAccessor)
		{
			_variationContextAccessor = variationContextAccessor;
			_serviceContext = context;
		}

		public override IActionResult Index()
		{
            var foodListingVm = new FoodListingViewModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor));
			foodListingVm.Heading = CurrentPage?.Name ?? string.Empty;
            var currentPage = (Outlet)CurrentPage;
            foodListingVm.AdditionalText = currentPage.MainText;
            foreach (var blockItem in currentPage.ProductList)
            {
                var foodList = new List<Food>();
                var productGroup = (ProductGroup1)blockItem.Content;
                foreach (var blockItem2 in productGroup.Products)
                {
                    var foodDetails = (TextWithLink)blockItem2.Content;
                    foodList.Add(new Food { Name = foodDetails.Text, Url = foodDetails.Link });
                }
                foodListingVm.GroupedListings.Add(productGroup.Heading, foodList);
            }
            return CurrentTemplate(foodListingVm);
        }
    }
}
