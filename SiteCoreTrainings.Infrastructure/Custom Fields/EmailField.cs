using System;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using Sitecore;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace SiteCoreTrainings.Infrastructure
{
    public class EmailField : Edit
    {
        public EmailField()
        {
            this.Class = "scContentControl";
        }

        public override void HandleMessage(Message message)
        {
            base.HandleMessage(message);
            switch (message.Name)
            {
                case "emailfield:validate":
                    this.EmailValidate();
                    break;
            }
        }

        protected void EmailValidate()
        {
            string currentvalue = WebUtil.GetFormValue(ID);
            if (Regex.IsMatch(currentvalue, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                SheerResponse.SetInnerHtml("validator_" + ID, "Valid email entered!");
            else
                SheerResponse.SetInnerHtml("validator_" + ID, "Invalid");
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.ServerProperties["Value"] = this.ServerProperties["Value"];
        }

        protected override void Render(System.Web.UI.HtmlTextWriter output)
        {
            base.Render(output);
            RenderValidatorOutput(output);
        }

        protected void RenderValidatorOutput(System.Web.UI.HtmlTextWriter output)
        {
            HtmlGenericControl validatortext = new HtmlGenericControl("div");

            validatortext.Attributes.Add("ID", "validator_" + ID);

            validatortext.Attributes.Add("style", "color:grey");

            validatortext.InnerHtml = "";

            validatortext.RenderControl(output);
        }

        protected override bool LoadPostData(string value)
        {
            value = StringUtil.GetString(new string[1]
            {
                value
            });
            if (!(Value != value))
                return false;

            this.Value = value;

            base.SetModified();
            return true;
        }
    }
}
