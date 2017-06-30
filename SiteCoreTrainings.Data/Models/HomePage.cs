using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Newtonsoft.Json;
using Sitecore.Data;
using SiteCoreTrainings.Infrastructure.Models;

namespace SiteCoreTrainings.Data.Models
{
    [SitecoreType]
    public class HomePage : IContentBase
    {
        public ID Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }

        [SitecoreField("Page Header")]
        public string PageHeader { get; set; }

        [SitecoreField("Main Image")]
        public Image MainImage { get; set; }

        [SitecoreField("Slider Images")]
        public IEnumerable<Image> SliderImages { get; set; }

        [SitecoreField("Youtube Video")]
        private string YoutubeSrc { get; set; }

        public string YoutubeVideo
        {
            get
            { 
                if (this.YoutubeSrc.StartsWith("https://www.youtube.com/watch?"))
                {
                    var parsed = HttpUtility.ParseQueryString(this.YoutubeSrc);
                    var videoId = parsed.Get("https://www.youtube.com/watch?v");
                    if (!string.IsNullOrEmpty(videoId))
                        return "https://www.youtube.com/embed/" + videoId;
                    return "";
                }
                else if (this.YoutubeSrc.StartsWith("https://www.youtube.com/embed/"))
                {
                    return this.YoutubeSrc;
                }
                return "";
            }
            set { }
        }

        [SitecoreField("Address")]
        private string RawAddress { get; set; }

        public Address Address
        {
            get { return JsonConvert.DeserializeObject<Address>(this.RawAddress); }
            set { }
        }
    }
}
