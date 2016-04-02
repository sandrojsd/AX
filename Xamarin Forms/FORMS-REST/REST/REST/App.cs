using Newtonsoft.Json;
using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace REST
{
    public class RETORNO
    {
        [JsonProperty("$id")]
        public string id { get; set; }
        public string currentDateTime { get; set; }
        public string utcOffset { get; set; }
        public bool isDayLightSavingsTime { get; set; }
        public string dayOfTheWeek { get; set; }
        public string timeZoneName { get; set; }
        public long currentFileTime { get; set; }
        public string ordinalDate { get; set; }
        public object serviceResponse { get; set; }
    }

    

    public class App : Application
    {
        async void GetClock()
        {
            var client = new RestClient("http://worldclockapi.com");

            var request = new RestRequest("api/json/utc/now", System.Net.Http.HttpMethod.Get);

            IRestResponse<RETORNO> response2 = await client.Execute<RETORNO>(request);

            Debug.WriteLine(response2.Data.currentDateTime);
        }

        public App()
        {
            GetClock();

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
