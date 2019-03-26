using ASPNETCORE.Web.Client.Interfaces;
using ASPNETCORE.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCORE.Web.Clients
{
    public class HttpTeamClient : ITeamClient
    {
        private readonly string url;
        private readonly Uri uri;
        private readonly Uri uriBase;

        public HttpTeamClient(string url)
        {
            this.url = url;
            this.uri = new Uri(url);
            this.uriBase = new Uri(uri.GetLeftPart(UriPartial.Authority));
        }
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = this.uriBase,
                
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public async Task AddTeamAsync(Team team)
        {
            using (var httpClient = CreateClient())
            {
                var jsonString = JsonConvert.SerializeObject(team);
                HttpResponseMessage response =
                  await httpClient.PostAsync(this.uri, new StringContent(jsonString, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task<Team> GetTeam(Guid teamID)
        {
            Team team = new Team();

            using (var httpClient = CreateClient())
            {
                var request = string.Format("{0}/{1}", this.uri.AbsolutePath, teamID.ToString());
                HttpResponseMessage response = await httpClient.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    team = JsonConvert.DeserializeObject<Team>(json);
                }
            }

            return team;
        }

        public async Task<ICollection<Team>> GetTeamsAsync()
        {
            ICollection<Team> teams = new List<Team>();

            using (var httpClient = CreateClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(this.uri.AbsolutePath);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    teams = JsonConvert.DeserializeObject<List<Team>>(json);
                }
            }

            return teams;
        }
    }
}
