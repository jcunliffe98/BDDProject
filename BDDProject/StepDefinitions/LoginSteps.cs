using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages;

namespace BDDProject.LoginSteps
{
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["mydriver"];
        }
        [Given(@"I have logged in to my account using '(.*)' and password")]
        public void GivenIHaveLoggedInToMyAccountUsingAnd(string username)
        {
            string password = TestContext.Parameters["password"]; //Retrieve password from runsettings

            LoginPagePOM login = new LoginPagePOM(_driver);
            login.Login(username, password);

            Console.WriteLine("Logged in successfully");

            AccountPagePOM account = new AccountPagePOM(_driver);
            account.TakeLoginConfirmationScreenshot(); //Take screenshot after logging in
        }

        [Given(@"I add a '(.*)' to my cart")]
        public void GivenIAddAItemToMyCart(string item)
        {
            NavPOM nav = new NavPOM(_driver);
            nav.NavigateToShop();

            ShopPagePOM shop = new ShopPagePOM(_driver);
            shop.AddItem(item);
            shop.ViewCart();

            Console.WriteLine("Item added to cart");

            CartPagePOM cart = new CartPagePOM(_driver);
            cart.TakeCartScreenshot(); //Take screenshot of current cart
        }
    }
}
