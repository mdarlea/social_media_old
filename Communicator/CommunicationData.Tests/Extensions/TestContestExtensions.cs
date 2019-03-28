using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Swaksoft.Infrastructure.Data.Communicator.Tests.Extensions
{
    public static class TestContestExtensions
    {
        public static T Resolve<T>(this TestContext testContext)
        {
            var locator = testContext.Properties["IOC"] as IServiceLocator;
            if (locator == null)
            {
                throw new Exception("Could not find the service locator");
            }
            return locator.GetInstance<T>();
        }
    }
}
