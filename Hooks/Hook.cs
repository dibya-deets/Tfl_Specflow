using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using tfl_automation.Drivers;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;

namespace tfl_automation.Hooks
{
    [Binding]
    public sealed class HooksIntialize
    {
        private static ScenarioContext _scenariocontext;

        private static ExtentReports extent;
        private static ExtentTest feature;
        private static ExtentTest scenario;
        static string testreportpath = "/Users/divyajyoti/Desktop/tfl_automation/Reports/ExtentReport.html";

        public HooksIntialize(ScenarioContext scenariocontext) => _scenariocontext = scenariocontext;

        [BeforeScenario]
        [Obsolete]
        public void BeforeScenario()
        {
            SeleniumDriver seleniumDriver = new SeleniumDriver(_scenariocontext);
            _scenariocontext.Set(seleniumDriver, "SeleniumDriver");
            scenario = feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);


        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("Selenium Driver quit");
         _scenariocontext.Get<IWebDriver>("WebDriver").Quit();

        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var report = new ExtentHtmlReporter(testreportpath);
            extent = new ExtentReports();
            extent.AttachReporter(report);
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }

        [BeforeFeature]
        [Obsolete]
        public static void BeforeFeature()
        {
            feature = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);

        }
   
        [AfterStep]
        public  void AfterStep()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if(ScenarioContext.Current.TestError == null)
            {
            if(stepType == "Given")
             scenario = feature.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
            else if (stepType == "When")
             scenario = feature.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
            else if (stepType == "Then")
             scenario = feature.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if(ScenarioContext.Current.TestError != null)
            {
            if(stepType == "Given")
             scenario = feature.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioStepContext.Current.TestError.InnerException);
            else if (stepType == "When")
             scenario = feature.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioStepContext.Current.TestError.InnerException);
            else if (stepType == "Then")
             scenario = feature.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioStepContext.Current.TestError.InnerException);
            }
           
        }



    }
}