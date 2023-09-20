using APITest;
using DashScope.SDK.Net6;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder().
    AddJsonFile("appsettings.json").
    Build();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    foreach (FieldInfo fileld in typeof(ApiVersionInfo).GetFields())
    {
        option.SwaggerDoc(fileld.Name, new OpenApiInfo { Title = "≤‚ ‘Api", Version = fileld.Name, Description = $"API{fileld.Name}Œƒµµµƒ√Ë ˆ" });
    }
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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
app.UseSwaggerUI(
    c =>
    {
        foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
        {
            c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
        }
        //c.SwaggerEndpoint("/swagger/v1/swagger.json", "≤‚ ‘API"); 
    }
    );

app.UseHttpsRedirection();
app.UseCors("DashScopeAPI");
app.UseAuthorization();

app.MapControllers();

app.Run();
