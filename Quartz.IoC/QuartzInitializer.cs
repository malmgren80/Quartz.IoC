using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz.Impl;
using Quartz.Spi;

namespace Quartz.IoC
{
    public class QuartzInitializer : IQuartzInitializer
    {
        private readonly IJobFactory _jobFactory;

        public QuartzInitializer(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        public void Start()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.JobFactory = _jobFactory;
            sched.Start();

            // construct job info
            JobDetail jobDetail = new JobDetail("myJob", null, typeof(HelloJob));
            // fire every hour
            Trigger trigger = TriggerUtils.MakeSecondlyTrigger(5);
            trigger.Name = "myTrigger";
            sched.ScheduleJob(jobDetail, trigger);
        }
    }
}
