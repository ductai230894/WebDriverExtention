using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Utils
{
    public static partial class SeleniumExtension
    {
        public static void NavigateToUrl(this IWebDriver webdriver, string url, TimeSpan timeout)
        {
            try
            {
                webdriver.Navigate().GoToUrl(url);

                //webdriver.Wait((driver) =>
                //{
                //    return !driver.Title.StartsWith("Just a moment");
                //}, timeout);

                webdriver.Wait((driver) =>
                {
                    return driver.IsHaveScrollBar();
                }, timeout);
            }
            catch
            {
                return;
            }
        }

    }
}
