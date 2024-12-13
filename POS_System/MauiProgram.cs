using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS_System.Infrastructure;
using POS_System.Infrastructure.Contexts;
using POS_System.Interfaces;
using System.Diagnostics;


namespace POS_System
{
    public static class MauiProgram
    {
        public static  MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Oswald-Regular.ttf", "Oswald-Regular");
                    fonts.AddFont("Oswald-Bold.ttf", "Oswald-Bold.ttf");
                });
            builder.Services.AddDbContext<POS_SystemDBContext>(o => 
            {
                o.UseSqlite($"Data Source={Path.Combine(FileSystem.AppDataDirectory, "POS.Db")}");
                //o.EnableSensitiveDataLogging();
                //o.LogToConsole();
                Debug.WriteLine($"{Path.Combine(FileSystem.AppDataDirectory, "POS.Db")}");

            });
            builder.Services.AddScoped<IUntiofWork, Unitofwork>();

#if DEBUG
    		builder.Logging.AddDebug();

#endif
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var dbcontext = provider.GetRequiredService<POS_SystemDBContext>();
                var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(MauiProgram));
                try
                {
                    dbcontext.Database.EnsureCreated();
                    SeedData.Seed(dbcontext);
                }
                catch (Exception ex)
                {

                    logger.LogError(ex.Message,"there is an Error During Apply The Migration");
                }
            }
            
                return app;
        }
    }
}
