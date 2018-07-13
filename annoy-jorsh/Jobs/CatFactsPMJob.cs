using System;
using System.Threading.Tasks;
using Quartz;

namespace annoyjorsh.Jobs
{
    public class CatFactsPMJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
