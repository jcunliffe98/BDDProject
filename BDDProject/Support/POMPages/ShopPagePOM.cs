using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages
{
    internal class ShopPagePOM
    {
        private IWebDriver _driver;
        private string _item;

        public ShopPagePOM(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement _addItemToCart => _driver.FindElement(By.CssSelector("[aria-label='Add “" + _item + "” to your cart']"));
        private IWebElement _viewCart => StaticHelpers.WaitForElement(_driver, By.CssSelector("a[title='View cart']"), 5);

        public void AddItem(string item)
        {
            _item = item;
            var actions = new Actions(_driver);

            actions.MoveToElement(_addItemToCart);
            actions.Perform();
            _addItemToCart.Click();

        }

        public void ViewCart()
        {
            _viewCart.Click();
        }
    }
}
