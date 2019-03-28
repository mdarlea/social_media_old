﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Application.SocialMedia.Tests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class EventAppServiceFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "EventAppService.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "EventAppService", "\tScenarios for the Event Application Service", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "EventAppService")))
            {
                Application.SocialMedia.Tests.EventAppServiceFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Create a new event at an existing address")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "EventAppService")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CreateNewEventAtExistingAddress")]
        public virtual void CreateANewEventAtAnExistingAddress()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new event at an existing address", new string[] {
                        "CreateNewEventAtExistingAddress"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table1.AddRow(new string[] {
                        "Name",
                        "Youth Meeting"});
            table1.AddRow(new string[] {
                        "Description",
                        "Young adults meeting"});
            table1.AddRow(new string[] {
                        "AddressId",
                        "2"});
            table1.AddRow(new string[] {
                        "UserId",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5"});
            table1.AddRow(new string[] {
                        "StartTime",
                        "10 PM"});
            table1.AddRow(new string[] {
                        "EndTime",
                        "11 PM"});
#line 6
testRunner.Given("the following event:", ((string)(null)), table1, "Given ");
#line 14
testRunner.When("I create this event", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table2.AddRow(new string[] {
                        "Name",
                        "Youth Meeting"});
            table2.AddRow(new string[] {
                        "Description",
                        "Young adults meeting"});
            table2.AddRow(new string[] {
                        "AddressId",
                        "2"});
            table2.AddRow(new string[] {
                        "UserId",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5"});
            table2.AddRow(new string[] {
                        "StartTime",
                        "10 PM"});
            table2.AddRow(new string[] {
                        "EndTime",
                        "11 PM"});
#line 15
testRunner.Then("a new event with the information below should be created in the database:", ((string)(null)), table2, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table3.AddRow(new string[] {
                        "Status",
                        "Success"});
            table3.AddRow(new string[] {
                        "Name",
                        "Youth Meeting"});
            table3.AddRow(new string[] {
                        "Description",
                        "Young adults meeting"});
            table3.AddRow(new string[] {
                        "AddressId",
                        "2"});
            table3.AddRow(new string[] {
                        "UserId",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5"});
            table3.AddRow(new string[] {
                        "StartTime",
                        "10 PM"});
            table3.AddRow(new string[] {
                        "EndTime",
                        "11 PM"});
            table3.AddRow(new string[] {
                        "Instructor",
                        "Michelle Darlea"});
#line 23
testRunner.And("the event application service should return a dto with the following event inform" +
                    "ation:", ((string)(null)), table3, "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table4.AddRow(new string[] {
                        "Id",
                        "2"});
            table4.AddRow(new string[] {
                        "StreetAddress",
                        "3668 Livernois Rd"});
            table4.AddRow(new string[] {
                        "SuiteNumber",
                        ""});
            table4.AddRow(new string[] {
                        "City",
                        "Troy"});
            table4.AddRow(new string[] {
                        "State",
                        "MI"});
            table4.AddRow(new string[] {
                        "Zip",
                        "48083"});
            table4.AddRow(new string[] {
                        "CountryIsoCode",
                        "us"});
            table4.AddRow(new string[] {
                        "Latitude",
                        "42.572365"});
            table4.AddRow(new string[] {
                        "Longitude",
                        "-83.146155"});
            table4.AddRow(new string[] {
                        "GeolocationStreetNumber",
                        "3668"});
            table4.AddRow(new string[] {
                        "GeolocationStreet",
                        "3668 Livernois Rd"});
            table4.AddRow(new string[] {
                        "IsMainAddress",
                        "falses"});
#line 33
testRunner.And("the followng address:", ((string)(null)), table4, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Find weekly events for user")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "EventAppService")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("FindWeeklyEventsForUser")]
        public virtual void FindWeeklyEventsForUser()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Find weekly events for user", new string[] {
                        "FindWeeklyEventsForUser"});
#line 49
this.ScenarioSetup(scenarioInfo);
#line 50
 testRunner.Given("The user with the \'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5\' id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 51
 testRunner.When("I search for the user\'s events in the weekly calendar", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name",
                        "Description",
                        "AddressId",
                        "UserId",
                        "Instructor"});
            table5.AddRow(new string[] {
                        "1",
                        "Prayer Meeting",
                        "Meet up for prayer",
                        "1",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5",
                        "Michelle Darlea"});
            table5.AddRow(new string[] {
                        "3",
                        "Small Group Meeting",
                        "Small Group Meeting",
                        "1",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5",
                        "Michelle Darlea"});
#line 52
 testRunner.Then("the service should return the following events:", ((string)(null)), table5, "Then ");
#line 56
 testRunner.And("the \'Prayer Meeting\' event should start on Saturday from 7 PM until 9 PM", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Find event")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "EventAppService")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("FindEvent")]
        public virtual void FindEvent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Find event", new string[] {
                        "FindEvent"});
#line 59
this.ScenarioSetup(scenarioInfo);
#line 60
testRunner.Given("the event id equal with 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 61
testRunner.When("I search for this event", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table6.AddRow(new string[] {
                        "Status",
                        "Success"});
            table6.AddRow(new string[] {
                        "Id",
                        "1"});
            table6.AddRow(new string[] {
                        "Name",
                        "Prayer Meeting"});
            table6.AddRow(new string[] {
                        "Description",
                        "Meet up for prayer"});
            table6.AddRow(new string[] {
                        "AddressId",
                        "1"});
            table6.AddRow(new string[] {
                        "UserId",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5"});
            table6.AddRow(new string[] {
                        "Instructor",
                        "Michelle Darlea"});
#line 62
testRunner.Then("the event application service should return a dto with the following event inform" +
                    "ation:", ((string)(null)), table6, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table7.AddRow(new string[] {
                        "StreetAddress",
                        "10023 Belle Rive Blvd."});
            table7.AddRow(new string[] {
                        "SuiteNumber",
                        "Apt. 1204"});
            table7.AddRow(new string[] {
                        "City",
                        "Jacksonville"});
            table7.AddRow(new string[] {
                        "State",
                        "Florida"});
            table7.AddRow(new string[] {
                        "Zip",
                        "32256"});
            table7.AddRow(new string[] {
                        "CountryIsoCode",
                        "us"});
            table7.AddRow(new string[] {
                        "Latitude",
                        "30.210796"});
            table7.AddRow(new string[] {
                        "Longitude",
                        "-81.5489216"});
            table7.AddRow(new string[] {
                        "GeolocationStreetNumber",
                        "10023"});
            table7.AddRow(new string[] {
                        "GeolocationStreet",
                        "Belle Rive Boulevard"});
            table7.AddRow(new string[] {
                        "IsMainAddress",
                        "true"});
#line 71
testRunner.And("the followng address:", ((string)(null)), table7, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Find repeated events")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "EventAppService")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("FindRepeatedEvents")]
        public virtual void FindRepeatedEvents()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Find repeated events", new string[] {
                        "FindRepeatedEvents"});
#line 86
this.ScenarioSetup(scenarioInfo);
#line 87
testRunner.Given("The user with the \'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5\' id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 88
testRunner.And("a start time equal with 12/11/2016", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
testRunner.And("an end time equal with 12/17/2016", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 90
testRunner.When("I search for the user\'s events in the given time range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
testRunner.Then("the application service should return an action result with the Success status", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table8.AddRow(new string[] {
                        "Status",
                        "Success"});
            table8.AddRow(new string[] {
                        "Id",
                        "3"});
            table8.AddRow(new string[] {
                        "Name",
                        "Small Group Meeting"});
            table8.AddRow(new string[] {
                        "Instructor",
                        "Michelle Darlea"});
            table8.AddRow(new string[] {
                        "Description",
                        "Small Group Meeting"});
            table8.AddRow(new string[] {
                        "AddressId",
                        "1"});
            table8.AddRow(new string[] {
                        "UserId",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5"});
            table8.AddRow(new string[] {
                        "Repeat",
                        "True"});
            table8.AddRow(new string[] {
                        "StartTime",
                        "12/14/2016 1:00 PM"});
            table8.AddRow(new string[] {
                        "EndTime",
                        "12/14/2016 3:00 PM"});
#line 92
testRunner.And("the following event should be found:", ((string)(null)), table8, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Find weekly repeated events (1)")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "EventAppService")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("FindRepeatedEvents")]
        public virtual void FindWeeklyRepeatedEvents1()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Find weekly repeated events (1)", new string[] {
                        "FindRepeatedEvents"});
#line 106
this.ScenarioSetup(scenarioInfo);
#line 107
testRunner.Given("The user with the \'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5\' id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 108
testRunner.And("a start time equal with 12/18/2016", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 109
testRunner.And("an end time equal with 12/24/2016", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
testRunner.When("I search for the user\'s events in the given time range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 111
testRunner.Then("the application service should return an action result with the Success status", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table9.AddRow(new string[] {
                        "Status",
                        "Success"});
            table9.AddRow(new string[] {
                        "Id",
                        "3"});
            table9.AddRow(new string[] {
                        "Name",
                        "Small Group Meeting"});
            table9.AddRow(new string[] {
                        "Instructor",
                        "Michelle Darlea"});
            table9.AddRow(new string[] {
                        "Description",
                        "Small Group Meeting"});
            table9.AddRow(new string[] {
                        "AddressId",
                        "1"});
            table9.AddRow(new string[] {
                        "UserId",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5"});
            table9.AddRow(new string[] {
                        "Repeat",
                        "True"});
            table9.AddRow(new string[] {
                        "StartTime",
                        "12/14/2016 1:00 PM"});
            table9.AddRow(new string[] {
                        "EndTime",
                        "12/14/2016 3:00 PM"});
#line 112
testRunner.And("the following event should be found:", ((string)(null)), table9, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Find weekly repeated events (2)")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "EventAppService")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("FindRepeatedEvents")]
        public virtual void FindWeeklyRepeatedEvents2()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Find weekly repeated events (2)", new string[] {
                        "FindRepeatedEvents"});
#line 126
this.ScenarioSetup(scenarioInfo);
#line 127
testRunner.Given("The user with the \'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5\' id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 128
testRunner.And("a start time equal with 12/25/2016", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 129
testRunner.And("an end time equal with 12/31/2016", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 130
testRunner.When("I search for the user\'s events in the given time range", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 131
testRunner.Then("the application service should return an action result with the Success status", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table10.AddRow(new string[] {
                        "Id",
                        "3"});
            table10.AddRow(new string[] {
                        "Name",
                        "Small Group Meeting"});
            table10.AddRow(new string[] {
                        "Instructor",
                        "Michelle Darlea"});
            table10.AddRow(new string[] {
                        "Description",
                        "Small Group Meeting"});
            table10.AddRow(new string[] {
                        "AddressId",
                        "1"});
            table10.AddRow(new string[] {
                        "UserId",
                        "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5"});
            table10.AddRow(new string[] {
                        "Repeat",
                        "True"});
            table10.AddRow(new string[] {
                        "StartTime",
                        "12/14/2016 1:00 PM"});
            table10.AddRow(new string[] {
                        "EndTime",
                        "12/14/2016 3:00 PM"});
#line 132
testRunner.And("the following event should be found:", ((string)(null)), table10, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion