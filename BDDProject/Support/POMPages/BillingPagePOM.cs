using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages
{
    internal class BillingPagePOM
    {
        private IWebDriver _driver;

        public BillingPagePOM(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement _firstNameTextBox => _driver.FindElement(By.CssSelector("#billing_first_name"));
        private IWebElement _lastNameTextBox => _driver.FindElement(By.CssSelector("#billing_last_name"));
        private IWebElement _addressTextBox => _driver.FindElement(By.CssSelector("#billing_address_1"));
        private IWebElement _cityTextBox => _driver.FindElement(By.CssSelector("#billing_city"));

        private IWebElement _postcodeTextBox => _driver.FindElement(By.CssSelector("#billing_postcode"));
        private IWebElement _phoneTextBox => _driver.FindElement(By.CssSelector("#billing_phone"));
        private IWebElement _placeOrderTextBox
        {
            get
            {
                StaticHelpers.WaitForElement(_driver, By.CssSelector("#place_order"), 5);
                return _driver.FindElement(By.CssSelector("#place_order"));
            }
        }
        private IWebElement _billingInfo => _driver.FindElement(By.CssSelector("#customer_details > div.col-1 > div"));

        public void FillBillingInfo(string firstName, string lastName, string address, string city, string postcode, string phoneNumber)
        {
            var actions = new Actions(_driver);
            actions.MoveToElement(_phoneTextBox);
            actions.Perform();
            FillName(firstName, lastName);
            FillAddress(address, city, postcode, phoneNumber);
        }

        public void FillName(string firstName, string lastName)
        {
            _firstNameTextBox.Clear();
            _firstNameTextBox.SendKeys(firstName);

            _lastNameTextBox.Clear();
            _lastNameTextBox.SendKeys(lastName);
        }

        public void FillAddress(string address, string city, string postcode, string phoneNumber)
        {
            _addressTextBox.Clear();
            _addressTextBox.SendKeys(address);

            _cityTextBox.Clear();
            _cityTextBox.SendKeys(city);

            _postcodeTextBox.Clear();
            _postcodeTextBox.SendKeys(postcode);

            _phoneTextBox.Clear();
            _phoneTextBox.SendKeys(phoneNumber);
        }
        public void PlaceOrder()
        {
            _placeOrderTextBox.Click();
        }
        public void TakeBillingScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _billingInfo, "billing.png");
        }
    }
}
