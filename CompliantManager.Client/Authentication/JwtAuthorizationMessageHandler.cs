using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace CompliantManager.Client.Authentication
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly string url;

        public JwtAuthorizationMessageHandler(ILocalStorageService localStorage, IWebAssemblyHostEnvironment hostEnvironment)
        {
            _localStorage = localStorage;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(hostEnvironment.BaseAddress)
            };
            url = hostEnvironment.BaseAddress;
            Console.WriteLine(hostEnvironment.BaseAddress);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken", cancellationToken);
            var email = GetEmailFromJwt(token);
            if (!string.IsNullOrEmpty(email) && !await DoesUserExist(email, token))
            {
                // Remove token from local storage to log out user
                await _localStorage.RemoveItemAsync("authToken", cancellationToken);
            }
            else if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<bool> DoesUserExist(string email, string? token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return false;

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/auth/exists?email={email}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        private string? GetEmailFromJwt(string? jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
                return null;

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken? jwt = null;

            try
            {
                jwt = handler.ReadJwtToken(jwtToken);
            }
            catch
            {
                return null;
            }

            var emailClaim = jwt?.Claims.FirstOrDefault(c =>
                c.Type == "email");

            return emailClaim?.Value;
        }
    }
}
