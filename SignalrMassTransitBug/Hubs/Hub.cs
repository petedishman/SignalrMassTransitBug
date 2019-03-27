using Microsoft.AspNet.SignalR;
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
                await Clients.All.TestResponse($"{DateTime.Now} - Hello");
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