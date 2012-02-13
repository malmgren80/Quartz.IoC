using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Quartz.Spi;

namespace Quartz.IoC.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IoC.BootStrap(new WindsorContainer());
            IoC.Container.Resolve<IQuartzInitializer>().Start();
        }
    }

    public static class IoC
    {
        public static IWindsorContainer Container { get; private set; }

        public static void BootStrap(IWindsorContainer container)
        {
            Container = container;

            IJobFactory jobFactory = new WindsorJobFactory(Container);

            Container.Register(Component.For<IJobFactory>().Instance(jobFactory));
            Container.Register(Component.For<IQuartzInitializer>().ImplementedBy<QuartzInitializer>());
            Container.Register(Component.For<ILogger>().ImplementedBy<ConsoleLogger>());

            JobRegistrar jobRegistrar = new JobRegistrar(Container);
            jobRegistrar.RegisterJobs();
        }
    }
}
