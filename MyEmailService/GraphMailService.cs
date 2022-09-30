using Azure.Identity;
using Microsoft.Graph;

namespace MyEmailService;

public class GraphMailService
{
    private readonly IConfiguration _config;

    public GraphMailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string fromAddress, string toAddress, string subject, string content)
    {
        string? tenantId = _config["tenantId"];
        string? clientId = _config["clientId"];
        string? clientSecret = _config["clientSecret"];

        ClientSecretCredential credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        GraphServiceClient graphClient = new GraphServiceClient(credential);

        var message = new Message
        {
            Subject = subject,
            Body = new ItemBody
            {
                ContentType = BodyType.Text,
                Content = content
            },
            ToRecipients = new List<Recipient>()
            {
                new Recipient
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = toAddress
                    }
                }
            }
        };

        var saveToSentItems = true;

        await graphClient.Users[fromAddress]
          .SendMail(message, saveToSentItems)
          .Request()
          .PostAsync();
    }
}
