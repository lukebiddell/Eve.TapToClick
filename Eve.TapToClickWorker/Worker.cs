using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eve.TapToClickWorker.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Eve.TapToClickWorker
{
    public class Worker : BackgroundService, IDisposable
    {
        private readonly ILogger<Worker> _logger;

        private DummyWindow dummyWindow;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Initialize();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void Initialize()
        {
            dummyWindow = new DummyWindow();
        }

        public override void Dispose()
        {
            dummyWindow.Dispose();
            base.Dispose();
        }
    }
}
