using Quartz;
using Quartz.Impl;

ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
IScheduler scheduler = await schedulerFactory.GetScheduler();
await scheduler.Start();

IJobDetail job = JobBuilder.Create<MyPrintClass>()
    .WithIdentity("myJob", "group1")
    .Build();

ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("myTrigger", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

Console.WriteLine("Press [Enter] to exit.");
Console.ReadLine();

await scheduler.Shutdown();