using System.Net.Http.Json;
using System.Net.Http.Headers;
using Library_API_repeat.Client.Models.Auth;
using Microsoft.JSInterop;

namespace Library_API_repeat.Client.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public string? CurrentRole { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin =>
            IsAuthenticated &&
            CurrentRole == "Admin";

        public event Action? AuthStateChanged;

        public AuthenticationService(
            HttpClient httpClient,
            IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<LoginResponse?> LoginAsync(
            LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "/api/authentication/login",
                request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var loginResponse =
                await response.Content
                    .ReadFromJsonAsync<LoginResponse>();

            if (loginResponse == null ||
                string.IsNullOrWhiteSpace(loginResponse.Token))
            {
                return null;
            }

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "authToken",
                loginResponse.Token);

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "userRole",
                loginResponse.Role);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    loginResponse.Token);

            CurrentRole = loginResponse.Role;
            IsAuthenticated = true;

            AuthStateChanged?.Invoke();

            return loginResponse;
        }

        public async Task<bool> RegisterAsync(
            RegisterRequest request)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "/api/authentication/register",
                    request);

            return response.IsSuccessStatusCode;
        }

        public async Task InitializeAsync()
        {
            var token =
                await _jsRuntime.InvokeAsync<string>(
                    "localStorage.getItem",
                    "authToken");

            var role =
                await _jsRuntime.InvokeAsync<string>(
                    "localStorage.getItem",
                    "userRole");

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        token);

                CurrentRole = role;
                IsAuthenticated = true;
            }
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync(
                "localStorage.removeItem",
                "authToken");

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.removeItem",
                "userRole");

            _httpClient.DefaultRequestHeaders.Authorization = null;

            CurrentRole = null;
            IsAuthenticated = false;

            AuthStateChanged?.Invoke();
        }

    }
}
