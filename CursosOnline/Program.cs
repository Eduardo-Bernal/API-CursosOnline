using CursosOnline.Applications.Autenticacao;
using CursosOnline.Applications.Services;
using CursosOnline.Contexts;
using CursosOnline.Interface;
using CursosOnline.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<CursosOnlineContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(default)));

builder.Services.AddScoped<IInstrutorRepository, InstrutorRepository>();
builder.Services.AddScoped<InstrutorService>();

//builder.Services.AddScoped<ICursoRepository, CursoRepository>();
//builder.Services.AddScoped<CursoService>();

builder.Services.AddScoped<GeradorTokenJwt>();
builder.Services.AddScoped<AutenticacaoService>();

//builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
//builder.Services.AddScoped<AlunoService>();

//builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
//builder.Services.AddScoped<MatriculaService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Adiciona o suporte para autenticação usando JWT.
    .AddJwtBearer(options =>
    {

        var chave = builder.Configuration["Jwt:Key"]!;


        var issuer = builder.Configuration["Jwt:Issuer"]!;


        var audience = builder.Configuration["Jwt:Audience"]!;


        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true,


            ValidateAudience = true,


            ValidateLifetime = true,


            ValidateIssuerSigningKey = true,


            ValidIssuer = issuer,


            ValidAudience = audience,


            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave)
            )
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
