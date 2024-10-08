using Microsoft.EntityFrameworkCore;
using PinewoodTechnologies.API.Data;
using PinewoodTechnologies.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

//----------------------------
// Added by Dave Lau for demo
// Configure DbContext with SQLite using the connection string
builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
//----------------------------

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//-------------------------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//----------------------------------------------------
// Added by Dave Lau for demo
// Use custom API Key middleware
app.UseMiddleware<ApiKeyMiddleware>();
//----------------------------------------------------

app.Run();