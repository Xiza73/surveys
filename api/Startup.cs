using Microsoft.EntityFrameworkCore;
using manuel_fajardo_prueba_tecnica.Services;
using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;

public class Startup
{
  private readonly IConfiguration _configuration;

  public Startup(IConfiguration configuration)
  {
    _configuration = configuration;
  }


  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    app.UseCors("AllowAllOrigins");
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
    });
  }
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddCors(options =>
    {
      options.AddPolicy("AllowAllOrigins",
          builder =>
          {
            builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
          });
    });
    services.AddControllersWithViews();
    services.AddDbContext<DbRetailContext>(options =>
        options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
    services.AddScoped<ISurveyService, SurveyService>();
    services.AddScoped<IPersonService, PersonService>();
    services.AddScoped<IQuestionService, QuestionService>();
    services.AddScoped<IBranchService, BranchService>();
  }
}