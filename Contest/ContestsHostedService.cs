using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Lun2Code.Contest
{
    public class ContestsHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer  = new Timer(UpdateContestList, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
        
        private void UpdateContestList(object state)
        {
            Console.WriteLine("Update contests list");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}