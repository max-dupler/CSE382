namespace NHLTrack;

public partial class App : Application
{
	public App()
	{
        DB.InitDB();
        InitializeComponent();

        if (!Preferences.ContainsKey("FavoriteTeam"))
        {
            Preferences.Set("FavoriteTeam", "NHL");
        }
        if (!Preferences.ContainsKey("TimeZone"))
        {
            Preferences.Set("TimeZone", TimeZoneInfo.Local.Id.ToString());
        }

        MainPage = new AppShell();
	}
}

