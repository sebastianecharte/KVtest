using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using KVtest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

SecretClientOptions options = new SecretClientOptions()
{
    Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
};

var client = new SecretClient(new Uri("https://kvboveda.vault.azure.net/"), new DefaultAzureCredential(), options);

string secretValue;
try
{
    KeyVaultSecret secret = client.GetSecret("elsecreto");

    secretValue = secret.Value;
}
catch (Exception ex)
{
    secretValue = "ERROR: " + ex.Message;
}



//string elTexto = "El texto 3";

UnServicio unServicio = new(secretValue);

builder.Services.AddSingleton(unServicio);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
