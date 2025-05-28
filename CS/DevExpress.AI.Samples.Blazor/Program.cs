using System.ClientModel;
using Azure.AI.OpenAI;
using DevExpress.AI.Samples.Blazor.Services;
using DevExpress.AI.Samples.Blazor.Components;
using DevExpress.AI.Samples.Blazor.Data;
using DevExpress.AIIntegration;
using Microsoft.Extensions.AI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();

string azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
string azureOpenAIKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
string deploymentName = "gpt4o-big";

var azureOpenAIClient = new AzureOpenAIClient(
    new Uri(azureOpenAIEndpoint),
    new ApiKeyCredential(azureOpenAIKey));

var chatClient = azureOpenAIClient.GetChatClient(deploymentName).AsIChatClient();

var assistantCreator = new AIAssistantCreator(azureOpenAIClient, deploymentName);

builder.Services.AddDevExpressBlazor();
builder.Services.AddDevExpressServerSideBlazorReportViewer();
builder.Services.AddChatClient(chatClient);
builder.Services.AddDevExpressAI((config) => {
    //Reference the DevExpress.AIIntegration.OpenAI NuGet package to use Open AI Asisstants
    config.RegisterOpenAIAssistants(azureOpenAIClient, deploymentName); 
});
builder.Services.AddSingleton(assistantCreator);
builder.Services.AddSingleton<IDemoReportSource, DemoReportSource>();
builder.Services.AddDbContextFactory<IssuesContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("IssuesConnectionString"));
});
builder.Services.AddScoped<IssuesDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
