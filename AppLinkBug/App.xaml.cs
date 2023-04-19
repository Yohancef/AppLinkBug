namespace AppLinkBug;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

#if IOS
        (Application.Current as IApplicationController)?.SetAppIndexingProvider(new Microsoft.Maui.Controls.Compatibility.Platform.iOS.IOSAppIndexingProvider());
#endif
        try
        {
            var entry = new AppLinkEntry
            {
                Title = "Find Shop",
                Description = "Find Shop Deep Link",
                AppLinkUri = new Uri("https://popupshop.azurewebsites.net/findshop", UriKind.RelativeOrAbsolute),
                IsLinkActive = true
            };

            Application.Current.AppLinks.RegisterLink(entry);
        }
        catch (Exception ex)
        {
            throw new Exception("Deeplink failed.", ex);
        }

    }
 
    protected async override void OnAppLinkRequestReceived(Uri uri)
    {
        await Current.MainPage.DisplayAlert("AppLink", "You are linking!", "OK");
    }
}