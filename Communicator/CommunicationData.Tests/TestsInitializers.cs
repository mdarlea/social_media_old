using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Swaksoft.Infrastructure.Data.Communicator.Tests
{
    [TestClass]
    [Ignore]
    public static class TestsInitializers
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // Initalization code goes here
            var container = new UnityContainer();
            var bootstrap = new UnityBootstrap(container);
            var locator = new UnityServiceLocator(container);
            context.Properties.Add("IOC",locator);

        }
    }
}
