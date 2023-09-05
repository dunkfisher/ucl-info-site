using Umbraco.Cms.Core.Models.PublishedContent;

namespace Upf_Info_Site.Models
{
    public class FoodListingViewModel : PublishedContentWrapped
	{		
		public FoodListingViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
		{
			Heading = content.Name;
			GroupedListings = new Dictionary<string, IEnumerable<string>>();
		}
     
        public string Heading { get; set; }
        public Dictionary<string, IEnumerable<string>> GroupedListings { get; set; }
    }
}
