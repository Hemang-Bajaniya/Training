using Quartz;

public class MyPrintClass : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Current Time: " + DateTime.Now.ToString("hh:mm:ss tt"));
        return Task.CompletedTask;
    }
}