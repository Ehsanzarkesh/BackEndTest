using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using My.Backend.DB;
using My.Backend.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookDB>(Options=>
{
        Options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=KetabKhoneDB;Trusted_Connection=True");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/Book/add",(BookDB db,Book Books)=>{
db.Books.Add(Books);
db.SaveChanges();
});
app.MapPost("/Books/list",(BookDB db)=>
{
    return db.Books.ToList();
});
app.MapPost("/Book/update",(BookDB db,Book Books)=>
{
    db.Books.Update(Books);
    db.SaveChanges();
});
app.MapPost("/Books/remove/{Bookname}",(BookDB db,int id)=>
{
    var Books=db.Books.Find(id);
    if(Books!=null)
    {
        db.Books.Remove(Books);
        db.SaveChanges();
    }
    
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

