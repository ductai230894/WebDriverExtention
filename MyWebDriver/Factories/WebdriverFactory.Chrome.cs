using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Factories
{
    public partial class WebdriverFactory
    {
        public static ChromeDriver CreateGoogleChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = OpenQA.Selenium.PageLoadStrategy.None;
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
            options.AddArguments("disable-infobars");
            options.AddArgument("--disable-plugins"); // disable flash
            options.AddArgument("--log-level=3");           
            //options.AddArgument("--test-type");
            //options.AddArgument("--silent"); // disable silent
            ChromeDriver chromeDriver = new ChromeDriver(options);
            return chromeDriver;


            return chromeDriver;
        }

        public static FirefoxDriver CreateFirefoxDriver()
        {
            var firefoxDriver = new FirefoxDriver();


            return firefoxDriver;
        }
    }
}
