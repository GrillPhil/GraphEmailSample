using MyEmailService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GraphMailService>();

var app = builder.Build();

app.MapGet("/", async(GraphMailService mailService) => await mailService.SendAsync(
    "bauknecht@medialesson.de",
    "bauknecht@medialesson.de",
    "Testmail",
    "Hello world."));

app.Run();
