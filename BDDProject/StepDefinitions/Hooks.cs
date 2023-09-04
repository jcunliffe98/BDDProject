using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

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

        [Before]
        public void SetUp()
        {
            string Browser = Environment.GetEnvironmentVariable("BROWSER");

            switch (Browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                case "ie":
                    _driver = new InternetExplorerDriver();
                    break;
                case "remotechrome":
                    ChromeOptions options = new ChromeOptions();
                    _driver = new RemoteWebDriver(new Uri("http://172.30.190.244:4444/wd/hub"), options);
                    break;
                default:
                    Console.WriteLine("No browser set - launching chrome");
                    _driver = new ChromeDriver();
                    break;
            };

            _driver.Manage().Window.Maximize();
            _scenarioContext["mydriver"] = _driver;
            _driver.Url = TestContext.Parameters["url"]; //Retrieve URL from runsettings

            NavPOM nav = new NavPOM(_driver);
            nav.DismissBanner();
        }


        [After]
        public void TearDown()
        {
            NavPOM nav = new NavPOM(_driver);

            nav.NavigateToMyAccount();

            AccountPagePOM account = new AccountPagePOM(_driver);
            account.LogOut();

            Console.WriteLine("Logged out successfully");
            account.TakeLogoutScreenshot();

            Thread.Sleep(3000); //To see the result
            _driver.Quit();
        }
    }
}
