namespace Project2;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public void ButtonClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (button.Text == "Clear")
        {
            DisplayLabel.Text = "0";
        }
        else if (button.Text == "Delete")
        {
            if (DisplayLabel.Text.Length <= 1)
            {
                DisplayLabel.Text = "0";
            } else
            {
                DisplayLabel.Text = DisplayLabel.Text.Substring(0, DisplayLabel.Text.Length - 1);
            }
        }
        else if (DisplayLabel.Text == "0")
        {
            if (button.Text == "0")
            {
                // Do nothing. Do not add continuous zeroes
            } else
            {
                // replace the leading 0
                DisplayLabel.Text = button.Text;
            }
        } 
        else
        {
            DisplayLabel.Text = DisplayLabel.Text + button.Text;
        }
        updateValues(DisplayLabel.Text);
    }

    private void updateValues(string displayText)
    {
        // create list to hold values, in the end the order will be {decimal, binary, octal, hex}
        List<string> values = new List<string>();
        // convert displayText to decimal to begin
        switch(InputPicker.SelectedItem)
        {
            case "Octal":
                values.Add(Convert.ToInt32(displayText, 8).ToString());
                break;
            case "Binary":
                values.Add(Convert.ToInt32(displayText, 2).ToString());
                break;
            case "Hexadecimal":
                values.Add(Convert.ToInt32(displayText, 16).ToString());
                break;
            case "Decimal":
                values.Add(displayText);
                break;
        }
        convertValues(ref values);
        DecimalLabel.Text = values[0];
        BinaryLabel.Text = values[1];
        OctalLabel.Text = values[2];
        HexLabel.Text = values[3];
    }

    private void convertValues (ref List<string> values)
    {
        // convert decimal to int
        int decimalNum = int.Parse(values[0]);
        // add binary
        values.Add(Convert.ToString(decimalNum, 2));
        // add octal
        values.Add(Convert.ToString(decimalNum, 8));
        // add hex
        values.Add(decimalNum.ToString("X"));
    }

    private void InputPicker_SelectedIndexChanged(Object sender, EventArgs e)
    {
        switch (InputPicker.SelectedIndex)
        {
            case 0:
                setButtonsToDecimal();
                break;
            case 1:
                setButtonsToHex();
                break;
            case 2:
                setButtonsToOctal();
                break;
            case 3:
                setButtonsToBinary();
                break;
        }
    }

    private void setButtonsToDecimal()
    {
        Grid grid = (Grid)this.FindByName("buttonGrid");
        foreach (var child in grid)
        {
            if (child is Button button)
            {
                if (button.Text == "Delete" || button.Text == "Clear")
                {
                    continue;
                }
                else if (char.IsDigit(button.Text[0]))
                {
                    enableButton(button);
                }
                else
                {
                    disableButton(button);
                }
            }
        }
    }

    private void setButtonsToOctal()
    {
        Grid grid = (Grid)this.FindByName("buttonGrid");
        foreach (var child in grid)
        {
            if (child is Button button)
            {
                if (button.Text == "Delete" || button.Text == "Clear")
                {
                    continue;
                }
                else if (char.IsDigit(button.Text[0]) && int.Parse(button.Text) <= 8)
                {
                    enableButton(button);
                }
                else
                {
                    disableButton(button);
                }
            }
        }
    }

    private void setButtonsToBinary()
    {
        Grid grid = (Grid)this.FindByName("buttonGrid");
        foreach (var child in grid)
        {
            if (child is Button button)
            {
                if (button.Text == "Delete" || button.Text == "Clear")
                {
                    continue;
                }
                else if (button.Text == "1" || button.Text == "0")
                {
                    enableButton(button);
                }
                else
                {
                    disableButton(button);
                }
            }
        }
    }

    private void setButtonsToHex()
    {
        Grid grid = (Grid)this.FindByName("buttonGrid");
        foreach (var child in grid)
        {
            if (child is Button button)
            {
                enableButton(button);
            }
        }
    }



    private void disableButton(Button disabledButton)
    {
        if (disabledButton.IsEnabled == true)
        {
            disabledButton.BackgroundColor = Colors.Gray;
            disabledButton.IsEnabled = false;
        }
    }

    private void enableButton(Button enabledButton)
    {
        if (enabledButton.IsEnabled == false)
        {
            enabledButton.BackgroundColor = Colors.Purple;
            enabledButton.IsEnabled = true;
        }
    }
}
