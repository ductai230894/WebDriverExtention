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
        public static void ExcuteJavascript(this IWebDriver webdriver, string script, out object objectReturn)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webdriver;
            objectReturn = js.ExecuteScript(script);
        }
        public static void ExcuteJavascript(this IWebDriver webdriver, string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webdriver;
            js.ExecuteScript(script);
        }

        public static object ExcuteJavascript(this IWebDriver webDriver, string script, params object[] args)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            return js.ExecuteScript(script, args);
            
        }
    }
}
