using MassTransit;
using SignalrMassTransitBug.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SignalrMassTransitBug
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static IBusControl _busControl;

        public const string QueueName = "Test-Queue";

        public static IBus Bus
        {
            get { return _busControl; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            _busControl = ConfigureBus();
            _busControl.Start();
        }

        protected void Application_End()
        {
            _busControl.Stop(TimeSpan.FromSeconds(10));
        }

        IBusControl ConfigureBus()
        {
            return MassTransit.Bus.Factory.CreateUsingInMemory(cfg => 
            {
                cfg.ReceiveEndpoint(QueueName, ep => 
                {
                    ep.Consumer<TestConsumer>();
                });
            });
        }
    }
}
