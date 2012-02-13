using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.IoC
{
    public class HelloJob : IJob
    {
        private readonly ILogger _logger;

        public HelloJob(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute(JobExecutionContext context)
        {
            _logger.Log(@"Oh Hai \o/");
        }
    }

    public interface ILogger
    {
        void Log(string msg);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
