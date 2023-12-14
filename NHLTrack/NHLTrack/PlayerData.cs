using System;
using Newtonsoft.Json;
namespace NHLTrack;

public class FirstName
{
	public string Default { get; set; }
}

public class LastName
{
	public string Default { get; set; }
}
public class BirthCity
{
	public string Default { get; set; }
}

public class BirthStateProvince
{
	public string Default { get; set; }
}


public class Player
{
	public int Id { get; set; }
	public string Headshot { get; set; }
	public FirstName FirstName { get; set; }
	public LastName LastName { get; set; }
	public string FullName
	{
		get
		{
			return $"{FirstName.Default} {LastName.Default}";
		}
	}
	[JsonProperty("sweaterNumber")]
	public int Number { get; set; }
	private string _position;
    [JsonProperty("PositionCode")]
    public string Position
	{
		set
		{
			if (value == "R")
			{
				_position = "RW";
			} else if (value == "L")
			{
				_position = "LW";
			} else
			{
				_position = value;
			}
		}
		get
		{
			return _position;
		}
	}
	[JsonProperty("shootsCatches")]
	public string Handedness { get; set; }
	private string _height;
	[JsonProperty("heightInInches")]
	public string Height
	{
		set
		{
			int h = int.Parse(value);
			_height = $"{(h / 12).ToString()}'{(h % 12).ToString()}";
		}
		get
		{
			return _height;
		}
	}
	[JsonProperty("weightInPounds")]
	public int Weight { get; set; }
	public string BirthDate { get; set; }
	public BirthCity BirthCity { get; set; }
	public string BirthCountry { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public BirthStateProvince BirthStateProvince { get; set; }
}
public class PlayerData
{
	public List<Player> Forwards { get; set; }
	public List<Player> Defensemen { get; set; }
	public List<Player> Goalies { get; set; }
}


