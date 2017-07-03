using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SiteCoreTrainings.Infrastructure.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZoomValue { get; set; }
        public bool IsAddressValid { get; set; }

        public Location Location { get; set; }

        internal static AddressValidationResponse ValidateAddress(string address)
        {
            var jsonAddress = JsonConvert.DeserializeObject<Address>(address);

            var validationRequestBody = new Dictionary<string, string>
            {
                {"street1", jsonAddress.Street},
                {"city", jsonAddress.City},
                {"state", jsonAddress.State},
                {"country", "US"},
                {"validate", "true"}
            };

            var request = (HttpWebRequest)WebRequest.Create("https://api.goshippo.com/addresses/");

            var postData = JsonConvert.SerializeObject(validationRequestBody);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", "ShippoToken shippo_test_8ed060a354a98e3e58e12c5ac68393dc697ce56e");

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            try
            {
                var response = (HttpWebResponse) request.GetResponse();

                var validationResult =
                    JsonConvert.DeserializeObject<AddressValidationResponse>(
                        new StreamReader(response.GetResponseStream()).ReadToEnd());

                return validationResult;
            }
            catch
            {
                return null;
            }
        }

        public Location GetAddressLocation()
        {
            var uriAddress = (this.Street + " " + this.City + " " + this.State).Replace(" ", "+");
            var request = WebRequest.Create($"http://maps.google.com/maps/api/geocode/json?address={uriAddress}");

            request.Method = "GET";
            request.ContentType = "application/json";

            var response = (HttpWebResponse)request.GetResponse();

            var result = JObject.Parse(new StreamReader(response.GetResponseStream()).ReadToEnd());

            try
            {
                var location =
                    JsonConvert.DeserializeObject<Location>(result["results"][0]["geometry"]["location"].ToString());
                return location;
            }
            catch(Exception)
            {
                if (!result["results"].Any() && result["status"].ToString() == "ZERO_RESULTS")
                    return null;
                throw;
            }
        }
    }
}
