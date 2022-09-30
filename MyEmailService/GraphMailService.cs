using MyEmailService.Model;
using Newtonsoft.Json;
using System.Text;

namespace MyEmailService;

public class GraphMailService
{
    private const string ENDPOINT = "https://graph.microsoft.com/beta/";

    private readonly GraphAuthService _authService;
    private readonly IConfiguration _config;

    public GraphMailService(IConfiguration config, GraphAuthService authService)
    {
        _authService = authService;
        _config = config;
    }

    public async Task SendAsync(string fromAddress, string toAddress, string subject, string content)
    {
        string scope = "Mail.Send";
        HttpClient client = await CreateHttpClientWithAuthorizationAsync(scope);

        string requestUrl = $"{ENDPOINT}users/{fromAddress}/sendMail";
        GraphSendMailRequest request = new()
        {
            Message = new GraphMessage
            {
                Subject = subject,
                Body = new GraphItemBody
                {
                    Content = content
                },
                ToRecipients = new GraphRecipient[]
                {
                    new GraphRecipient
                    {
                        EmailAddress = new GraphEmailAddress
                        {
                            Address = toAddress
                        }
                    }
                }
            }
        };

        var requestBody = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        await client.PostAsync(requestUrl, requestBody);
    }

    private async Task<HttpClient> CreateHttpClientWithAuthorizationAsync(string scope)
    {
        string? tenantId = _config["tenantId"];
        string? clientId = _config["clientId"];
        string? clientSecret = _config["clientSecret"];
        string? token = await _authService.GetAccessTokenAsync(tenantId, clientId, clientSecret, scope);

        HttpClient client = new();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        return client;
    }
}
