using Microsoft.Maui.Graphics;
using Graphics;
using Graphics.Drawables;
namespace NHLTrack;

public partial class PlayerPage : ContentPage
{
    private static string StatsEndpoint = "https://api-web.nhle.com/v1/player";
	private RestService _RestService;
	private PlayerStats _playerStats;
	public PlayerPage(Player player)
	{
		InitializeComponent();
		Title = player.FullName;
		_RestService = new RestService();
		GetStats(player.Id);

	}

    public async void GetStats(int id)
	{
		string query = $"{StatsEndpoint}/{id}/landing";
		_playerStats = await _RestService.GetStats(query);
        BindingContext = _playerStats;
		StatGraph();
    }

	public void StatGraph()
	{
        PlottingDrawable plot = (PlottingDrawable)statGraphic.Drawable;
		plot.Labels = new string[] { "Games Played", "Goals", "Assists", "Points" };
		plot.Data = new int[] {_playerStats.FeaturedStats.RegularSeason.SubSeason.GamesPlayed,
								_playerStats.FeaturedStats.RegularSeason.SubSeason.Goals,
								_playerStats.FeaturedStats.RegularSeason.SubSeason.Assists,
								_playerStats.FeaturedStats.RegularSeason.SubSeason.Points};
		statGraphic.Invalidate();
    }
}
