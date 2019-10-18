using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AutofacDiIssue60
{
    public class DepTests
    {
        private class SomeDependency {}
        
        [Fact]
        public void DependencyRegisteredResolved()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<SomeDependency>(_ => null);
            var provider = serviceCollection.BuildServiceProvider();
            var service = provider.GetService<SomeDependency>();
            
            Assert.Null(service);
        }
        
        [Fact]
        public void DependencyOptionalResolved()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<SomeDependency>(_ => null);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);
            var container = containerBuilder.Build();
            var autofacServiceProvider = new AutofacServiceProvider(container);
            var service = autofacServiceProvider.GetService<SomeDependency>();
            
            Assert.Null(service);
        }
    }
}
