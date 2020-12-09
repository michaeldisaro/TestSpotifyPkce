using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using TestWebApplication.Models;

namespace TestWebApplication.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(ILogger<IndexModel> logger,
                          IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            SpotifyCodes.Init();
            
            var parameters = new Dictionary<string, string>
            {
                {"client_id", SpotifyCodes.ClientId},
                {"response_type", "code"},
                {"redirect_uri", "https://localhost:5001/SpotifyResponse"},
                {"code_challenge_method", "S256"},
                {"code_challenge", SpotifyCodes.CodeChallenge},
                {"state", "1234567890"}
            };

            var url = QueryHelpers.AddQueryString("https://accounts.spotify.com/authorize", parameters);
            return Redirect(url);
        }

    }
}