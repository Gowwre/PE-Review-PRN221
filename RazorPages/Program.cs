using DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages {
    public class Program {
        public static void Main(string[] args) {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            
            builder.Services.AddScoped<IHrAccountRepository, HrAccountRepository>();
            builder.Services.AddScoped<ICandidateProfileRepository, CandidateProfileRepository>();
            builder.Services.AddScoped<IJobPostingRepository, JobPostingRepository>();

            Action<RazorPagesOptions> setupAction = options => options.Conventions.AddPageRoute("/Login/Index", "/");
            builder.Services.AddRazorPages().AddRazorPagesOptions(
                setupAction);

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}