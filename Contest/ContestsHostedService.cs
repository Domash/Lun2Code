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
            _timer  = new Timer(CollectInfo, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
        
        private void CollectInfo(object state)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}