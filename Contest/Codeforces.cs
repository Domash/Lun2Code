using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lun2Code.Contest
{
    public class Codeforces : IContest
    {
        //private string _apiKey = "761f043932d0b749c6748268367b632f568ca2a6";
        //private string _secretKey = "e5eedefe20ec42f887201829d80733222e23e74a";
        private DateTime LowerBound { get; set; }
        
        public Codeforces(DateTime lowerBound)
        {
            LowerBound = lowerBound;
        }
        
        public async Task<List<Contest>> GetContestsList()
        {
            var contestsList = new List<Contest>();

            using (var webClient = new WebClient())
            {
                var response = await webClient.DownloadStringTaskAsync("https://codeforces.com/api/contest.list?gym=false");

                var list = response;

                dynamic result = JsonConvert.DeserializeObject(list);

                for (int i = 0; i < 10; ++i)
                {
                    var contest = new Contest
                    {
                        StartTime = Contest.UnixTimeStampToDateTime((long)result.result[i].startTimeSeconds),
                        CloseTime = Contest.UnixTimeStampToDateTime((long)result.result[i].startTimeSeconds + (long)result.result[i].durationSeconds),
                        EventName = result.result[i].name,
                        EventUrl = "https://codeforces.com/contests/" + result.result[i].id,
                        PlatformUrl = "https://codeforces.com",
                        PlatformName = "Codeforces"
                    };
                    
                    if(contest.StartTime < LowerBound) break;
                    
                    contestsList.Add(contest);
                }
            }

            return contestsList;
        }
    }
}