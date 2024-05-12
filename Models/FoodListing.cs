using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;

namespace Upf_Info_Site.Models
{
    public class FoodListingViewModel : PublishedContentWrapped
	{		
		public FoodListingViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
		{
			Heading = content.Name;
			GroupedListings = new Dictionary<string, IEnumerable<Food>>();
		}

        public FoodListingViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback, string heading, IHtmlEncodedString additionalText) : base(content, publishedValueFallback)
        {
            Heading = heading;
            AdditionalText = additionalText;
            GroupedListings = new Dictionary<string, IEnumerable<Food>>();
        }

        public string Heading { get; set; }
        public IHtmlEncodedString AdditionalText { get; set; }
        public Dictionary<string, IEnumerable<Food>> GroupedListings { get; set; }
    }

    public struct Food
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
