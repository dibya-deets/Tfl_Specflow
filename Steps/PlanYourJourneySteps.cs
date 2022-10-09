using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium;
using static System.Net.WebRequestMethods;
using System;
using OpenQA.Selenium.Chrome;
using tfl_automation.Drivers;
using System.Collections.Generic;

namespace tfl_automation.Steps
{
    [Binding]
    public sealed class PlanyourjourneySteps
    {
        IWebDriver driver;
        private ScenarioContext _scenarioContext;

        public PlanyourjourneySteps(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;
        [Given(@"user is on tfl site")]
        public void GivenUserisontflsite(){

            driver = _scenarioContext.Get<SeleniumDriver>("SeleniumDriver").Setup();
            driver.Navigate().GoToUrl("https://tfl.gov.uk/");
            driver.FindElement(By.XPath("//button[@id=\"CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll\"]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
            driver.FindElement(By.XPath("//button[@onclick=\"endCookieProcess(); return false;\"]")).Click();


        }

          [When(@"user selects plan a journey option")]
        public void check1(){
            driver.FindElement(By.XPath("(//li[@class=\"plan-journey\"])[1]")).Click();

        }
          [Then(@"he should lands on plan a journey widget")]
        public void check2(){
            String title = driver.Title;
            Console.WriteLine("PAGE TITLE IS:" + title);
        }
          [When(@"he enters from_address as (.*)")]
        public void check3(string get_fromAddress){

            driver.FindElement(By.XPath("//input[@id=\"InputFrom\"]")).SendKeys(get_fromAddress);
            driver.FindElement(By.XPath("//input[@id=\"InputFrom\"]")).SendKeys(Keys.Enter);

        }
        [When(@"he enters to_address as (.*)")]
        public void check6(string get_toAddress){

            driver.FindElement(By.XPath("//input[@id=\"InputTo\"]")).SendKeys(get_toAddress);
            driver.FindElement(By.XPath("//input[@id=\"InputTo\"]")).SendKeys(Keys.Enter);

        }

        [When(@"he leaves from_address and to_address blank")]
         public void leaveAddressBlank(){
              driver.FindElement(By.XPath("//input[@id=\"InputTo\"]")).SendKeys("");
            driver.FindElement(By.XPath("//input[@id=\"InputTo\"]")).SendKeys(Keys.Enter);
               driver.FindElement(By.XPath("//input[@id=\"InputFrom\"]")).SendKeys("");
            driver.FindElement(By.XPath("//input[@id=\"InputFrom\"]")).SendKeys(Keys.Enter);
         }
        [Then(@"he clicks on plan a journey button")]
        public void check4(){
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
             //  driver.FindElement(By.Id("plan-journey-button")).Submit();
        IWebElement element = driver.FindElement(By.XPath("//input[@class=\"primary-button plan-journey-button\"][@data-send=\"true\"]"));
             IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
             js.ExecuteScript("arguments[0].scrollIntoView();",element);
                js.ExecuteScript("arguments[0].click();", element);
        }
        [Then(@"he should be able to verify his journey result")]
          public void check5(){
            String title = driver.Title;
            Console.WriteLine("You are on page: " + title);

            List <IWebElement> elementList = new List <IWebElement>();
            elementList.AddRange(driver.FindElements(By.XPath("//li[@class=\"field-validation-error\"]")));

          if(elementList.Count > 0)
           {
            string getErrorText = driver.FindElement(By.XPath("//li[@class=\"field-validation-error\"]")).Text;
            Assert.AreEqual(getErrorText,"Sorry, we can't find a journey matching your criteria", "Error");
          
          }    
          else{
             Console.WriteLine("User have entered valid address value" );
           }
          
        }

        [Then(@"user receives an error message")]
        public void checkforEmptyAddressField(){
           string getFromErrorText = driver.FindElement(By.XPath("//span[@class=\"field-validation-error\"][@data-valmsg-for=\"InputFrom\"]/span")).Text;
             Console.WriteLine(getFromErrorText);
           string getToErrorText = driver.FindElement(By.XPath("//span[@class=\"field-validation-error\"][@data-valmsg-for=\"InputTo\"]/span")).Text;
             Console.WriteLine(getToErrorText);
        }

        [When(@"he clicks on edit journey option")]
        public void selectEditJourneyOption(){
          driver.FindElement(By.XPath("//a[@class=\"edit-journey\"]")).Click();
        }


        [Then(@"he navigates to Edit journey page and enterd new from_address")]
        public void EditJourneypage(){
            driver.FindElement(By.XPath("(//a[@class=\"remove-content hide-text\"])[2]")).Click();
            driver.FindElement(By.Id("InputFrom")).SendKeys("Maryland");
           driver.FindElement(By.Id("plan-journey-button")).Click();
        }


        [Then(@"he should see the updated journey result")]
        public void VerifyEditJourneyResult(){
         string get_fromAddress =  driver.FindElement(By.XPath("(//span[@class=\"notranslate\"])[1]")).Text;
         Assert.AreEqual(get_fromAddress,"Maryland", "updated address is not visible");
        }


        [When(@"user selects recent option")]
        public void selectRecentOption(){
          driver.FindElement(By.Id("jp-recent-tab-jp")).Click();
        }

        [Then(@"he should see list of recent searched journey")]
        public void listRecentSearch(){

             List <IWebElement> ele = new List <IWebElement>();
            ele.AddRange(driver.FindElements(By.XPath("//a[@class=\"plain-button journey-item\"]")));
            Console.WriteLine("element count is:" +ele.Count);
          if(ele.Count > 0)
           {
             for(int i=0;i<ele.Count;i++)
              {
                Console.WriteLine(ele[i].Text);
              }
          
          }    
          else{
             Console.WriteLine("No Recent Search" );
           }
        }
     
     [Then(@"navigate to plan a journey page")]
     public void NavigateToPlanJpourneyPage(){
            String title = driver.Title;
            Console.WriteLine("PAGE TITLE IS:" + title);
        }
    }
}

