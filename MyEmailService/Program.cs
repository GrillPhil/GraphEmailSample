using MyEmailService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GraphAuthService>();
builder.Services.AddScoped<GraphMailService>();

var app = builder.Build();

app.MapGet("/", async(GraphMailService mailService) => await mailService.SendAsync(
    "<YOUR_SENDER_EMAIL>",
    "<YOUR_RECIPENT_EMAIL>",
    "<YOUR_SUBJECT>",
    "<YOUR_MESSAGE>"));

app.Run();
