using System;
using CAPMessageBusWithRabbitMq.Core;
using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CAPMessageBusWithRabbitMq.ExternalSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ServiceBuilder();
            Console.ReadLine();
        }

        private static ServiceProvider ServiceBuilder()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(x => x.AddConsole());
            serviceCollection.AddDbContext<AppDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql("User ID=dev;Password=password;Server=localhost;port=5432;Database=MessageBusWithRabbitMq;Integrated Security=true;Pooling=true;", topology => topology.UseNetTopologySuite());
            });
            serviceCollection.AddCap(capOptions =>
            {
                capOptions.UseRabbitMQ("localhost");
                capOptions.UseEntityFramework<AppDbContext>();
            });
            serviceCollection.AddSingleton<GeoDataSerialisationService>();
            serviceCollection.AddScoped<MessageHandler>();
            var builder = serviceCollection.BuildServiceProvider();
            builder.GetService<IBootstrapper>().BootstrapAsync(default);
            return builder;
        }
        

    }
}