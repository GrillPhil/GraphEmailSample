namespace MyEmailService.Model;

class GraphMessage
{
    public GraphItemBody Body { get; set; }
    public string Subject { get; set; }
    public GraphRecipient[] ToRecipients { get; set; }
}
