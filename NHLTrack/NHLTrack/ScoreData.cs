using System;
using System.Collections.Generic;
using System.Globalization;

namespace NHLTrack;

public class Venue
{
    public string Default { get; set; }
}

public class TvBroadcast
{
    public int Id { get; set; }
    public string Market { get; set; }
    public string CountryCode { get; set; }
    public string Network { get; set; }
}

public class PlaceName
{
    public string Default { get; set; }
}

public class GameTeam
{
    public int Id { get; set; }
    public PlaceName PlaceName { get; set; }
    public string Abbrev { get; set; }
    public string Logo
    {
        get { return $"../Resources/Images/Teams/{Abbrev}.png"; }
    }
    public string DarkLogo { get; set; }
    public bool AwaySplitSquad { get; set; }
    public int Score { get; set; }
}

public class PeriodDescriptor
{
    public int Number { get; set; }
    public string PeriodType { get; set; }
}

public class GameOutcome
{
    public string LastPeriodType { get; set; }
}

public class GamePlayer
{
    public int PlayerId { get; set; }
    public PlaceName FirstInitial { get; set; }
    public PlaceName LastName { get; set; }
}

public class Game
{
    public int Id { get; set; }
    public int Season { get; set; }
    public int GameType { get; set; }
    public Venue Venue { get; set; }
    public bool NeutralSite { get; set; }
    public string StartTimeUTC { get; set; }
    public string EasternUTCOffset { get; set; }
    public string VenueUTCOffset { get; set; }
    public string VenueTimezone { get; set; }
    private string _gameState;
    public string GameState
    {
        get { return _gameState; }
        set
        {
            if (value == "OFF")
            {
                _gameState = "Final";
            } else if (value == "FUT") {
                DateTime start = DateTime.Parse(StartTimeUTC);
                string id = Preferences.Get("TimeZone", "America/Eastern");
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
                TimeZoneInfo local = TimeZoneInfo.Local;
                _gameState = TimeZoneInfo.ConvertTime(start, local, timeZone).ToString("hh:mm tt");
            } else
            {
                _gameState = value;
            }
        }
    }
    public string GameScheduleState { get; set; }
    public List<TvBroadcast> TvBroadcasts { get; set; }
    public GameTeam AwayTeam { get; set; }
    public GameTeam HomeTeam { get; set; }
    public PeriodDescriptor PeriodDescriptor { get; set; }
    public GameOutcome GameOutcome { get; set; }
    public GamePlayer WinningGoalie { get; set; }
    public GamePlayer WinningGoalScorer { get; set; }
    public string ThreeMinRecap { get; set; }
    public string ThreeMinRecapFr { get; set; }
    public string GameCenterLink { get; set; }
    public string RadioLink { get; set; }
    public List<string> Odds { get; set; }
    public string TicketsLink { get; set; }
}

public class GameDay
{
    public string Date { get; set; }
    public string DayAbbrev { get; set; }
    public int NumberOfGames { get; set; }
    public List<Game> Games { get; set; }
}

public class ScoreData
{
    public List<GameDay> GameWeek { get; set; }
}



