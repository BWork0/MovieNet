using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Repositories;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Application.Mediator;
using MovieNet.Application.Queries;
using MovieNet.Infrastructure;
using MovieNet.Infrastructure.Repositories;
using MovieNet.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieNetContext>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IWatchlistService, WatchlistService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IMovieSearchService, MovieSearchService>();

builder.Services.AddTransient<IMediator, Mediator>();

builder.Services.AddTransient<ICommandHandler<CreateMovieCommand>, CreateMovieCommandHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteMovieCommand>, DeleteMovieCommandHandler>();
builder.Services.AddTransient<ICommandHandler<UpdateMovieCommand>, UpdateMovieCommandHandler>();

builder.Services.AddTransient<ICommandHandler<CreateWatchlistEntryCommand>, CreateWatchlistEntryCommandHandler>();
builder.Services.AddTransient<ICommandHandler<RemoveWatchlistEntryCommand>, RemoveWatchlistEntryCommandHandler>();
builder.Services.AddTransient<ICommandHandler<UpdateWatchlistEntryCommand>, UpdateWatchlistEntryCommandHandler>();


builder.Services.AddTransient<ICommandHandler<DeleteMovieCommand>, DeleteMovieCommandHandler>();
builder.Services.AddTransient<ICommandHandler<UpdateMovieCommand>, UpdateMovieCommandHandler>();

builder.Services.AddTransient<ICommandHandler<CreateReviewCommand>, CreateReviewCommandHandler>();



builder.Services.AddTransient<IQueryHandler<GetMoviesQuery, List<MovieDto>>, GetMoviesQueryHandler>();
builder.Services.AddTransient<IQueryHandler<GetUserWatchlistQuery, List<WatchlistEntryDto>>, GetUserWatchlistQueryHandler>();
builder.Services.AddHttpContextAccessor();
var opt = new DbContextOptionsBuilder()
    .UseSqlite(connectionString)
    .Options;

using (var db = new MovieNetContext())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? "TODO")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
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
