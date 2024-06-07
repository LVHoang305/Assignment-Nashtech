using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Services;
using System.Text.Json.Serialization;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Repository.BookRepository;
using LibraryManagement.Repository.CategoryRepository;
using LibraryManagement.Repository.BookBorrowingRequestDetailsRepository;
using LibraryManagement.Repository.BookBorrowingRequestRepository;
using LibraryManagement.Repository.BookCategoryRepository;
using LibraryManagement.Repository.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<LibraryManagementContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
                       .UseLazyLoadingProxies());
            // Add services to the container.

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperUserPolicy", policy =>
                    policy.RequireClaim("SuperUser", "True"));
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders("Authorization");
                });
            });

            builder.Services.AddControllers();
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddScoped<JwtService>();

            builder.Services.AddScoped<IBookService<BookDTO, BookCreateDTO>, BookService>();
            builder.Services.AddScoped<IBaseRepository<Book>, BookRepository>();

            builder.Services.AddScoped<ICategoryService<CategoryDTO, CategoryCreateDTO>, CategoryService>();
            builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();

            builder.Services.AddScoped<IBookCategoryService<BookCategoryDTO, BookCategoryCreateDTO>, BookCategoryService>();
            builder.Services.AddScoped<IBaseRepository<BookCategory>, BookCategoryRepository>();

            builder.Services.AddScoped<IBookBorrowingRequestService<BookBorrowingRequestDTO, BookBorrowingRequestCreateDTO>, BookBorrowingRequestService>();
            builder.Services.AddScoped<IBaseRepository<BookBorrowingRequest>, BookBorrowingRequestRepository>();

            builder.Services.AddScoped<IBookBorrowingRequestDetailsService<BookBorrowingRequestDetailsDTO, BookBorrowingRequestDetailsCreateDTO>, BookBorrowingRequestDetailsService>();
            builder.Services.AddScoped<IBaseRepository<BookBorrowingRequestDetails>, BookBorrowingRequestDetailsRepository>();

            builder.Services.AddScoped<IUserService<UserDTO, UserCreateDTO>, UserService>();
            builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();

            builder.Services.AddControllers().AddJsonOptions(x =>
                  x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.UseCors("AllowOrigin");

            app.MapControllers();

            app.Run();
        }
    }
}
