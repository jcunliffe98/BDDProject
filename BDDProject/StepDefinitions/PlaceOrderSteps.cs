using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void GivenIHaveLoggedIntoMyAccount()
        {
            _driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            LoginPagePOM login = new LoginPagePOM(_driver);
            login.Login("jack.cunliffe@nfocus.co.uk", "Mu3Wbu!AstG!!6Z");
        }

        [Given(@"I add a hat to my cart")]
        public void GivenIAddAHatToMyCart()
        {
            AccountPagePOM account = new AccountPagePOM(_driver);
            account.SelectShop();

            ShopPagePOM shop = new ShopPagePOM(_driver);
            shop.DismissBanner();
            shop.AddItem();
            shop.ViewCart();
        }

        [When(@"I input my address")]
        public void WhenIInputMyAddress()
        {
            CartPagePOM cart = new CartPagePOM(_driver);
            cart.ProceedToCheckout();

            BillingPagePOM billing = new BillingPagePOM(_driver);
            billing.FillBillingInfo("Jack", "Cunliffe", "24 London Street", "London", "SW1A 0AA", "020 7219 4272");
        }

        [When(@"I place the order")]
        public void WhenPlaceTheOrder()
        {
            BillingPagePOM billing = new BillingPagePOM(_driver);
            billing.PlaceOrder();

            StaticHelpers.WaitForElement(_driver, By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__date.date > strong"), 3);
            //Can't get this to work ^^^

            //Thread.Sleep(3000);
        }

        [Then(@"the order is placed")]
        public void ThenTheOrderIsPlaced()
        {
            OrderConfirmationPOM orderConfirmation = new OrderConfirmationPOM(_driver);
            AccountPagePOM account = new AccountPagePOM(_driver);
            NavPOM navigation = new NavPOM(_driver);

            orderConfirmation.TakeOrderNumberScreenshot();
            string orderNumber = orderConfirmation.RetrieveOrderNumber();
            orderNumber = "#" + orderNumber;
            navigation.NavigateToMyAccount();
            account.SelectOrders();
            string orderHistoryNumber = account.ReturnOrderHistoryNumber();

            Assert.That(orderNumber, Is.EqualTo(orderHistoryNumber), "Latest order history number does not match just placed order");

        }
    }
}
