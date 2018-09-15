using MyWebDriver.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Utils
{
    public static partial class SeleniumExtension
    {
        private static ConcurrentDictionary<IWebDriver, InfoOfBrowser> InfoOfBrowsers = new ConcurrentDictionary<IWebDriver, InfoOfBrowser>();
    }
}
