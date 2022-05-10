using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add http client (to connect to a repository)
builder.Services.AddHttpClient("BibliotekaDB", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:7051");

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "BibliotekaOnline");
});

// Use CORS for connections
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("BibliotekaCorsPolicy", options => options.WithOrigins("http://localhost:3001"));
});

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:3001"));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/pozycje", async (IHttpClientFactory clientFactory, IOptions<JsonOptions> options, string? s) =>
{
    if (clientFactory is null)
    {
        return Results.Problem("Server error", statusCode: 500);
    }

    var httpClient = clientFactory?.CreateClient("BibliotekaDB");

    var q = string.IsNullOrEmpty(s) ? "" : "?s=" + s;

    var response = await httpClient.GetAsync("/pozycje" + q);
    if (response.IsSuccessStatusCode)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();
        var pozycje = await JsonSerializer.DeserializeAsync<IEnumerable<Pozycja>>(contentStream, options.Value.SerializerOptions);

        if (pozycje is not null)
        {
            return Results.Ok(pozycje);
        }
        return Results.Problem("Error", statusCode: 500);
    }
    return Results.Problem("Error", statusCode: 500);
})
.WithName("GetWeatherForecast")
.Produces(200)
.ProducesProblem(500);

app.Run("http://localhost:5001");
