using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities;

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
        private IWebElement _applyCoupon => _driver.FindElement(By.CssSelector("[name='apply_coupon']"));
        private IWebElement _couponDiscountText
        {
            get
            {
                StaticHelpers.WaitForElement(_driver, By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"), 5);
                return _driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));
            }
        }
        private IWebElement _subTotalText
        {
            get
            {
                StaticHelpers.WaitForElement(_driver, By.CssSelector(".cart-subtotal .woocommerce-Price-amount"), 5);
                return _driver.FindElement(By.CssSelector(".cart-subtotal .woocommerce-Price-amount"));
            }
        }
        private IWebElement _shippingCostText
        {
            get
            {
                StaticHelpers.WaitForElement(_driver, By.CssSelector("#shipping_method > li > label > span"), 5);
                return _driver.FindElement(By.CssSelector("#shipping_method > li > label > span"));
            }
        }
        private IWebElement _totalText
        {
            get
            {
                StaticHelpers.WaitForElement(_driver, By.CssSelector("strong > .amount.woocommerce-Price-amount"), 5);
                return _driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount"));
            }
        }
        private IWebElement _proceedToCheckout => _driver.FindElement(By.CssSelector(".checkout-button"));
        private IWebElement _cart => _driver.FindElement(By.CssSelector("#post-5 > div > div > form"));
        private IWebElement _couponConfirmation => _driver.FindElement(By.CssSelector("[role='alert']"));
        private IWebElement _cartTotals => _driver.FindElement(By.CssSelector(".cart_totals"));

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
            return _couponDiscountText.Text.ToString();
        }
        public string ReturnTotal()
        {
            return _totalText.Text.ToString();
        }
        public string ReturnSubTotal()
        {
            return _subTotalText.Text.ToString();
        }
        public string ReturnShippingCost()
        {
            return _shippingCostText.Text.ToString();
        }

        public void ProceedToCheckout()
        {
            var actions = new Actions(_driver);
            actions.MoveToElement(_proceedToCheckout);
            actions.Perform();
            _proceedToCheckout.Click();
        }
        public void TakeCartScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _cart, "cart.png");
        }
        public void TakeCouponScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _couponConfirmation, "coupon.png");
        }
        public void TakeCartTotalsScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _cartTotals, "cartTotals.png");
        }

    }
}

