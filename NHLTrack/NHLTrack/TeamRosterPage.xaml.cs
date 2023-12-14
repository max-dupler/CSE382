namespace NHLTrack;

public partial class TeamRosterPage : ContentPage
{
	public TeamRosterPage(List<Player> players, string teamName)
	{
		InitializeComponent();
        teamLabel.Text = teamName;
		rosterLV.ItemsSource = players;
		this.Title = teamName;

		foreach(var p in players)
		{
            Console.WriteLine(p.ToString());
        }
	}

    void rosterLV_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
		Player player = (Player)e.Item;
		PlayerPage playerPage = new PlayerPage(player);
		Navigation.PushAsync(playerPage);
    }
}
