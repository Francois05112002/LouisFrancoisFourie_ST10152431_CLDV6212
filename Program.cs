//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//var host = new HostBuilder()
//    .ConfigureFunctionsWebApplication()
//    .ConfigureServices(services =>
//    {
//        services.AddApplicationInsightsTelemetryWorkerService();
//        services.ConfigureFunctionsApplicationInsights();

//    })
//    .ConfigureWebJobs(b =>
//    {
//        // Register specific storage bindings
//        b.AddHttp();
//        b.AddAzureStorageBlobs(); // For Blob Storage functions
//        b.AddAzureStorageQueues(); // For Queue Storage functions
//        // b.AddAzureStorageQueuesScaleForTrigger(); // Add this if scaling is needed for Queue Triggers
//    })
//    .Build();

//host.Run();

using CLDV6212_POE_Part1_st10152431.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.Xml;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register custom services for Azure storage
builder.Services.AddSingleton<TableService>();
builder.Services.AddSingleton<QueueService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<BlobService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // This line is crucial to serve static files

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();