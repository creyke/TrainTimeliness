using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrainTimeliness.Client.Requests;
using TrainTimeliness.Client.Responses;

namespace TrainTimeliness.Client
{
    public class HspClient
    {
        private readonly string baseUrl;
        private HttpClient client;

        public HspClient(string baseUrl, string username, string password)
        {
            this.baseUrl = baseUrl;
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes($"{username}:{password}")));
        }

        public async Task<ServiceMetricsResponse> ServiceMetricsAsync(ServiceMetricsRequest request)
        {
            return await RequestAsync<ServiceMetricsResponse>(request);
        }

        public async Task<ServiceDetailsResponse> ServiceDetailsAsync(ServiceDetailsRequest request)
        {
            return await RequestAsync<ServiceDetailsResponse>(request);
        }

        private async Task<TResponse> RequestAsync<TResponse>(object request, [CallerMemberName] string name = null)
        {
            var content = new StringContent(
                            JsonConvert.SerializeObject(request),
                            Encoding.UTF8,
                            "application/json"
                        );

            var method = $"{name.Substring(0, 1).ToLowerInvariant()}{name.Substring(1, name.Length - 1).Replace("Async", string.Empty)}";

            var requestUri = $"{baseUrl}/api/v1/{method}";

            var httpResponse = await client.PostAsync(requestUri, content);

            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<TResponse>(responseContent);

            return response;
        }
    }
}
