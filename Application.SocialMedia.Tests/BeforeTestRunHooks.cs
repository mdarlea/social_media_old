using System;
using System.Collections.Generic;
using System.Data.Entity;
using Application.SocialMedia.Tests.Data;
using log4net.Appender;
using log4net.Config;
using Swaksoft.Application.Seedwork.Logging;
using Swaksoft.Application.Seedwork.Validation;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using Swaksoft.Infrastructure.Crosscutting.TypeMapping;
using Swaksoft.Infrastructure.Crosscutting.Validation;
using TechTalk.SpecFlow;

namespace Application.SocialMedia.Tests
{
    [Binding]
    public class BeforeTestRunHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private static readonly Lazy<TestDomainEventsHandlers> eventHandlers = new Lazy<TestDomainEventsHandlers>();

        public static IEnumerable<IDomainEvent> GetRaisedDomanEvents()
        {
            return eventHandlers.Value.RaisedDomainEvents;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            BasicConfigurator.Configure(new MemoryAppender());

            TypeAdapterLocator.SetCurrent(new TestMapperTypeAdapterFactory());
            EntityValidatorLocator.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            LoggerLocator.SetCurrent(new Log4NetLoggerFactory());

            using (var uofw = new TestSocialMediaUnitOfWork())
            {
                Database.SetInitializer(new SocialMediaDatabaseInitializer());
                uofw.Database.Initialize(true);
            }
            DomainEvents.SetCurrent(eventHandlers.Value);
            Console.WriteLine(@"Domain events set to TestDomainEventsHandlers");
        }
    }
}
