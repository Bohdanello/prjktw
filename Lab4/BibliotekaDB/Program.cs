var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BazaDanych>();
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("BibliotekaDBCorsPolicy", options => options.WithOrigins("http://localhost:5001"));
});

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:5001"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<BazaDanych>();
    if (db is null)
    {
        throw new Exception("db is not loaded.");
    }
    db.Seed();
}

app.MapGet("/pozycje", (string? s, BazaDanych db) =>
{
    if (string.IsNullOrEmpty(s))
    {
        return db.Pozycje;
    }

    return db.Szukaj(s);
})
.Produces(200);

app.Run("http://localhost:7051");
