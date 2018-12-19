using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Utils
{
    public static partial class SeleniumExtension
    {
        public static void Wait(this IWebDriver webdriver, Func<IWebDriver, bool> waitFunction, TimeSpan timeout)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webdriver, timeout);
            try
            {
                webDriverWait.Until(waitFunction);
            }
            catch(Exception e)
            {

            }
        }
        public static T Wait<T>(this IWebDriver webdriver, Func<IWebDriver, T> waitFunction, TimeSpan timeout)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webdriver, timeout);
            try
            {
                return webDriverWait.Until(waitFunction);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public static void WaitAlert(this IWebDriver webDriver, bool isPresent, TimeSpan timeout)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, timeout);
            webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertState(isPresent));
        }
        

        public static void WaitTopPage(this IWebDriver webDriver)
        {
            var script = "return (document.all ? document.scrollTop : window.pageYOffset);";
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(60));
            object outObject;
            try
            {
                webDriverWait.Until(d =>
                {
                    d.ExcuteJavascript(script, out outObject);
                    return Convert.ToInt32(outObject) == 0;
                });
            }
            catch
            {

            }
            
        }
        public static void WaitPageReady(this IWebDriver webdriver, TimeSpan timeout)
        {
            var script = "return document.readyState";
            WebDriverWait webDriverWait = new WebDriverWait(webdriver, timeout);
            object outObject;
            try
            {
                webDriverWait.Until(driver =>
                {
                    driver.ExcuteJavascript(script, out outObject);
                    if(outObject.ToString() == "complete")
                    {
                        return true;
                    }
                    return false;
                });
            }
            catch
            {

            }
        }

        public static bool IsBottomOfThePage(this IWebDriver webDriver)
        {
            //var script = @"if ((window.innerHeight + window.pageYOffset) >= document.body.offsetHeight) { return true; } else { return false; }";
            var script = @"if ((window.innerHeight + window.pageYOffset) >= document.body.scrollHeight * 0.8) { return true; } else { return false; }";
            object objectReturn = null;
            webDriver.ExcuteJavascript(script, out objectReturn);
            return Convert.ToBoolean(objectReturn);
        }

        

        public static IWebElement WaitToVisibleElement(this IWebDriver webdriver, By by, TimeSpan timeout)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webdriver, timeout);
            try
            {
                return webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            }
            catch
            {
                return null;
            }
                       
        }

        public static IWebElement WaitElementCanClickable(this IWebDriver webDriver, By by, TimeSpan timeout)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, timeout);
            try
            {
                return webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            }
            catch
            {
                return null;
            }
            
        }

        public static bool WaitElementDisapear(this IWebDriver webdriver, By by, TimeSpan timeout)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webdriver, timeout);
            try
            {
                return webDriverWait.Until(driver => {
                    try
                    {
                        var element = driver.FindElement(by);
                        if (element.Displayed == false) return true;
                        return false;
                    }
                    catch
                    {
                        return true;
                    }
                });
            }
            catch
            {
                return false;
            }
        }
        public static void WaitToVisibleElement(this IWebDriver webdriver, IWebElement webElement, TimeSpan timeout)
        {
            WebDriverWait webDriverWait = new WebDriverWait(webdriver, timeout);
            var script = $@"var elem = arguments[0],                 " +
                          "  box = elem.getBoundingClientRect(),    " +
                          "  cx = box.left + box.width / 2,         " +
                          "  cy = box.top + box.height / 2,         " +
                          "  e = document.elementFromPoint(cx, cy); " +
                          "for (; e; e = e.parentElement) {         " +
                          "  if (e === elem)                        " +
                          "    return true;                         " +
                          "}                                        " +
                          "return false;";
            try
            {
                webDriverWait.Until((d) =>
                {
                    var isVisibleInViewport = d.ExcuteJavascript(script, webElement);
                    return Convert.ToBoolean(isVisibleInViewport);
                });

            }
            catch
            {

            }
           


        }
        public static Rectangle BrowserInfo(this IWebDriver webDriver, bool isRefresh = false)
        {
            Point position;
            Size size;
            if(isRefresh == false && InfoOfBrowsers.ContainsKey(webDriver))
            {
                if(InfoOfBrowsers[webDriver].LocationOfBrowserOnScreen != default(Point) && InfoOfBrowsers[webDriver].SizeOfBrowser != default(Size))
                {
                    position = InfoOfBrowsers[webDriver].LocationOfBrowserOnScreen;
                    size = InfoOfBrowsers[webDriver].SizeOfBrowser;

                    return new Rectangle(position, size);
                }                             
            }
            position = webDriver.Manage().Window.Position;
            size = webDriver.Manage().Window.Size;
            if(!InfoOfBrowsers.ContainsKey(webDriver))
            {
                InfoOfBrowsers.TryAdd(webDriver, new Models.InfoOfBrowser()
                {
                    LocationOfBrowserOnScreen = position,
                    SizeOfBrowser = size
                });
            }
            else
            {
                InfoOfBrowsers[webDriver].LocationOfBrowserOnScreen = position;
                InfoOfBrowsers[webDriver].SizeOfBrowser = size;
            }
            
            return new Rectangle(position, size);
        }

        
        public static int HeightInnerHtml(this IWebDriver webDriver, bool isRefresh = false)
        {
            int height = 0;
            if(isRefresh == false && InfoOfBrowsers.ContainsKey(webDriver))
            {
                height = InfoOfBrowsers[webDriver].HeightInnerHtml;
                if (height != 0) return height;
            }

            string script = "return window.innerHeight";
            object outObject = null;
            webDriver.ExcuteJavascript(script, out outObject);
            height = Convert.ToInt32(outObject);

            if(!InfoOfBrowsers.ContainsKey(webDriver))
            {
                InfoOfBrowsers.TryAdd(webDriver, new Models.InfoOfBrowser()
                {
                    HeightInnerHtml = height
                });
            }
            else
            {
                InfoOfBrowsers[webDriver].HeightInnerHtml = height;
            }
            return height;
        }

        public static Point Location(this IWebDriver webdriver)
        {
            return webdriver.Manage().Window.Position;
        }

        public static Rectangle ElementInfoOnScreen(this IWebDriver webDriver, IWebElement webElement)
        {
            var size = webElement.Size;
            var location = ((ILocatable)webElement).LocationOnScreenOnceScrolledIntoView;
            var browserInfo = webDriver.BrowserInfo();
            var locationOfWebdriver = browserInfo.Location;
            var sizeOfBrowser = browserInfo.Size;
            var heightInnerHtml = webDriver.HeightInnerHtml();

           

            location.X += locationOfWebdriver.X;
            location.Y += (locationOfWebdriver.Y + (sizeOfBrowser.Height - heightInnerHtml));

            return new Rectangle(location, size);
        }

        public static void StopPageLoading(this IWebDriver webDriver)
        {
            webDriver.ExcuteJavascript("return window.stop");
        }

        public static List<System.Net.Cookie> GetCookie(this IWebDriver webdirver)
        {
            StopPageLoading(webdirver);
            var cookiesOnSelenium = webdirver.Manage().Cookies.AllCookies;
            List<System.Net.Cookie> cookies = new List<System.Net.Cookie>();

            foreach (var ck in cookiesOnSelenium)
            {
                System.Net.Cookie cookie = new System.Net.Cookie();
                cookie.Domain = ck.Domain;
                cookie.Name = ck.Name;
                cookie.Value = ck.Value;
                cookie.HttpOnly = ck.IsHttpOnly;
                cookie.Expires = ck.Expiry ?? DateTime.Now;
                cookie.Expired = ck.Expiry == default(DateTime) ? true : ck.Expiry < DateTime.Now;
                cookie.Path = ck.Path;
                cookies.Add(cookie);
            }

            return cookies;
        }
        
    }
}
