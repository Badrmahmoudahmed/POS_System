using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS_System.Infrastructure.Contexts;

namespace POS_System
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddDbContext<POS_SystemDBContext>(o => 
            {
                o.UseSqlite(Path.Combine(FileSystem.AppDataDirectory, "POS.Db"));
                //o.EnableSensitiveDataLogging();
                //o.LogToConsole();

            });

#if DEBUG
    		builder.Logging.AddDebug();

#endif
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var dbcontext = provider.GetRequiredService<DbContext>();
                var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(MauiProgram));
                try
                {
                    dbcontext.Database.MigrateAsync().Wait();
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
