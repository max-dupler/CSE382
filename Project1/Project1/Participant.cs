using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project1;
public class Participant {
	public enum MedalType { None, Bronze, Silver, Gold };
	public enum SeasonType { Summer, Winter };
	public enum GenderType { Female, Male };


	public int ID { get; set; }
	public String Name { get; set; }
	public GenderType Gender { get; set; }
	public int Age { get; set; }
	public String Country { get; set; }
	public int Year { get; set; }
	public SeasonType Season { get; set; }
	public String Location { get; set; }
	public String Sport { get; set; }
	public String Event { get; set; }
	public MedalType Medal { get; set; }

	public Participant(String str) {
		string[] toks = str.Split(new char[] { '\t' });

        ID = int.Parse(toks[0]);
        Name = toks[1];
        Gender = (toks[2] == "M") ? GenderType.Male : GenderType.Female;
        if (int.TryParse(toks[3], out int ageValue))
        {
            Age = ageValue;
        }
        else
        {
            Age = -1;
        }
        Country = toks[6];
        Year = int.Parse(toks[9]);
        Season = (toks[10] == "Summer") ? SeasonType.Summer : SeasonType.Winter;
        Location = toks[11];
        Sport = toks[12];
        Event = toks[13];

        string medalValue = toks[14];
        if (medalValue == "Bronze")
        {
            Medal = MedalType.Bronze;
        }
        else if (medalValue == "Silver")
        {
            Medal = MedalType.Silver;
        }
        else if (medalValue == "Gold")
        {
            Medal = MedalType.Gold;
        }
        else
        {
            Medal = MedalType.None;
        }
    }
}
