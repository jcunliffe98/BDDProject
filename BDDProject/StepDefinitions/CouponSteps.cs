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

        [When(@"I try to apply the coupon code '(.*)'")]
        public void WhenITryToApplyTheCouponCodeEdgewords(string coupon)
        {
            CartPagePOM cart = new CartPagePOM(_driver);
            cart.InputCoupon(coupon);
            cart.ApplyCoupon();

            Console.WriteLine("Coupon applied");

            cart.TakeCouponScreenshot(); //Take screenshot of coupon being applied
        }

        [Then(@"the coupon should apply a '(.*)' discount")]
        public void ThenTheTotalValueShouldBeCorrect_(string expectDiscount)
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

            cart.TakeCartTotalsScreenshot(); //Take screenshot of cart values

            decimal expectedDiscount = couponAmount / subTotalAmount * 100;

            Assert.That(couponAmount / subTotalAmount * 100 == couponDiscount, Is.True, "Coupon was expected to apply a " + couponDiscount + "% discount but instead applied a " + expectedDiscount + "% discount"); //Check coupon applies correct discount

            decimal expectedAmount = subTotalAmount - couponAmount;
            expectedAmount = expectedAmount + shippingCostAmount;

            Assert.That(expectedAmount == totalAmount, Is.True, "Expected amount was expected to be " + expectedAmount + " but was " + totalAmount); //Check that expected amount is equal to actual amount


        }
    }
}
