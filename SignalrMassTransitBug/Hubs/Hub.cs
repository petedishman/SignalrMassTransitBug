using MassTransit;
using Microsoft.AspNet.SignalR;
using SignalrMassTransitBug.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalrMassTransitBug.Hubs
{
    public class TestHub : Hub
    {
        public async Task Test()
        {
            try
            {

                var serviceAddress = new Uri($"rabbitmq://localhost/{MvcApplication.QueueName}");
                var requestClient = new MessageRequestClient<ITest, ITestResponse>(MvcApplication.Bus, serviceAddress, TimeSpan.FromSeconds(10));
                var response = await requestClient.Request(new Test { Message = "SendTestCommand" });

                await Clients.All.TestResponse($"{DateTime.Now} - Got response: {response.Response}");
            }
            catch (Exception ex)
            {
                await Clients.All.TestResponse($"{DateTime.Now} - Got exception:\n{ex.ToString()}");
            }            
        }

        private async void TestAsyncVoid()
        {

        }
    }
}