using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;

namespace AppLinkBug;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCompatibility()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureLifecycleEvents(events =>
            {
#if IOS
                    if (Version.Parse(UIKit.UIDevice.CurrentDevice.SystemVersion) > Version.Parse("12.0"))
                    {
                        events.AddiOS(ios => ios
                            .SceneOpenUrl((parameter1, parameter2) => LogEvent(parameter1, parameter2)));

                        static bool LogEvent(UIKit.UIScene scene, Foundation.NSSet<UIKit.UIOpenUrlContext> context)
                        {
                            var url = context.ToArray().First().Url;
                            Microsoft.Maui.Controls.Application.Current.SendOnAppLinkRequestReceived(url);
                            return true;
                        }
                    }
                    else
                    {
                        events.AddiOS(ios => ios
                            .OpenUrl((app, url, opion) => LogEvent2(app, url, opion)));

                        static bool LogEvent2(UIKit.UIApplication application, Foundation.NSUrl url, Foundation.NSDictionary options)
                        {
                            Microsoft.Maui.Controls.Application.Current.SendOnAppLinkRequestReceived(url);
                            return true;
                        }
                    }                   
#endif
            });       

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
