
using Digital.Data.Data;
using Microsoft.AspNetCore.Hosting;
using Digital_Signature.Api.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/*var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Staging,
    WebRootPath = "customwwwroot"
});*/

/*static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Program>());*/

// migrate any database changes on startup (includes initial db creation)


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.ConfigCors();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddBusinessService();
builder.Services.AddAutoMapper();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.BufferBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();


app.Services.ApplyPendingMigrations();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDBContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.MapControllers();

app.Run();
