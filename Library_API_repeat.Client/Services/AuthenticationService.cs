using System.Net.Http.Json;
using System.Net.Http.Headers;
using Library_API_repeat.Client.Models.Auth;
using Microsoft.JSInterop;

namespace Library_API_repeat.Client.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsruntime;

        public AuthenticationService(
            HttpClient httpClient,
            IJSRuntime jsruntime)
        {
            _httpClient = httpClient;
            _jsruntime = jsruntime;
        }
        public async Task InitializeAsync()
        {
            var token = await _jsruntime.InvokeAsync<string>(
                "localStorage.getItem",
                "authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                 "/api/authentication/login",
                request
                );

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (loginResponse == null || string.IsNullOrWhiteSpace(loginResponse.Token))
            {
                return null;
            }

            await _jsruntime.InvokeVoidAsync(
                "localStorage.setItem",
                "authToken",
                loginResponse.Token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                loginResponse.Token);

            return loginResponse;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "/api/authentication/register",
                request);

            return response.IsSuccessStatusCode;
        }

    }
}
