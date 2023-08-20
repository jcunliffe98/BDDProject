using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages
{
    internal class CartPagePOM
    {
        private IWebDriver _driver;

        public CartPagePOM(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement _couponTextBox => _driver.FindElement(By.CssSelector("#coupon_code"));
        private IWebElement _applyCoupon => _driver.FindElement(By.CssSelector("#post-5 > div > div > form > table > tbody > tr:nth-child(2) > td > div > button"));
        private IWebElement _couponDiscountText;
        private IWebElement _subTotalText;
        private IWebElement _shippingCostText;
        private IWebElement _totalText;
        private IWebElement _proceedToCheckout => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > div > a"));

        public void InputCoupon(string coupon)
        {
            _couponTextBox.Click();
            _couponTextBox.Clear();
            _couponTextBox.SendKeys(coupon);
        }

        public void ApplyCoupon()
        {
            _applyCoupon.Click();
            Thread.Sleep(3000);
            //Sleep to avoid the animation
        }

        public string ReturnCoupon()
        {
            _couponDiscountText = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span"));
            return _couponDiscountText.Text.ToString();
        }
        public string ReturnTotal()
        {
            _totalText = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span"));
            return _totalText.Text.ToString();
        }
        public string ReturnSubTotal()
        {
            _subTotalText = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span"));
            return _subTotalText.Text.ToString();
        }
        public string ReturnShippingCost()
        {
            _shippingCostText = _driver.FindElement(By.CssSelector("#shipping_method > li > label > span"));
            return _shippingCostText.Text.ToString();
        }

        public void ProceedToCheckout()
        {
            var actions = new Actions(_driver);
            actions.MoveToElement(_proceedToCheckout);
            actions.Perform();
            _proceedToCheckout.Click();
        }

        public void ScrollToTop()
        {
            var actions = new Actions(_driver);
            actions.ScrollByAmount(0, -500);
            actions.Perform();
        }
    }
}

