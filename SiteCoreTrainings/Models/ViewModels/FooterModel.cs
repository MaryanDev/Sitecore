using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;


namespace SiteCoreTrainings.Models.ViewModels
{
    public class FooterModel : IRenderingModel
    {
        public Rendering Rendering { get; set; }
        public Item Item { get; set; }
        public Item PageItem { get; set; }

        public string FooterContent { get; set; }

        public void Initialize(Rendering rendering)
        {
            Rendering = rendering;
            Item = rendering.Item;
            PageItem = PageContext.Current.Item;

            FooterContent = Item["FooterContent"];
        }
    }
}
