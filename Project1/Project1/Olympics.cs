using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Project1;
public class Olympics {
	public static List<Participant>? participants;

	public static List<Participant> ReadParticipants(string fname) {
		participants = new List<Participant>();
		using (StreamReader input = new StreamReader(fname)) {
			string? header = input.ReadLine();  // Read and discard the header
			while (!input.EndOfStream) {
				string? line = input.ReadLine();
				if (line == null) {
					continue;
				}
				Participant p = new Participant(line);
				participants.Add(p);
			}
		}
		return participants;
	}
	public static void Main(string[] args) {
		participants = ReadParticipants("olympics.tsv");

		while (true) {
			Console.Write(">> ");
			string? userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Invalid input. Please enter a valid command.");
                continue;
            }

            string[] inputTok = userInput.Split(' ');
            string command = inputTok[0];
            List<string> arguments = new List<string>();
            if (inputTok.Length > 1)
            {
                arguments = parseArguments(inputTok);
            }


            switch (command)
            {
                case "hosts":
                    listHosts();
                    break;
                case "count":
                    if (arguments.Count == 2 && int.TryParse(arguments[0], out int year))
                    {
                        string country = arguments[1];
                        CountMedalsByYearAndCountry(year, country);
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Usage: count year country");
                    }
                    break;
                case "golds":
                    if (arguments.Count == 2 && int.TryParse(arguments[0], out int goldYear))
                    {
                        string goldCountry = arguments[1];
                        ListGoldMedalWinners(goldYear, goldCountry);
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Usage: golds year country");
                    }
                    break;
                case "podium":
                    if (arguments.Count == 3 && int.TryParse(arguments[0], out int podiumYear))
                    {
                        string podiumSeason = arguments[1];
                        string podiumEvent = arguments[2];
                        ListMedalWinnersForEvent(podiumYear, ParseSeason(podiumSeason), podiumEvent);
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Usage: podium year season event");
                    }
                    break;
                case "exit":
                    ExitProgram();
                    break;

                default:
                    Console.WriteLine("Invalid command. Please try again");
                    break;
            }
		}
	}

    private static Participant.SeasonType ParseSeason(string season)
    {
        return season.Equals("Summer")
            ? Participant.SeasonType.Summer
            : Participant.SeasonType.Winter;
    }

    private static void listHosts()
	{
        List<string> hosts = new List<string>();
        foreach (Participant p in participants)
        {
            if (!hosts.Contains(p.Location))
            {
                hosts.Add(p.Location);
            }
        }

        hosts.Sort(); // Sort the list of host cities
        foreach (string host in hosts)
        {
            Console.WriteLine(host);
        }
    }

    private static void CountMedalsByYearAndCountry(int year, string country)
    {
        int count = participants.Count(p => p.Year == year && string.Equals(p.Country, country) && p.Medal != Participant.MedalType.None);
        Console.WriteLine($"Total medals won by {country} in {year}: {count}");
    }

    private static void ListGoldMedalWinners(int year, string country)
    {
        var goldMedalWinners = participants
            .Where(p => p.Year == year && string.Equals(p.Country, country) && p.Medal == Participant.MedalType.Gold)
            .Select(p => p.Name)
            .Distinct();

        foreach (string winner in goldMedalWinners)
        {
            Console.WriteLine(winner);
        }
    }


    private static void ListMedalWinnersForEvent(int year, Participant.SeasonType season, string eventName)
    {
        var medalWinners = participants
            .Where(p => p.Year == year && p.Season == season && string.Equals(p.Event, eventName) && p.Medal != Participant.MedalType.None)
            .OrderBy(p => p.Medal) // Order by medal type (Gold, Silver, Bronze)
            .ThenBy(p => p.Name)   // Then order by athlete name
            .GroupBy(p => p.Medal)
            .ToList();

        foreach (var medalGroup in medalWinners)
        {
            foreach (var participant in medalGroup)
            {
                Console.WriteLine($"{participant.Medal} - {participant.Name}");
            }
        }
    }


    private static void ExitProgram()
    {
        Environment.Exit(0);
    }

    public static List<string> parseArguments(string[] inputTok)
    {
        List<string> arguments = new List<string>();
        arguments.Add(inputTok[1]);
        if (inputTok[2] == "Summer" || inputTok[2] == "Winter")
        {
            arguments.Add(inputTok[2]);
            arguments.Add(string.Join(" ", inputTok.Skip(3)));
        } else
        {
            arguments.Add(string.Join(" ", inputTok.Skip(2)));
        }

        return arguments;
    }
}