using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalrMassTransitBug.Consumers
{
    public interface ITest
    {
        string Message { get; set; }
    }

    public interface ITestResponse
    {
        string Response { get; set; }
    }

    public class Test : ITest
    {
        public string Message { get; set; }
    }

    public class TestResponse : ITestResponse
    {
        public string Response { get; set; }
    }


    public class TestConsumer : IConsumer<ITest>
    {
        public async Task Consume(ConsumeContext<ITest> context)
        {
            await context.RespondAsync(new TestResponse
            {
                Response = $"Processed Request: {context.Message.Message}"
            });
        }
    }
}