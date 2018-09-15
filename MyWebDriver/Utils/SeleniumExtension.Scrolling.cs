using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Utils
{
    public static partial class SeleniumExtension
    {
        public static bool IsHaveScrollBar(this IWebDriver driver)
        {
            var script = "return document.documentElement.scrollHeight>document.documentElement.clientHeight;";
            object outObject;
            driver.ExcuteJavascript(script, out outObject);
            return Convert.ToBoolean(outObject);
        }
        public static void ScrollTo(this IWebDriver driver, int xPosition = 0, int yPosition = 0)
        {
            //var js = String.Format(@"window.scrollTo({0}, {1})", xPosition, yPosition);
            var js = $@"window.scrollTo({{
                                        top : {yPosition},
                                        behavior: 'smooth'
                                    }})";
            driver.ExcuteJavascript(js);
            Task.Delay(1000).Wait();
        }

        public static IWebElement ScrollToView(this IWebDriver driver, By selector)
        {
            var element = driver.FindElement(selector);
            driver.ScrollToView(element);
            return element;
        }

        public static void ScrollToView(this IWebDriver driver, IWebElement element)
        {
            //var js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'})", element);
            ////driver.WaitToElementInViewPort(element, TimeSpan.FromSeconds(30));
            ///
            driver.ExcuteJavascript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'})", element);
            driver.WaitToElementInViewPort(element, TimeSpan.FromMinutes(1));
            Task.Delay(1000).Wait();

        }
        public static void WaitToElementInViewPort(this IWebDriver driver , IWebElement element, TimeSpan timeout)
        {
            ILocatable location = element as ILocatable;
            WebDriverWait webDriverWait = new WebDriverWait(driver, timeout);
            try
            {
                webDriverWait.Until(d => {
                    try
                    {
                        var locationInViewPort = location.LocationOnScreenOnceScrolledIntoView;
                        return true;
                    }
                    catch(Exception e)
                    {
                        return false;
                    }
                });
            }
            catch
            {

            }
        }
    }
}
