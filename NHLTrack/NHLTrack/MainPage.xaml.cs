using System.Collections.ObjectModel;

namespace NHLTrack;

public partial class MainPage : ContentPage
{
	RestService _RestService;
	ObservableCollection<Game> Scores;
	public static string NHLEndpoint = "https://api-web.nhle.com/v1/";

    public MainPage()
	{
		InitializeComponent();
		_RestService = new RestService();
		Scores = new ObservableCollection<Game>();
		scoreDate.Date = DateTime.Today;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		GetScores();
        scoresLV.ItemsSource = Scores;
    }

    public async void GetScores()
	{
		string date = scoreDate.Date.ToString("yyyy-MM-dd");

        Scores.Clear();
		string requesturi = NHLEndpoint;
		requesturi += $"schedule/{date}";
		ScoreData data = await _RestService.GetScores(requesturi);
		if (data != null)
		{
			foreach (var gameDay in data.GameWeek)
			{
				if (gameDay.Date.Equals(date)) {
					foreach (var game in gameDay.Games)
					{
						Scores.Add(game);
					}
				}
			}
		}
	}

    void scoreDate_DateSelected(System.Object sender, Microsoft.Maui.Controls.DateChangedEventArgs e)
    {
		GetScores();
    }

	void scoresLV_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
	{
		var x = scoresLV.SelectedItem;
	}
}


