﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages
{
    internal class AccountPagePOM
    {
        private IWebDriver _driver;

        public AccountPagePOM(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement _ordersButton => _driver.FindElement(By.LinkText("Orders"));
        private IWebElement _recentOrderNumber => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1) > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));
        private IWebElement _loginConfirmation => _driver.FindElement(By.CssSelector("#post-7"));
        private IWebElement _mostRecentOrder => _driver.FindElement(By.CssSelector("tbody > tr:nth-of-type(1)"));

        public void SelectOrders()
        {
            _ordersButton.Click();
        }

        public string ReturnOrderHistoryNumber()
        {
            return _recentOrderNumber.Text;
        }
        public void TakeLoginConfirmationScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _loginConfirmation, "loginConfirmation.png");
        }
        public void TakeMostRecentOrderScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _mostRecentOrder, "mostRecentOrder.png");
        }
    }
}
