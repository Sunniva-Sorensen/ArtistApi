using ArtistApi.Clients;
using ArtistApi.Models;
using System.Net.Http.Headers;
using Azure.Identity;

namespace ArtistApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var keyVaultUrl = new Uri("https://kv-brights-common-01.vault.azure.net/");
            builder.Configuration.AddAzureKeyVault(
                keyVaultUrl,
                new DefaultAzureCredential()
            );

            var spotifyToken = builder.Configuration["spotify-token"];

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<IArtistRepo, ArtistRepo>();

            builder.Services.AddHttpClient<ISpotifyClient, SpotifyClient>(client =>
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", spotifyToken);
            });

            var app = builder.Build();

            app.MapGet("/test-secret", (IConfiguration config) =>
            {
                var token = config["spotify-token"];
                return token ?? "Secret not found";
            });

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
