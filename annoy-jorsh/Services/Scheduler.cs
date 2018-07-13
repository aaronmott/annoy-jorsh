using System;
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

namespace annoyjorsh.Services
{
    public class Scheduler : IScheduler
    {
        IScheduler _scheduler;
        StdSchedulerFactory _factory;
        public Scheduler()
        {
            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            _factory = new StdSchedulerFactory(props);
            _scheduler = await factory.GetScheduler();
            await scheduler.Start();
        }
    }
}
