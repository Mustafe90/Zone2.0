using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

    }
}
