using System;
using System.Threading;
using System.Threading.Tasks;
using Lun2Code.Controllers;
using Microsoft.Extensions.Hosting;

namespace Lun2Code.Contest
{
    public class ContestsHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer  = new Timer(UpdateContestList, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
        
        private void UpdateContestList(object state)
        {
            ContestsController.UpdateContests();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}