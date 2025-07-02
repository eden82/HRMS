var builder = WebApplication.CreateBuilder(args);

// CORS for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowFrontend");
//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Login endpoint
app.MapPost("/api/login", (UserLogin login) =>
{
    if (login.Username == "admin" && login.Password == "1234")
    {
        return Results.Ok(new { message = "Login successful", token = "fake-jwt-token" });
    }
    else
    {
        return Results.Unauthorized();
    }
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();


// Place all record declarations AFTER app.Run()

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record UserLogin(string Username, string Password);
