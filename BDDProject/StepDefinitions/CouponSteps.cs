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
            _driver.Url = TestContext.Parameters["url"];

            LoginPagePOM login = new LoginPagePOM(_driver);
            AccountPagePOM account = new AccountPagePOM(_driver);

            string password = TestContext.Parameters["password"];

            login.Login(username, password);

            Console.WriteLine("Logged in successfully");
            account.TakeLoginConfirmationScreenshot();
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
            cart.TakeCartScreenshot();
        }

        [When(@"I try to apply the coupon code '(.*)'")]
        public void WhenITryToApplyTheCouponCodeEdgewords(string coupon)
        {
            CartPagePOM cart = new CartPagePOM(_driver);

            cart.InputCoupon(coupon);
            cart.ApplyCoupon();

            Console.WriteLine("Coupon applied");
            cart.TakeCouponScreenshot();
        }

        [Then(@"the total value should be correct")]
        public void ThenTheTotalValueShouldBeCorrect_()
        {
            CartPagePOM cart = new CartPagePOM(_driver);

            string totalString = cart.ReturnTotal();
            string subTotalString = cart.ReturnSubTotal();
            string couponString = cart.ReturnCoupon();
            string shippingCostString = cart.ReturnShippingCost();

            NumberFormatInfo FormatInfo = new NumberFormatInfo();
            FormatInfo.CurrencySymbol = "£";

            decimal totalAmount = decimal.Parse(totalString, NumberStyles.Currency, FormatInfo);
            decimal subTotalAmount = decimal.Parse(subTotalString, NumberStyles.Currency, FormatInfo);
            decimal couponAmount = decimal.Parse(couponString, NumberStyles.Currency, FormatInfo);
            decimal shippingCostAmount = decimal.Parse(shippingCostString, NumberStyles.Currency, FormatInfo);

            int couponDiscount = 15;


            Assert.That(couponAmount / subTotalAmount * 100 == couponDiscount, Is.True, "Coupon discount is incorrect");

            decimal expectedAmount = subTotalAmount - couponAmount;
            expectedAmount = expectedAmount + shippingCostAmount;

            Assert.That(expectedAmount == totalAmount, Is.True, "Expected amount does not match total amount");

            cart.TakeCartTotalsScreenshot();

        }
    }
}
