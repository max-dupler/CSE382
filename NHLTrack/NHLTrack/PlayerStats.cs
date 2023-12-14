using System;
using Newtonsoft.Json;

namespace NHLTrack
{

    public class DraftDetails
    {
        public int Year { get; set; }
        public string TeamAbbrev { get; set; }
        public int Round { get; set; }
        public int PickInRound { get; set; }
        public int OverallPick { get; set; }
    }

    public class SubSeason
    {
        public int GamesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Points { get; set; }
        public int PlusMinus { get; set; }
        public int Pim { get; set; }
        public int GameWinningGoals { get; set; }
        public int OtGoals { get; set; }
        public int Shots { get; set; }
        public double ShootingPctg { get; set; }
        public int PowerPlayGoals { get; set; }
        public int PowerPlayPoints { get; set; }
        public int ShorthandedGoals { get; set; }
        public int ShorthandedPoints { get; set; }
    }

    public class RegularSeason
    {
        public SubSeason SubSeason { get; set; }
        public SubSeason Career { get; set; }
    }

    public class FeaturedStats
    {
        public int Season { get; set; }
        public RegularSeason RegularSeason { get; set; }
    }

    public class RegularSeason2
    {
        public int GamesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Pim { get; set; }
        public int Points { get; set; }
        public int PlusMinus { get; set; }
        public int PowerPlayGoals { get; set; }
        public int PowerPlayPoints { get; set; }
        public int ShorthandedPoints { get; set; }
        public int GameWinningGoals { get; set; }
        public int OtGoals { get; set; }
        public int Shots { get; set; }
        public double ShootingPctg { get; set; }
        public double FaceoffWinningPctg { get; set; }
        public string AvgToi { get; set; }
        public int ShorthandedGoals { get; set; }
    }

    public class Playoffs
    {
        public int GamesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Pim { get; set; }
        public int Points { get; set; }
        public int PlusMinus { get; set; }
        public int PowerPlayGoals { get; set; }
        public int PowerPlayPoints { get; set; }
        public int ShorthandedPoints { get; set; }
        public int GameWinningGoals { get; set; }
        public int OtGoals { get; set; }
        public int Shots { get; set; }
        public double ShootingPctg { get; set; }
        public double FaceoffWinningPctg { get; set; }
        public string AvgToi { get; set; }
        public int ShorthandedGoals { get; set; }
    }

    public class CareerTotals
    {
        public RegularSeason2 RegularSeason { get; set; }
        public Playoffs Playoffs { get; set; }
    }

    public class Last5Game
    {
        public int GameId { get; set; }
        public int GameTypeId { get; set; }
        public string TeamAbbrev { get; set; }
        public string HomeRoadFlag { get; set; }
        public DateTime GameDate { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Points { get; set; }
        public int PlusMinus { get; set; }
        public int PowerPlayGoals { get; set; }
        public int Shots { get; set; }
        public int Shifts { get; set; }
        public int ShorthandedGoals { get; set; }
        public int Pim { get; set; }
        public string OpponentAbbrev { get; set; }
        public string Toi { get; set; }
    }

    public class PlayerStats
    {
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
                }
                else if (value == "L")
                {
                    _position = "LW";
                }
                else
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
        public DraftDetails DraftDetails { get; set; }
        public string PlayerSlug { get; set; }
        public FeaturedStats FeaturedStats { get; set; }
        public CareerTotals CareerTotals { get; set; }
        public List<Last5Game> Last5Games { get; set; }
    }
}

