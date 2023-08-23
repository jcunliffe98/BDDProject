using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities;

namespace BDDProject.PlaceOrderSteps
{
    [Binding]
    public class PlaceOrderSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public PlaceOrderSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["mydriver"];
        }
        [Given(@"I have logged in to my account using '(.*)' and password")]
        public void GivenIHaveLoggedInToMyAccountUsingAnd(string username)
        {
            _driver.Url = TestContext.Parameters["url"]; //Retrieve URL from runsettings

            LoginPagePOM login = new LoginPagePOM(_driver);
            AccountPagePOM account = new AccountPagePOM(_driver);

            string password = TestContext.Parameters["password"]; //Retrieve password from runsettings
            login.Login(username, password);

            Console.WriteLine("Logged in successfully");
            account.TakeLoginConfirmationScreenshot(); //Take screenshot after logging in
        }

        [Given(@"I add a '(.*)' to my cart")]
        public void GivenIAddAItemToMyCart(string item)
        {
            NavPOM nav = new NavPOM(_driver);
            nav.NavigateToShop();

            ShopPagePOM shop = new ShopPagePOM(_driver);
            shop.DismissBanner();
            shop.AddItem(item);
            shop.ViewCart();

            Console.WriteLine("Item added to cart");

            CartPagePOM cart = new CartPagePOM(_driver);
            cart.TakeCartScreenshot(); //Take screenshot of current cart
        }

        [When(@"I input my address")]
        public void WhenIInputMyAddress()
        {
            CartPagePOM cart = new CartPagePOM(_driver);
            cart.ProceedToCheckout();

            BillingPagePOM billing = new BillingPagePOM(_driver);
            billing.FillBillingInfo("Jack", "Cunliffe", "24 London Street", "London", "SW1A 0AA", "020 7219 4272");

            Console.WriteLine("Address filled in");

            billing.TakeBillingScreenshot(); //Take screenshot of filled in info
        }

        [When(@"I place the order")]
        public void WhenPlaceTheOrder()
        {
            BillingPagePOM billing = new BillingPagePOM(_driver);
            billing.PlaceOrder();

            StaticHelpers.WaitForElement(_driver, By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__date.date > strong"), 3);

            Console.WriteLine("Order placed");

            OrderConfirmationPOM orderConfirmation = new OrderConfirmationPOM(_driver);
            orderConfirmation.TakeOrderDetailsScreenshot(); //Take screenshot of order details
        }

        [Then(@"the order is confirmed")]
        public void ThenTheOrderIsConfirmed()
        {
            OrderConfirmationPOM orderConfirmation = new OrderConfirmationPOM(_driver);
            AccountPagePOM account = new AccountPagePOM(_driver);
            NavPOM navigation = new NavPOM(_driver);

            orderConfirmation.TakeOrderNumberScreenshot(); //Take screenshot of order number
            string orderNumber = orderConfirmation.RetrieveOrderNumber(); //Get order number
            orderNumber = "#" + orderNumber; //Append # to start of order number
            navigation.NavigateToMyAccount();
            account.SelectOrders();
            string orderHistoryNumber = account.ReturnOrderHistoryNumber(); //Return order number value in account history

            Assert.That(orderNumber, Is.EqualTo(orderHistoryNumber), "Latest order history number does not match just placed order"); //Check if order number from history matches order number from earlier

            Console.WriteLine("Latest order history number matches just placed order");
            account.TakeMostRecentOrderScreenshot(); //Take screenshot of most recent order

        }
    }
}
