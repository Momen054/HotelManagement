using HotelManagement.Middelware;
using HotelManagement.Models;
using HotelManagement.Services;
using HotelManagement.Options;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using HotelManagement.Data;
using HotelManagement.Mapping;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HotelManagementContext") ?? 
    throw new InvalidOperationException("Connection string 'HotelManagementContext' not found.");

builder.Services.AddDbContext<HotelManagementContext>
    (options => options.UseSqlServer(connectionString));



//services
builder.Services.AddScoped<I_Invoice, InvoiceService>();
builder.Services.AddScoped<IPayment, PaymentService>();
builder.Services.AddScoped<IReservation, Reservation_Service>();
builder.Services.AddScoped<IResRom, ResRomService>();
builder.Services.AddScoped<IResSer, ResSerService>();
builder.Services.AddScoped<IReview, ReviewService>();
builder.Services.AddScoped<IRoom, RoomService>();
builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthentication, AuthenticationService>();
builder.Services.AddScoped<IGoogleService, GoogleLoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoomType, RoomTypeService>();
builder.Services.AddScoped<IService, Service_ser>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));



builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
                {
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireNonAlphanumeric = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireUppercase = true;
                    opt.User.RequireUniqueEmail = true;
                }
                )
                .AddEntityFrameworkStores<HotelManagementContext>()
                .AddDefaultTokenProviders();


var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOption>();
builder.Services.AddSingleton(jwtOptions);

var googleOptions = builder.Configuration.GetSection("Google").Get<GoogleOption>();
builder.Services.AddSingleton(googleOptions);

builder.Services.AddAuthentication()
                .AddJwtBearer( opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))

                    };
                }
                ).AddGoogle(opt =>
                {
                    opt.ClientId = googleOptions.ClientId;
                    opt.ClientSecret = googleOptions.ClientSecret;
                    opt.SignInScheme = IdentityConstants.ExternalScheme;

                });


var EmailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettingOption>();
builder.Services.AddSingleton(EmailSettings);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedPolicy", option =>
    {
        option.PermitLimit = 10;
        option.Window = TimeSpan.FromMinutes(1);
        option.QueueLimit = 2;
        option.QueueProcessingOrder =
            QueueProcessingOrder.OldestFirst;
    });
    options.RejectionStatusCode =
        StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseMiddleware<MaintenanceMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
