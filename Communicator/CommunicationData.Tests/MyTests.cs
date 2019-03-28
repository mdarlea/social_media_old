using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Swaksoft.Infrastructure.Data.Communicator.Tests
{
    /// <summary>
    /// Summary description for MyTests
    /// </summary>
    [TestClass]
    [Ignore]
    public class MyTests
    {
        public MyTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void MyTest()
        {
            using (var uow = new ProfiledCommunicatorUnitOfWork())
            {
                var results =
                    (from r in uow.MessageOperations.OfType<VoiceOperation>()
                     where r.Timeout == 0
                     select r).ToList();
            }
        }
    }
}
