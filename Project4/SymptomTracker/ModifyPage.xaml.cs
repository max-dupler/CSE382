namespace SymptomTracker;

public partial class ModifyPage : ContentPage
{

    private Symptom selectedSymptom;

	public ModifyPage(Symptom symptom, bool modify)
	{
		InitializeComponent();

        selectedSymptom = symptom;

        if (modify)
        {
            addModifyButton.Text = "Update";
        } else
        {
            addModifyButton.Text = "Add";
        }

        if (symptom != null)
        {
            datePicker.Date = symptom.Date;
            timePicker.Time = symptom.Time;
            intensityPicker.SelectedItem = symptom.Intensity;
            noteEntry.Text = symptom.Description;
        }
    }

    public async void addModifyButton_Clicked(Object sender, EventArgs e)
    {

        if (addModifyButton.Text == "Add")
        {
            Symptom s = new Symptom($"{datePicker.Date.ToString()}\t{intensityPicker.SelectedItem.ToString()}" +
                $"\t{timePicker.Time.ToString()}\t{noteEntry.Text}");
            DB.conn.Insert(s);
        } else if (addModifyButton.Text == "Update")
        {
            selectedSymptom.Date = datePicker.Date;
            selectedSymptom.Time = timePicker.Time;
            selectedSymptom.Intensity = int.Parse(intensityPicker.SelectedItem.ToString());
            selectedSymptom.Description = noteEntry.Text;

            DB.conn.Update(selectedSymptom);
        }
        await Navigation.PopModalAsync();
    }

    async void cancelButton_Clicked(Object sender, EventArgs e)
    {
        bool cancelConfirm = await DisplayAlert("Warning", "Do you wish to discard this?", "Yes", "No");
        if (cancelConfirm)
        {
            await Navigation.PopModalAsync();
        }
    }
}
