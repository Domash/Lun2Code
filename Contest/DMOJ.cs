using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lun2Code.Contest
{
    public class DMOJ : IContest
    {
        private DateTime LowerBound { get; set; }

        public DMOJ(DateTime lowerBound)
        {
            LowerBound = lowerBound;
        }
        
        public async Task<List<Contest>> GetContestsList()
        {
            var contestsList = new List<Contest>();
            
            using (var webClient = new WebClient())
            {
                var response = await webClient.DownloadStringTaskAsync("https://dmoj.ca/api/contest/list");
                
                var result = JsonConvert.DeserializeObject<Dictionary<string, DmojObject>>(response);

                foreach (var res in result)
                {
                    var flag = ConvertToDateTime(res.Value.StartTime, res.Value.CloseTIme, out var startTime, out var closeTime);

                    if (flag && closeTime > LowerBound)
                    {
                        contestsList.Add(new Contest
                        {
                            StartTime = startTime,
                            CloseTime = closeTime,
                            EventName = res.Value.Name,
                            EventUrl = "https://dmoj.ca/contest/" + res.Key,
                            PlatformUrl = "https://dmoj.ca/",
                            PlatformName = "DMOJ"
                        });
                    }
                }    
            }

            return contestsList;
        }
        
        private bool ConvertToDateTime(string valueStartTime, string valueCloseTime, out DateTime startTime, out DateTime closeTime)
        {

            valueStartTime = valueStartTime.Replace('T', ' ').Substring(0, valueStartTime.Length - 6);
            valueCloseTime = valueCloseTime.Replace('T', ' ').Substring(0, valueCloseTime.Length - 6);
            
            bool first = DateTime.TryParseExact(valueStartTime, "yyyy-MM-dd hh:mm:ss", null, DateTimeStyles.None, out startTime);
            bool second = DateTime.TryParseExact(valueCloseTime, "yyyy-MM-dd hh:mm:ss", null, DateTimeStyles.None, out closeTime);

            return first && second;
        }    
    }

    internal class DmojObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("start_time")]
        public string StartTime { get; set; }
        
        [JsonProperty("end_time")]
        public string CloseTIme { get; set; }

    }
    
}