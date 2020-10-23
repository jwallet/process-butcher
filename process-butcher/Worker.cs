using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace process_butcher
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Arguments _args;

        public Worker(ILogger<Worker> logger, string[] args)
        {
            _args = new Arguments(args);
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bothMounted = false;

            if (string.IsNullOrWhiteSpace(_args.master) || string.IsNullOrWhiteSpace(_args.slave))
            {
                if (string.IsNullOrWhiteSpace(_args.master)) _logger.LogInformation("No argument '--master' specified; needs a process name; (e.g.: --master=nordvpn)");
                if (string.IsNullOrWhiteSpace(_args.slave)) _logger.LogInformation("No argument '--slave' specified; needs a process name; (.eg.: --slave=utorrent)");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var processes = Process.GetProcesses();
                var vpnProcesses = processes.Where(x => x.ProcessName.ToLower() == _args.master).ToList();
                var torrentProcesses = processes.Where(x => x.ProcessName.ToLower() == _args.slave).ToList();

                if (bothMounted)
                {
                    if (!vpnProcesses.Any())
                    {
                        if (torrentProcesses.Any())
                        {
                            torrentProcesses.ForEach(t => t.Kill());
                            vpnProcesses.ForEach(t => t.Kill());
                        }
                        else
                        {
                            bothMounted = false;
                        }
                    }
                }
                else
                {
                    if (torrentProcesses.Any())
                    {
                        if (vpnProcesses.Any())
                        {
                            bothMounted = true;
                        }
                        else
                        {
                            torrentProcesses.ForEach(t => t.Kill());
                        }
                    }
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
