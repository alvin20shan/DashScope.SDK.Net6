using DashScope.SDK.Net6;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder().
    AddJsonFile("appsettings.json").
    Build();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "≤‚ ‘Api", Version = "v1", Description = "APIŒƒµµµƒ√Ë ˆ" }); 
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("DashScopeAPI",
        builder =>
        {
            builder.SetIsOriginAllowed(d => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});


builder.Services.AddScoped(d =>
{
    string apikey = configuration["Apikey"];
    return new QwenClient(apikey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseSwagger();
app.UseSwaggerUI(c => {

    c.SwaggerEndpoint("/swagger/v1/swagger.json", "≤‚ ‘API");
    c.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseCors("DashScopeAPI");
app.UseAuthorization();

app.MapControllers();

app.Run();
