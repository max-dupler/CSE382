using SQLite;
namespace NHLTrack;

[Table("team")]
public class Team
{
	public Team() { }
	[PrimaryKey, AutoIncrement]
	public int ID { get; set; }
	public string Name { get; set; }
	public string Abbreviation { get; set; }
	public string Primary { get; set; }
	public string  Secondary { get; set; }
    public string TeamLogo
    {
        get
        {
            return $"../Resources/Images/Teams/{Abbreviation}.png"; 
        }
    }

    public Team(string teamInfo)
	{
		string[] toks = teamInfo.Split(new char[] { ',' });
		Name = toks[0];
		Abbreviation = toks[1];
		Primary = toks[2];
		Secondary = toks[3];
	}
}
