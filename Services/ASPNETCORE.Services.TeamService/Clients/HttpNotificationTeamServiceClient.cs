using ASPNETCORE.Services.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCORE.Services.Clients.Interfaces

{
    public class HttpNotificationTeamServiceClient : IHttpNotificationTeamServiceClient
    {
        private readonly string notificationServiceUri;
        
        
        public HttpNotificationTeamServiceClient(string notificationServiceUri)
        {
            this.notificationServiceUri = notificationServiceUri;
            
        }
        public async Task NewTeamAsync(Team team)
        {
            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(notificationServiceUri);
                Uri uriBase = new Uri(uri.GetLeftPart(UriPartial.Authority));

                httpClient.BaseAddress = uriBase;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = JsonConvert.SerializeObject(team.Name);

                HttpResponseMessage response = await
                    httpClient.PostAsync(uri.AbsolutePath, new StringContent(jsonString, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
