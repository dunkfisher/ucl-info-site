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
    public class OutletController : RenderController 
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
		private readonly ServiceContext _serviceContext;

		private readonly string[] _mealTypeAliases = { "breakfast", "lunch", "dinner", "snack" };
        private readonly string[] _foodTypeAliases = { "bread", "cereal", "drinks", "lunches", "readyMeals", "saucesDipsEtc", "snacks", "soup", "treats" };

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

            string[] typeAliases;
            var currentPage = (Outlet)CurrentPage;
            if (currentPage.IsRetail)
            {
                typeAliases = _foodTypeAliases;
            }
            else
            {
                typeAliases = _mealTypeAliases;
            }

            foreach (var type in typeAliases)
            {
                if (CurrentPage.HasValue(type))
                {
                    foodListingVm.GroupedListings.Add(type, CurrentPage.Value<string[]>(type));
                }
            }

            return CurrentTemplate(foodListingVm);
        }

        #region WIP for filtering on different things

        //public OutletController(
        //    IUmbracoContextAccessor umbracoContextAccessor,
        //    IUmbracoDatabaseFactory databaseFactory,
        //    ServiceContext services,
        //    AppCaches appCaches,
        //    IProfilingLogger profilingLogger,
        //    IPublishedUrlProvider publishedUrlProvider)
        //    : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        //{
        //    _umbracoContext = umbracoContextAccessor.GetRequiredUmbracoContext();
        //}

        //public IActionResult Index(string outlet = "", string mealType = "", string foodType = "")
        //{
        //    if (string.IsNullOrEmpty(outlet))
        //    {
        //        return NotFound();
        //    }

        //    var foodListing = new FoodListing();
        //    foodListing.Heading = outlet;

        //    var outletPage = _umbracoContext.Content.GetById(Guid.Parse(OutletFolderId))
        //        .Children
        //        .SingleOrDefault(x => string.Equals(x.Name, outlet, StringComparison.CurrentCultureIgnoreCase));

        //    if (outletPage == null)
        //    {
        //        return NotFound();
        //    }

        //    string[] typeAliases;
        //    if (outletPage.HasValue("isRetail") && outletPage.Value<bool>("isRetail"))
        //    {
        //        typeAliases = _foodTypeAliases;
        //    }
        //    else
        //    {
        //        typeAliases = _mealTypeAliases;
        //    }

        //    foreach (var type in typeAliases)
        //    {
        //        if (outletPage.HasValue(type))
        //        {
        //            foodListing.GroupedListings.Add(type, outletPage.Value<List<string>>(type));
        //        }
        //    }

        //    return View("FoodListing", foodListing);
        //}

        #endregion
    }
}
