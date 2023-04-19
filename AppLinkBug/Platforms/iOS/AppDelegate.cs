using Foundation;
using UIKit;

namespace AppLinkBug;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    [Export("application:openURL:options:")]
    public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
    {
        Microsoft.Maui.Controls.Application.Current.SendOnAppLinkRequestReceived(url);
        return true;
    }
}
