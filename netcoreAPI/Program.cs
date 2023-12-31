using netcoreAPI.Extensions;
using netcoreAPI.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//using extension for cammon settings
builder.ConfigureWebBuilder();
builder.Services.ConfigureDefaultServices(builder.Configuration.GetSection("JwtSettings").Get<JwtOption>(), builder.Configuration.GetSection("EnviromentSettings").Get<EnviromentOption>());

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

//using extension for cammon settings
app.ConfigureAppBuilder();
app.ConfigureMinimal();

app.Run();
