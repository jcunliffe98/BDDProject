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

        [When(@"I input my billing details")]
        public void WhenIInputMyBillingDetails(Table billingDetails)
        {
            CartPagePOM cart = new CartPagePOM(_driver);
            cart.ProceedToCheckout();
            BillingPagePOM billing = new BillingPagePOM(_driver);
            foreach (TableRow row in billingDetails.Rows)
            {
                string firstName = row["First Name"];
                string surname = row["Surname"];
                string street = row["Street"];
                string city = row["City"];
                string postcode = row["Postcode"];
                string telephone = row["Telephone"];
                billing.FillBillingInfo(firstName, surname, street, city, postcode, telephone);
            }
            
            Console.WriteLine("Address filled in");

            billing.TakeBillingScreenshot(); //Take screenshot of filled in info
        }

        [When(@"I place the order")]
        public void WhenIPlaceTheOrder()
        {
            BillingPagePOM billing = new BillingPagePOM(_driver);
            billing.PlaceOrder();

            Console.WriteLine("Order placed");

            OrderConfirmationPOM orderConfirmation = new OrderConfirmationPOM(_driver);
            orderConfirmation.TakeOrderDetailsScreenshot(); //Take screenshot of order details
        }

        [Then(@"the order is confirmed")]
        public void ThenTheOrderIsConfirmed()
        {
            OrderConfirmationPOM orderConfirmation = new OrderConfirmationPOM(_driver);
            
            orderConfirmation.TakeOrderNumberScreenshot(); //Take screenshot of order number
            string orderNumber = orderConfirmation.RetrieveOrderNumber(); //Get order number
            orderNumber = "#" + orderNumber; //Append # to start of order number

            NavPOM navigation = new NavPOM(_driver);
            navigation.NavigateToMyAccount();

            AccountPagePOM account = new AccountPagePOM(_driver);
            account.SelectOrders();

            string orderHistoryNumber = account.ReturnOrderHistoryNumber(); //Return order number value in account history

            try
            {
                Assert.That(orderNumber, Is.EqualTo(orderHistoryNumber), "Latest order history number does not match just placed order"); //Check if order number from history matches order number from earlier
            }
            catch (AssertionException e)
            {
                account.TakeMostRecentOrderScreenshot(); //Take screenshot of most recent order
                throw;
            }
            Console.WriteLine("Latest order history number matches just placed order");

        }
    }
}
