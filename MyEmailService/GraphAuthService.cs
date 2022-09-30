using Newtonsoft.Json;

namespace MyEmailService;

public class GraphAuthService
{
    public async Task<string?> GetAccessTokenAsync(string tenantId, string clientId, string clientSecret, string scope)
    {
        List<KeyValuePair<string, string>> values = new()
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", scope),
                new KeyValuePair<string, string>("resource", "https://graph.microsoft.com"),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };
        HttpClient client = new();
        string requestUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/token";
        FormUrlEncodedContent requestContent = new(values);
        HttpResponseMessage? response = await client.PostAsync(requestUrl, requestContent);
        string? responseBody = await response.Content.ReadAsStringAsync();
        dynamic? tokenResponse = JsonConvert.DeserializeObject(responseBody);
        return tokenResponse?.access_token;
    }
}