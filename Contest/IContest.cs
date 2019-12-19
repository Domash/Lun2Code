using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lun2Code.Contest
{
    public interface IContest
    {
        Task<List<Contest>> GetContestsList();
    }
}