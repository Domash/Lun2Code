using System;

namespace Lun2Code.Contest
{
    public class Contest
    {
        public DateTime StartTime { get; set; }
        public DateTime CloseTime { get; set; }
        
        public string EventName { get; set; }
        public string EventUrl { get; set; }
        public string PlatformUrl { get; set; }

        public Contest()
        {
            
        }

        public Contest(string eventName, string eventUrl, string platformUrl, DateTime startTime, DateTime closeTime)
        {
            EventName = eventName;
            EventUrl = eventUrl;
            PlatformUrl = platformUrl;
            StartTime = startTime;
            CloseTime = closeTime;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        
    }
}