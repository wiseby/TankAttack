using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiClient
{
    public class Score
    {
        public string GreenPlayerName { get; set; }
        public int GreenPlayerHealth { get; set; }
        public string BrownPlayerName { get; set; }
        public int BrownPlayerHealth { get; set; }
        public int SecondsPlayed { get; set; }
    }

    public class ApiClient
    {   
        private readonly HttpClient client = new HttpClient();

        public async Task<List<Score>> GetScoresAsync()
        {
            List<Score> scores = new List<Score>();

            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add
            ( new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("highscore");
            var result = await response.Content.ReadAsStringAsync();
            scores = JsonConvert.DeserializeObject<List<Score>>(result);
            return scores;
        }

        public async void PostScore(Score score)
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add
            ( new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonString = JsonConvert.SerializeObject(score);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Highscore", content);
            string result = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(result);
        }
    }
}