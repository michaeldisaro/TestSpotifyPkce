using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestWebApplication.Models;

namespace TestWebApplication.Pages
{
    public class SpotifyResponse : PageModel
    {

        private readonly ILogger<SpotifyResponse> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        public SpotifyResponse(ILogger<SpotifyResponse> logger,
                               IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;

        }

        public async Task OnGet(string code,
                                string state,
                                string error)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var parameters = new Dictionary<string, string>
            {
                {"client_id", SpotifyCodes.ClientId},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", "https://localhost:5001/SpotifyResponse"},
                {"code_verifier", SpotifyCodes.CodeVerifier}
            };

            var urlEncodedParameters = new FormUrlEncodedContent(parameters);
            var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token") { Content = urlEncodedParameters };
            var response = await httpClient.SendAsync(req);
            var content = response.Content;
        }

    }
}