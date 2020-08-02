using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Zone.Core.Service
{
    public class ShareHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShareHttpClientService> _logger;

        public ShareHttpClientService(HttpClient httpClient, ILogger<ShareHttpClientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> ShareAlbums()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "weatherforecast");
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer",BuildJWTToken());

            var content = await _httpClient.SendAsync(message);

            return await content.Content.ReadAsStringAsync();
        }

        private string BuildJWTToken()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "User Id"),
                new Claim(ClaimTypes.Name, "Mustafe")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mustafe is beautiful"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(120),
                IssuedAt = DateTime.UtcNow,
                
            };
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(securityToken);
        }

    }
}
