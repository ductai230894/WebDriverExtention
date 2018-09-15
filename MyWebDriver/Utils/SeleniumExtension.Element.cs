using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Utils
{
    public static partial class SeleniumExtension
    {
        public static IWebElement HaveElement(this IWebDriver webdriver, By by)
        {
            try
            {
                return webdriver.FindElement(by);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsVisibleInViewport(this IWebElement webElement)
        {
            var driver = webElement.GetWebDriverFromElement();
            var script = $@"var elem = arguments[0],                 " +
                          "  box = elem.getBoundingClientRect(),    " +
                          "  cx = box.left + box.width / 2,         " +
                          "  cy = box.top + box.height / 2,         " +
                          "  e = document.elementFromPoint(cx, cy); " +
                          "for (; e; e = e.parentElement) {         " +
                          "  if (e === elem)                        " +
                          "    return true;                         " +
                          "}                                        " +
                          "return false;"       ;

            var isVisibleInViewport =  driver.ExcuteJavascript(script, webElement);

            return Convert.ToBoolean(isVisibleInViewport);
        }


        public static void ForceClick(this IWebElement webElement)
        {
            var element = webElement;
            var driver = element.GetWebDriverFromElement();
            
            int maxClickCount = 3;
            while (maxClickCount-- > 0)
            {
                try
                {
                    element.Click();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.Wait((drv) => drv.IsHaveScrollBar(), TimeSpan.FromSeconds(30));
                    return;
                }
                catch(Exception e)
                {
                    element = element.FindElement(By.XPath(".."));                 
                    driver.ScrollToView(element);
                }
            }
        }

        public static IWebDriver GetWebDriverFromElement(this IWebElement element)
        {
            IWebDriver driver = null;

            if (element.GetType().ToString() == "OpenQA.Selenium.Support.PageObjects.WebElementProxy")
            {
                driver = ((IWrapsDriver)element
                                 .GetType().GetProperty("WrappedElement")
                                 .GetValue(element, null)).WrappedDriver;
            }
            else
            {
                driver = ((IWrapsDriver)element).WrappedDriver;
            }
            return driver;
        }

        public static void SendText(this IWebElement webElement, string text)
        {
            foreach(var c in text)
            {
                webElement.SendKeys(c.ToString());
                Task.Delay(200).Wait();
            }
        }
    }
}
