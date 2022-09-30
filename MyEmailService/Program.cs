using MyEmailService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GraphMailService>();

var app = builder.Build();

app.MapGet("/", async(GraphMailService mailService) => await mailService.SendAsync(
    "<YOUR_SENDER_ADDRESS>",
    "<YOUR_RECIPENT_ADDRESS>",
    "<YOUR_SUBJECT>",
    "<YOUR_MESSAGE>"));

app.Run();
