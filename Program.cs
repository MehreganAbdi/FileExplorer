using FileExplorer.Controllers;
using FileExplorer.Data;
using FileExplorer.IService;
using FileExplorer.Services;
using FileExplorer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FileExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            #region DI

            //Services DI

            builder.Services.AddScoped<IDirectoryService, DirectoryService>();
            builder.Services.AddScoped<IDataTranformerService, DataTransformerService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            #endregion



            builder.Services.AddDbContext<FileExplorerDbContext>(
               options =>
               {
                   options.UseSqlServer(builder.Configuration.GetConnectionString("FileExplorerDefaultConnection"));
               });




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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}