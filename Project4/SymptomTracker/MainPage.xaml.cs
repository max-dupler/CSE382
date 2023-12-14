namespace SymptomTracker;
using System;
using System.Diagnostics;
using System.Collections.ObjectModel;

public partial class MainPage : ContentPage {

	public MainPage() {
		InitializeComponent();

        loadSymptoms();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        loadSymptoms();
    }

    void sortByDate_CheckedChanged(Object sender, CheckedChangedEventArgs e)
    {
        sortByIntesity.IsChecked = false;
        loadSymptoms();
    }

    void sortByIntesity_CheckedChanged(Object sender, CheckedChangedEventArgs e)
    {
        sortByDate.IsChecked = false;
        loadSymptoms();
    }

    async void addButton_Clicked(Object sender, EventArgs e)
    {
        ModifyPage modifyPage = new ModifyPage(null, false);

        await Navigation.PushModalAsync(modifyPage);
    }

    public void loadSymptoms()
    {
        if (sortByIntesity.IsChecked)
        {
            symptomLV.ItemsSource = (from s in DB.conn.Table<Symptom>()
                                     select s).OrderBy(s => s.Intensity);
        } else if (sortByDate.IsChecked)
        {
            symptomLV.ItemsSource = (from s in DB.conn.Table<Symptom>()
                                     select s).OrderBy(s => s.Date);
        }
    }

    async void symptomLV_ItemSelected(Object sender, SelectedItemChangedEventArgs e)
    {
        string choice = await DisplayActionSheet("Color", "Cancel", null,
                                    "Modify Record", "Delete Record");

        if (choice == "Modify Record")
        {
            if (e.SelectedItem is Symptom selectedSymptom)
            {
                ModifyPage modifyPage = new ModifyPage(selectedSymptom, true);
                await Navigation.PushModalAsync(modifyPage);
            }
        } else if (choice == "Delete Record")
        {
            if (e.SelectedItem is Symptom selectedSymptom)
            {
                bool deleteConfirmed = await DisplayAlert("Delete Record", "Are you sure you want to delete this record?", "Yes", "No");

                if (deleteConfirmed) {
                    DB.conn.Delete(selectedSymptom);
                    loadSymptoms();
                    await DisplayAlert("Record Deleted", "The record has been deleted.", "OK");

                }
            }
        }

    }
}

