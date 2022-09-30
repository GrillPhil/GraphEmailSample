namespace MyEmailService.Model;

class GraphSendMailRequest
{
    public GraphMessage Message { get; set; }
    public bool SaveToSentItems { get; set; } = true;
}
