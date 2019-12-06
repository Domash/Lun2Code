using System;
using System.Collections.Generic;

namespace Lun2Code.Contest
{
    public abstract class Contest
    {
        public DateTime StartTime { get; set; }
        public DateTime CloseTime { get; set; }
        
        public string EventUrl { get; set; }
        public string PlatformUrl { get; set; }

        public abstract List<Contest> GetContestList();

    }
}