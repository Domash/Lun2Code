using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lun2Code.Contest
{
    public class Codeforces : IContest
    {
        private string _apiKey = "761f043932d0b749c6748268367b632f568ca2a6";
        private string _secretKey = "e5eedefe20ec42f887201829d80733222e23e74a";
        public DateTime LowerBound { get; set; }
        
        public Codeforces(DateTime lowerBound)
        {
            LowerBound = lowerBound;
        }
        
        public List<Contest> GetContestsList()
        {
            var contestsList = new List<Contest>();

            using (WebClient webClient = new WebClient())
            {
                var response = webClient.DownloadStringTaskAsync("https://codeforces.com/api/contest.list?gym=false");

                var list = response.Result;

                dynamic res = JsonConvert.DeserializeObject(list);
                
                Console.WriteLine("KEK");
                Console.WriteLine(res.result[0].name);
                
            }

            return new List<Contest>();
        }
    }
}