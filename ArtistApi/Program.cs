
using ArtistApi.Clients;
using ArtistApi.Models;
using System.Net.Http.Headers;

namespace ArtistApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<IArtistRepo, ArtistRepo>();
            builder.Services.AddHttpClient<ISpotifyClient, SpotifyClient>(client =>
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "BQBHT33kCnS_6oGx87EzPfKtKRUPmEpfDiR9TpTYT3UqTOkwh1DpRuAl5q6QgbP9NqJyZcKq2xvhIgtyP81nPJsbQyYPQi1_Jn0FnDr8SvyQVURj9FYdF4RvglH_E1IxyLuoJBazaSjW");
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
