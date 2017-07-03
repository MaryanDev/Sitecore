using Newtonsoft.Json;
using SiteCoreTrainings.Infrastructure.Models;
using System.Web;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Home
    {
        public string YoutubeVideo
        {
            get
            {
                if (this.Youtube_Video.StartsWith("https://www.youtube.com/watch?"))
                {
                    var parsed = HttpUtility.ParseQueryString(this.Youtube_Video);
                    var videoId = parsed.Get("https://www.youtube.com/watch?v");
                    if (!string.IsNullOrEmpty(videoId))
                        return "https://www.youtube.com/embed/" + videoId;
                    return "";
                }
                else if (this.Youtube_Video.StartsWith("https://www.youtube.com/embed/"))
                {
                    return this.Youtube_Video;
                }
                return "";
            }
            set { }
        }

        public Address StrongAddress
        {
            get { return JsonConvert.DeserializeObject<Address>(this.Address); }
            set { }
        }
    }
}
