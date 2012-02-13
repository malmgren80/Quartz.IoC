using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Quartz.IoC
{
    public class JobRegistrar
    {
        private readonly IWindsorContainer _container;

        public JobRegistrar(IWindsorContainer container)
        {
            _container = container;
        }

        private static IEnumerable<Type> GetJobTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IJob).IsAssignableFrom(p) && !p.IsInterface);
        }

        public void RegisterJobs()
        {
            var jobTypes = GetJobTypes();
            foreach (Type jobType in jobTypes)
            {
                _container.Register(Component.For(jobType).ImplementedBy(jobType).LifeStyle.Transient);
            }
        }
    }
}
