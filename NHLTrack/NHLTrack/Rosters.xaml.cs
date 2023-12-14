using System.Collections.ObjectModel;

namespace NHLTrack;

public partial class Rosters : ContentPage
{
	public static string RosterEndpoint = "https://api-web.nhle.com/v1/roster";
	RestService _RestService;
	List<Player> Players;

    public Rosters()
    {
        InitializeComponent();
        _RestService = new RestService();

        teamsLV.ItemsSource = (from t in DB.conn.Table<Team>()
							   select t).Distinct().OrderBy(t => t.Name).ToList();
	}

    public async Task<List<Player>> GetPlayers(string team)
	{
		Players = new List<Player>();
		string requesturi = RosterEndpoint;
		requesturi += $"/{team}/20232024";
		PlayerData data = await _RestService.GetPlayers(requesturi);
		if (data != null)
		{
			_processPlayers(data.Forwards);
			_processPlayers(data.Defensemen);
			_processPlayers(data.Goalies);
		}

		return Players;
	}

	private void _processPlayers(List<Player> players)
	{
		foreach (var p in players)
		{
			Players.Add(p);
		}
	}

    async void teamsLV_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
		Team selectedTeam = (Team)e.Item;
		List<Player> playerList = await GetPlayers(selectedTeam.Abbreviation);
		TeamRosterPage teamRosterPage = new TeamRosterPage(Players.ToList(), selectedTeam.Name);
		await Navigation.PushAsync(teamRosterPage);
    }
}
