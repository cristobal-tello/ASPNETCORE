using ASPNETCORE.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ASPNETCORE.Web.Interfaces;

namespace ASPNETCORE.Web.Clients
{
    public class HttpTeamClient : ITeamClient
    {
        private readonly string url;

        public HttpTeamClient(string url)
        {
            this.url = url;
        }

        public async Task<Team> GetTeam(Guid teamID)
        {
            Team team = new Team();

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(url);
                Uri uriBase = new Uri(uri.GetLeftPart(UriPartial.Authority));

                httpClient.BaseAddress = uriBase;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = string.Format("{0}/{1}", uri.AbsolutePath, teamID.ToString());
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

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(url);
                Uri uriBase = new Uri(uri.GetLeftPart(UriPartial.Authority));

                httpClient.BaseAddress = uriBase;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(uri.AbsolutePath);

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
