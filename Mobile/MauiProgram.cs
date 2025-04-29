using Microsoft.Extensions.Logging;
using Mobile.Handlers;
using CommunityToolkit.Maui;
using MapboxMaui;
using The49.Maui.BottomSheet;

namespace Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMapbox("sk.eyJ1IjoibmEyYXhsIiwiYSI6ImNtOXp2czk0ajAxa2UybXNhb2xxdTU0eGIifQ.w8o_emcHQOXUIpHHfUt79w")
            .UseBottomSheet()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        FormHandler.RemoveBorders();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}