using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMSDesktopUWP.Core.Models;

namespace SMSDesktopUWP.Core.Services
{
    public class WebApiService
    {
        // my local: https://localhost:44352/api/
        // azure: https://lcmsmswebapi20200711192512.azurewebsites.net/api/
        private const string BaseUrl = "https://lcmsmswebapi20200711192512.azurewebsites.net/api/";
        private const string OrphansController = "orphans";

        static readonly HttpClient _client = new HttpClient();

        public async Task<List<Orphan>> GetOrphansAsync()
        {
            string uri = $"{BaseUrl}{OrphansController}";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            // TODO Add JWT token to header
            // request.Headers 

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var contentAsString = await response.Content.ReadAsStringAsync();
            var orphans = JsonConvert.DeserializeObject<List<Orphan>>(contentAsString);

            foreach (var orphan in orphans)
            {
                Trace.WriteLine(orphan.FirstName);
            }

            return orphans;
        }

        public async Task AddOrphanAsync(Orphan newOrphan)
        {
            string uri = $"{BaseUrl}{OrphansController}";

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var payload = JsonConvert.SerializeObject(newOrphan);

            // TODO Add JWT token to header
            // request.Headers 

            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");


            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateOrphanAsync(int orphanId, OrphanUpdate orphanUpdate)
        {
            string uri = $"{BaseUrl}{OrphansController}/{orphanId}";

            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            var payload = JsonConvert.SerializeObject(orphanUpdate);

            // TODO Add JWT token to header
            // request.Headers 

            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrphanAsync(int orphanId)
        {
            string uri = $"{BaseUrl}{OrphansController}/{orphanId}";

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            //TODO Add JWT token to header
            // request.Headers

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Sponsor>> GetSponsorsAsync()
        {
            string uri = $"{BaseUrl}sponsors";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            // TODO Add JWT token to header
            // request.Headers 

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var contentAsString = await response.Content.ReadAsStringAsync();
            var sponsors = JsonConvert.DeserializeObject<List<Sponsor>>(contentAsString);


            return sponsors;
        }

        public async Task<List<Guardian>> GetGuardiansAsync()
        {
            string uri = $"{BaseUrl}guardians";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            // TODO Add JWT token to header
            // request.Headers 

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var contentAsString = await response.Content.ReadAsStringAsync();
            var guardians = JsonConvert.DeserializeObject<List<Guardian>>(contentAsString);

            return guardians;
        }

        public async Task<List<Narration>> GetNarrationsAsync()
        {
            string uri = $"{BaseUrl}narrations";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            // TODO Add JWT token to header
            // request.Headers 

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var contentAsString = await response.Content.ReadAsStringAsync();
            var narrations = JsonConvert.DeserializeObject<List<Narration>>(contentAsString);


            return narrations;
        }

        public async Task<List<Academic>> GetAcademicsAsync()
        {
            string uri = $"{BaseUrl}academics";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            // TODO Add JWT token to header
            // request.Headers 

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var contentAsString = await response.Content.ReadAsStringAsync();
            var academics = JsonConvert.DeserializeObject<List<Academic>>(contentAsString);


            return academics;
        }
    }
}
