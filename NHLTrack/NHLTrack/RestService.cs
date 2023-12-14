using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NHLTrack
{
	public class RestService
	{
		HttpClient _client;

		public RestService()
		{
			_client = new HttpClient();
		}

		public async Task<ScoreData> GetScores(string query)
		{
			ScoreData scoreData = null;
			try
			{
				var response = await _client.GetAsync(query);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					scoreData = JsonConvert.DeserializeObject<ScoreData>(content);
					JObject d = JObject.Parse(content);
					Console.WriteLine(d.Property("Title"));

				}
			} catch (Exception e)
			{
				Debug.WriteLine("\t\t Error" + e.Message);
			}

			return scoreData;
		}

		public async Task<PlayerData> GetPlayers(string query)
		{
			PlayerData playerData = null;
			try
			{
				var response = await _client.GetAsync(query);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					playerData = JsonConvert.DeserializeObject<PlayerData>(content);
                }
            } catch (Exception e)
            {
                Debug.WriteLine("\t\t Error" + e.Message);
            }
			return playerData;
        }

		public async Task<PlayerStats> GetStats(string query)
		{
			PlayerStats playerStats = null;
			try
			{
				var response = await _client.GetAsync(query);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					playerStats = JsonConvert.DeserializeObject<PlayerStats>(content);
				}
			} catch (Exception e)
            {
                Debug.WriteLine("\t\t Error" + e.Message);
            }
			return playerStats;
        }

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}

