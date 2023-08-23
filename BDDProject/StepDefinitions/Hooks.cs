using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(8)]

namespace BDDProject.Hooks
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Before("@Tests")]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _scenarioContext["mydriver"] = _driver;

        }


        [After("@Tests")]
        public void TearDown()
        {
            NavPOM nav = new NavPOM(_driver);

            nav.NavigateToMyAccount();
            nav.LogOut();

            Console.WriteLine("Logged out successfully");
            nav.TakeLogoutScreenshot();

            Thread.Sleep(3000); //To see the result
            _driver.Quit();
        }
    }
}
