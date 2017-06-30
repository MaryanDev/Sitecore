using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.Data.Converters;
using Sitecore.Data.Validators.ItemValidators;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using SiteCoreTrainings.Infrastructure.Models;
using Control = Sitecore.Web.UI.HtmlControls.Control;
using Image = System.Web.UI.WebControls.Image;


namespace SiteCoreTrainings.Infrastructure.Custom_Fields
{
    public class AddressField : Control, IContentField
    {
        private Address address;
        public override void HandleMessage(Message message)
        {
            var val = JsonConvert.DeserializeObject<Address>(Value);
            switch (message.Name)
            {
                case "addressfield:validate":
                    this.AddressValidate();
                    break;
                case "addressfield:zoomin":
                    val.ZoomValue = val.ZoomValue + 1 > 20 ? val.ZoomValue : val.ZoomValue + 1;
                    Value = JsonConvert.SerializeObject(val);
                    Sitecore.Context.ClientPage.Dispatch("contenteditor:save");
                    break;
                case "addressfield:zoomout":
                    val.ZoomValue = val.ZoomValue - 1 < 0 ? val.ZoomValue : val.ZoomValue - 1;
                    Value = JsonConvert.SerializeObject(val);
                    Sitecore.Context.ClientPage.Dispatch("contenteditor:save");
                    break;
                case "addressfield:handlebtnclick":
                    SheerResponse.SetInnerHtml("validator_" + ID, "Clicked");
                    break;
            }
            base.HandleMessage(message);
        }

        private void AddressValidate()
        {
            var addressValidationResponse = Address.ValidateAddress(Value);
            var message = "";
            if (addressValidationResponse?.ValidationResult != null)
            {
                
                if (addressValidationResponse.ValidationResult.IsValid)
                {
                    message = "Valid address entered!";
                    
                    //Sitecore.Context.ClientPage.Dispatch("contenteditor:save");
                }
                else
                {
                    message = addressValidationResponse.ValidationResult.Messages != null &&
                                       addressValidationResponse.ValidationResult.Messages.Any()
                        ? $"{addressValidationResponse.ValidationResult.Messages.FirstOrDefault().Code}, {addressValidationResponse.ValidationResult.Messages.FirstOrDefault().Text}"
                        : "Address does not exist";
                }
            }
            else
            {
                message = "Address does not exist";
            }
            SheerResponse.SetInnerHtml("validator_" + ID, message);
            SheerResponse.Alert(message, false);
            if (address != null)
            {
                var location = $"{address.Street} {address.City} {address.State}".Replace(" ", "+");
                var imgUrl = GetGoogleMapImageUrl(location, "400", "210", address.ZoomValue.ToString());
                var id = ID + "_Img_MapView";
                SheerResponse.SetImageSrc(id, imgUrl, 400, 210, "", "0 0 0 0 ");
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            if (!Sitecore.Context.ClientPage.IsEvent)
            {
                InitialyzePage();
            }
            else
            {

                var street = FindControl(GetID("streetTextBox")) as Text;
                var city = FindControl(GetID("cityTextBox")) as Text;
                var state = FindControl(GetID("stateTextBox")) as Text;

                this.address = new Address
                {
                    Street = street.Value,
                    City = city.Value,
                    State = state.Value
                };
                if (!string.IsNullOrEmpty(Value))
                    address.ZoomValue = Convert.ToInt32(JObject.Parse(Value)["ZoomValue"]?.ToString() ?? "14");
                else
                    address.ZoomValue = 14;

                address.Location = address.GetAddressLocation();

                this.Value = JsonConvert.SerializeObject(address);
            }
            base.OnLoad(e);
        }

        private void InitialyzePage()
        {
            address = JsonConvert.DeserializeObject<Address>(Value);
            var streetTextBox = CreateText("Stret", "streetTextBox");
            var cityTextBox = CreateText("City", "cityTextBox");
            var stateTextBox = CreateText("State", "stateTextBox");

            var mapImageCtrl = InitialyzeMapImg(Value);

            //var mapContainer = new HtmlGenericControl("div");
            //mapContainer.Attributes.Add("style", "width:150px;height:100px;");
            //mapContainer.ID = "mapContainerCustom";
            //mapContainer.InnerHtml =  @"<script>
            //    function initCustomControlMap() {
            //    var center = { lat: " + (address?.Location?.Lattitude ?? 0) + @", lng: " + (address?.Location?.Longittude ?? 0) + @"};
            //var mapOptions = {
            //        center: center,
            //        zoom: " + (address?.ZoomValue ?? 14) + @",
            //        mapTypeId: google.maps.MapTypeId.HYBRID
            //    }
            //    var map = new google.maps.Map(document.getElementById('myCustomControlMap'), mapOptions);
            //var marker = new google.maps.Marker({
            //       position: center,
            //    map: map
            //    });
            //</script>
            //<div id='myCustomControlMap' style='height:100px;'></div>
            //<script src='https://maps.googleapis.com/maps/api/js?key=AIzaSyBgH868XE87f5naVfKznFT2zFxB1mrWZyc&callback=initCustomControlMap'></script>
            //";

            //this.Controls.Add(mapContainer);

            var itemTable = InitialyzeTable(streetTextBox, cityTextBox, stateTextBox, mapImageCtrl);
            {
                this.Controls.Add(itemTable);
            }

            this.Controls.Add(InitialyzeValidationMessagge());

            if (address != null)
            {
                streetTextBox.Value = address.Street;
                cityTextBox.Value = address.City;
                stateTextBox.Value = address.State;

            }
            else
            {
                streetTextBox.Value = string.Empty;
                cityTextBox.Value = string.Empty;
                stateTextBox.Value = string.Empty;
            }
        }

        private Text CreateText(string placeholer, string id)
        {
            return new Text
            {
                Placeholder = placeholer,
                ID = GetID(id)
            };
        }

        private Image InitialyzeMapImg(string value)
        {
            var mapImageCtrl = new Image
            {
                ID = ID + "_Img_MapView",
                CssClass = "imageMapView",
                Width = 400,
                Height = 210
            };
            if (!string.IsNullOrEmpty(value))
            {
                var tempAddress = JsonConvert.DeserializeObject<Address>(value);
                var location = $"{tempAddress.Street} {tempAddress.City} {tempAddress.State}".Replace(" ", "+");
                var zoom = tempAddress.ZoomValue;

                mapImageCtrl.ImageUrl = GetGoogleMapImageUrl(location, mapImageCtrl.Width.Value.ToString(),
                    mapImageCtrl.Height.Value.ToString(), zoom.ToString());
                if (tempAddress.Location != null)
                    mapImageCtrl.ToolTip = $"Lat - {tempAddress.Location.Lattitude}, Lng - {tempAddress.Location.Longittude}";
            }
            return mapImageCtrl;
        }
        private HtmlTable InitialyzeTable(Text streetText, Text cityText, Text stateText, Image mapImg)
        {
            //var btn = new Button();
            //btn.CssStyle = "width:30px;height:20px;";
            //btn.Width = 20;
            //btn.Height = 20;
            //btn.CssClass = "";
            //btn.Header = "Message";
            //btn.Click = "addressfield:handlebtnclick";

            //this.Controls.Add(btn);
            var itemTable = new HtmlTable
            {
                Rows =
                {
                    new HtmlTableRow
                    {
                        Cells =
                        {
                            new HtmlTableCell
                            {
                                Controls = {new Label {Header = "Street"}},
                                Width = "70px"
                            },
                            new HtmlTableCell
                            {
                                Controls = {streetText},
                                Width = "250px"
                            },
                            new HtmlTableCell
                            {
                                RowSpan = 3,
                                Width = "20px"
                            },
                            new HtmlTableCell
                            {
                                Controls = {mapImg},
                                RowSpan = 3
                            }
                        }
                    },
                    new HtmlTableRow
                    {
                        Cells =
                        {
                            new HtmlTableCell
                            {
                                Controls = {new Label {Header = "City"}},
                                Width = "70px"
                            },
                            new HtmlTableCell
                            {
                                Controls = {cityText},
                                Width = "250px"
                            }
                        }
                    },
                    new HtmlTableRow
                    {
                        Cells =
                        {
                            new HtmlTableCell
                            {
                                Controls = {new Label {Header = "State"}},
                                Width = "70px"
                            },
                            new HtmlTableCell
                            {
                                Controls = {stateText},
                                Width = "250px"
                            }
                        }
                    },
                },
                Height = "250px",
                ID = "addressControlTable"
            };
            return itemTable;
        }

        private HtmlArea InitialyzeValidationMessagge()
        {
            var validationArea = new HtmlArea();
            validationArea.Attributes.Add("ID", "validator_" + ID);

            validationArea.Attributes.Add("style", "color:grey");

            return validationArea;
        }
        private string GetGoogleMapImageUrl(string location, string width, string heigth, string zoom)
        {
            return
    string.Format(
               "http://maps.googleapis.com/maps/api/staticmap?&q={0}&zoom={1}&size={2}x{3}&sensor=false&maptype=roadmap&markers=color:blue%7Clabel:1%7C{0}&key=AIzaSyA9Tonr3a48iyB-V0WVRlHQe8dbBKjYnNk",
               location, zoom, width, heigth);
        }

        public string GetValue()
        {
            return Value;
        }

        public void SetValue(string value)
        {
            Value = value;
        }
    }
}
