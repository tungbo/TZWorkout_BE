using TunzWorkout.Infrastructure;
using TunzWorkout.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TunzWorkout.Domain.Enums;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRole.Admin.ToString(), p => p.RequireClaim("Role", nameof(UserRole.Admin)));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 104857600;
});

//CORS
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policybuilder => {
        policybuilder
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
        .WithHeaders("Authorization", "Content-Type", "origin", "accept")
        .WithMethods("GET", "POST", "PUT", "DELETE");
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.

var uploadsPath = Path.Combine(
    Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
    "TunzWorkout.Infrastructure", 
    "Uploads"
);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads" // URL public
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
