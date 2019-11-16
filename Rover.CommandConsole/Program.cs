using Microsoft.Extensions.DependencyInjection;
using Rover.Common;
using Rover.Navigator;
using Rover.Navigator.Services;
using System.Linq;

namespace Rover.CommandConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<RoverOperator, RoverOperator>()
            .AddSingleton<IRoverNavigator, RoverNavigator>()
            .BuildServiceProvider();
            var provider = serviceProvider.GetService<RoverOperator>();
            provider.Operate();
        }


    }

}
