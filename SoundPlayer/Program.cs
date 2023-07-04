using Domain.Models;
using Services.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;
using Services.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Repositories;
using SoundPlayer.Hubs;

namespace SoundPlayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
               options.Filters.Add(typeof(NotImplExceptionFilterAttribute)));

            builder.Services.AddSignalR();

            #region Injections

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ILikeOrDislikeService, LikeOrDislikeService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddScoped<ISoundService, SoundService>();
            builder.Services.AddScoped<IUploadSoundService, UploadSoundService>();
            builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
            builder.Services.AddScoped<IFavoriteSoundsService, FavoriteSoundsService>();
            builder.Services.AddScoped<ICacheSoundsService, CacheSoundsService>();

            builder.Services.AddScoped<ISoundRepository, SoundRepository>();
            builder.Services.AddScoped<ILikeOrDislikeRepository, LikeOrDislikeRepository>();
            builder.Services.AddScoped<IFavoriteSoundsRepository, FavoriteSoundsRepository>();

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(connection ,b => b.MigrationsAssembly("SoundPlayer")));

            #endregion

            #region Identity
            builder.Services.AddIdentity<User, IdentityRole<string>>(options => options.SignIn.RequireConfirmedAccount = true)
           .AddEntityFrameworkStores<ApplicationContext>()
           .AddUserStore<UserStore<User,
                IdentityRole<string>,
                ApplicationContext, 
                string, IdentityUserClaim<string>, 
                IdentityUserRole<string>,
                IdentityUserLogin<string>, 
                IdentityUserToken<string>,
                IdentityRoleClaim<string>>>()
           .AddRoleStore<RoleStore<IdentityRole<string>,
               ApplicationContext, 
               string, IdentityUserRole<string>, 
               IdentityRoleClaim<string>>>(); ;

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = false;

                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedAccount = false;
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}");

            app.MapHub<ChatHub>("/Hubs/ÑhatHub");

            app.Run();
        }
    }
}