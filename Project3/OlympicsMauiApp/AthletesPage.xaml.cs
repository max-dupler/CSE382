using System.Diagnostics.Metrics;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace OlympicsMauiApp;

public partial class AthletesPage : ContentPage
{
	public AthletesPage()
	{
		InitializeComponent();

		Sports.ItemsSource = (from s in DB.conn.Table<Participant2016Summer>()
							  select s.Sport).Distinct().OrderBy(sportName => sportName);
		Countries.ItemsSource = (from c in DB.conn.Table<Participant2016Summer>()
								 select c.Country).Distinct().OrderBy(countryName => countryName);
        loadEvents();

    }

    private void itemSelected(object sender, EventArgs e)
	{
        loadEvents();
        Athletes.ItemsSource = from a in DB.conn.Table<Participant2016Summer>().ToList()
                               where (a.Country.Equals(Countries.SelectedItem?.ToString()))
                                  && (a.Sport.Equals(Sports.SelectedItem?.ToString()))
                                  && (a.Event.Equals(Events.SelectedItem?.ToString()))
                               select a;    
    }

    private void loadEvents() {
        Events.ItemsSource = (from e in DB.conn.Table<Participant2016Summer>().ToList()
                              where e.Event.Contains(Sports.SelectedItem?.ToString())
                              select e.Event).Distinct().OrderBy(eventName => eventName);
    }

    async void ViewCell_Tapped(System.Object sender, System.EventArgs e)
    {
        ViewCell viewCellSeletected = sender as ViewCell;
        Participant2016Summer athlete = (Participant2016Summer)viewCellSeletected.BindingContext;

        var events = (from x in DB.conn.Table<Participant2016Summer>().ToList()
                          where x.Name.Equals(athlete.Name)
                          select x.Event).OrderBy(eventName => eventName).ToList();

        string message = string.Join("\n", events);
        await DisplayAlert(athlete.Name, message, "OK");
        
    }

}