using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore;

namespace SiteCoreTrainings.Infrastructure.Custom_Fields
{
    public class YoutubeVideoField: Edit
    {
        public YoutubeVideoField()
        {
            this.Class = "scContentControl";
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.ServerProperties["Value"] = this.ServerProperties["Value"];
        }

        protected override void Render(System.Web.UI.HtmlTextWriter output)
        {
            base.Render(output);
        }
        protected override bool LoadPostData(string value)
        {
            if (value.StartsWith("https://www.youtube.com/watch?"))
            {
                var parsed = HttpUtility.ParseQueryString(value);
                var videoId = parsed.Get("https://www.youtube.com/watch?v");
                if (!string.IsNullOrEmpty(videoId))
                    Value =  "https://www.youtube.com/embed/" + videoId;
            }
            else if (value.StartsWith("https://www.youtube.com/embed/"))
            {
                Value = value;
            }

            //value = StringUtil.GetString(new string[1]
            //{
            //    value
            //});
            //if (Value == value)
            //    return false;

            //this.Value = value;

            base.SetModified();
            return true;
        }
    }
}
