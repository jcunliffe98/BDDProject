using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Globalization;
using TechTalk.SpecFlow;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages;

namespace BDDProject.CouponSteps
{
    [Binding]
    public class CouponSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public CouponSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["mydriver"];
        }
        [Given(@"I login to my account using '(.*)' and password")]
        public void GivenIHaveLoggedIntoMyAccount(string username)
        {
            _driver.Url = TestContext.Parameters["url"]; //Retrieve URL from runsettings

            LoginPagePOM login = new LoginPagePOM(_driver);
            AccountPagePOM account = new AccountPagePOM(_driver);

            string password = TestContext.Parameters["password"]; //Retrieve password from runsettings

            login.Login(username, password);

            Console.WriteLine("Logged in successfully");
            account.TakeLoginConfirmationScreenshot(); //Take screenshot after logging in
        }

        [Given(@"I add a hat to my basket")]
        public void GivenIAddAHatToMyBasket()
        {
            NavPOM nav = new NavPOM(_driver);
            nav.NavigateToShop();

            ShopPagePOM shop = new ShopPagePOM(_driver);
            shop.DismissBanner();
            shop.AddItem();
            shop.ViewCart();

            Console.WriteLine("Item added to cart");

            CartPagePOM cart = new CartPagePOM(_driver);
            cart.TakeCartScreenshot(); //Take screenshot of current cart
        }

        [When(@"I try to apply the coupon code '(.*)'")]
        public void WhenITryToApplyTheCouponCodeEdgewords(string coupon)
        {
            CartPagePOM cart = new CartPagePOM(_driver);

            cart.InputCoupon(coupon);
            cart.ApplyCoupon();

            Console.WriteLine("Coupon applied");
            cart.TakeCouponScreenshot(); //Take screenshot of coupon being applied
        }

        [Then(@"the total value should be correct")]
        public void ThenTheTotalValueShouldBeCorrect_()
        {
            CartPagePOM cart = new CartPagePOM(_driver);

            string totalString = cart.ReturnTotal(); //Gets total after coupon is applied
            string subTotalString = cart.ReturnSubTotal(); //Gets total before coupon is applied
            string couponString = cart.ReturnCoupon(); //Gets discount amount from coupon being applied
            string shippingCostString = cart.ReturnShippingCost(); //Gets shipping cost

            NumberFormatInfo FormatInfo = new NumberFormatInfo();
            FormatInfo.CurrencySymbol = "£"; //Settings to convert to decimal from a string containing a currency

            //Convert string values to decimals
            decimal totalAmount = decimal.Parse(totalString, NumberStyles.Currency, FormatInfo);
            decimal subTotalAmount = decimal.Parse(subTotalString, NumberStyles.Currency, FormatInfo);
            decimal couponAmount = decimal.Parse(couponString, NumberStyles.Currency, FormatInfo);
            decimal shippingCostAmount = decimal.Parse(shippingCostString, NumberStyles.Currency, FormatInfo);

            int couponDiscount = 15; //Discount amount from coupon as percentage


            Assert.That(couponAmount / subTotalAmount * 100 == couponDiscount, Is.True, "Coupon discount is incorrect"); //Check coupon applies correct discount

            decimal expectedAmount = subTotalAmount - couponAmount;
            expectedAmount = expectedAmount + shippingCostAmount;

            Assert.That(expectedAmount == totalAmount, Is.True, "Expected amount does not match total amount"); //Check that expected amount is equal to actual amount

            cart.TakeCartTotalsScreenshot(); //Take screenshot of cart values

        }
    }
}
