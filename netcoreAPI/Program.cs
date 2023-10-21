using netcoreAPI.Extensions;
using netcoreAPI.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//using extension for cammon settings
builder.Configure();
builder.Services.ConfigureDefaultServices(builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>());

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

//using extension for cammon settings
app.ConfigureAppBuilder();
app.ConfigureMinimal();

app.Run();
