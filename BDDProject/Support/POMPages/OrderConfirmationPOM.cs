using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages
{
    internal class OrderConfirmationPOM
    {
        private IWebDriver _driver;

        public OrderConfirmationPOM(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement _orderNumber => _driver.FindElement(By.CssSelector("li.woocommerce-order-overview__order.order > strong"));
        private IWebElement _orderDetails => _driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul"));

        public void TakeOrderNumberScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _orderNumber, "orderNumber.png");
        }
        public void TakeOrderDetailsScreenshot()
        {
            StaticHelpers.TakeScreenshot(_driver, _orderDetails, "orderDetails.png");
        }

        public string RetrieveOrderNumber()
        {
            string orderNumberText = _orderNumber.Text;
            return orderNumberText;
        }
    }
}
