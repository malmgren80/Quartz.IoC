using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Quartz.Spi;

namespace Quartz.IoC
{
    public class WindsorJobFactory : IJobFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorJobFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }
    }
}
