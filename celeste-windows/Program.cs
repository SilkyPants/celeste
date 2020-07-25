
using Celeste.Utility;

using Microsoft.Extensions.DependencyInjection;

namespace Celeste.Windows
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new CelesteWebHost();
            host.RunHost((_, services) =>
            {
                services.AddSingleton<IVirtualControls, VirtualControls>();
            });
        }
    }
}
