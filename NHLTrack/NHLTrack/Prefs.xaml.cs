using System.Collections.ObjectModel;

namespace NHLTrack;

public partial class Prefs : ContentPage
{
    ReadOnlyCollection<TimeZoneInfo> tz;
    public Prefs()
    {

        InitializeComponent();

        List<string> teamNames = (from t in DB.conn.Table<Team>()
                                  select t.Name).Distinct().OrderBy(t => t).ToList();
        teamNames.Insert(0, "none");
        teamPicker.ItemsSource = teamNames;

        tz = TimeZoneInfo.GetSystemTimeZones();

        timeZonePicker.ItemsSource = tz.Select(t => t.StandardName).Distinct().ToList();
    }
    void teamPicker_SelectedIndexChanged(Object sender, EventArgs e)
    {
        Preferences.Set("FavoriteTeam", teamPicker.SelectedItem.ToString());
        Console.WriteLine(Preferences.Get("FavoriteTeam", "NHL"));
    }

    void timeZonePicker_SelectedIndexChanged(Object sender, EventArgs e)
    {
        foreach(var t in tz)
        {
            if (t.StandardName == timeZonePicker.SelectedItem.ToString())
            {
                Preferences.Set("TimeZone", t.Id);
                Console.WriteLine(Preferences.Get("TimeZone", "invalid"));
                break;
            }
        }
    }
}
