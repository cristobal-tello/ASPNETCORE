using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Test
{
    class Program
    {
        static private HttpClient httpClient = new HttpClient();
        static void Main(string[] args)
        {
            NewMethodAsync().Wait();
        }

        private static async System.Threading.Tasks.Task NewMethodAsync()
        {
            try
            {
                string notificationServiceUri = "https://localhost:44366/services/notificationteam";

                Uri uri = new Uri(notificationServiceUri);
                Uri uriBase = new Uri(uri.GetLeftPart(UriPartial.Authority));

                httpClient.BaseAddress = uriBase;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = JsonConvert.SerializeObject("NOMBRE EQUIPO");
                /*  HttpResponseMessage response =
                    await httpClient.PostAsync(notificationServiceUri, new StringContent(team.Name, Encoding.UTF8, "application/json"));*/

                Console.WriteLine(uri.AbsolutePath);
                Console.WriteLine(uriBase.AbsolutePath);



                HttpResponseMessage response = await
                    httpClient.PostAsync(uri.AbsolutePath, new StringContent(jsonString, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
