using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace tfl_automation.Drivers
{
    public class SeleniumDriver
    {
        private IWebDriver driver;
    
        private readonly ScenarioContext _scenarioContext;

        public SeleniumDriver(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

      
        public IWebDriver Setup()
        {
            var options = new ChromeOptions();
             options.AddArgument("-no-sandbox");
            driver = new ChromeDriver("/Users/divyajyoti/Desktop/tfl_automation/Drivers/chromedriver", options,TimeSpan.FromSeconds(180));
            _scenarioContext.Set(driver, "WebDriver");
            driver.Manage().Window.Maximize();
            return driver;
        }

  

    }
}
