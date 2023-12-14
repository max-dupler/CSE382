using System;
using System.Diagnostics.Metrics;
using SQLite;


namespace SymptomTracker;

[Table("symptom")]
public class Symptom
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
	public DateTime Date { get; set; }
    public int Intensity { get; set; }
    public TimeSpan Time { get; set; }
    public string Description { get; set; }
    public Color IntensityColor
    {
        get
        {
            switch (Intensity)
            {
                case int n when (n >= 1 && n <= 3):
                    return Colors.Green;

                case int n when (n >= 4 && n <= 7):
                    return Colors.Yellow;

                case int n when (n >= 8 && n <= 10):
                    return Colors.Red;

                default:
                    return Colors.Black;
            }
        }
    }

    public Symptom() { }

    public Symptom(string str)
    {
        string[] toks = str.Split(new char[] { '\t' });
        Date = DateTime.Parse(toks[0]);
        Intensity = int.Parse(toks[1]);
        Time = TimeSpan.Parse(toks[2]);
        Description = toks[3];
    }


    public override string ToString()
    {
        return string.Format("{0} {1} Time={2} {3} ({4})", Date.ToString("MM/dd/yyyy"), Intensity,
                                Time.ToString(), Description, ID);
    }

}

