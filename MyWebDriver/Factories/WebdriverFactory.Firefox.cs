using MyWebDriver.Models;
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
        public static FirefoxDriver CreateFirefoxDriver()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();

            var firefoxDriver = new FirefoxDriver();
            


            return firefoxDriver;
        }

        public static FirefoxDriver CreateFirefoxDriver(DriverOptionsConfig driverOptionsConfig)
        {
            FirefoxProfile firefoxProfile = new FirefoxProfile();          
            InitParameterFirefoxOption(firefoxProfile, driverOptionsConfig);
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            if(driverOptionsConfig.IsHeadless)
            {
                firefoxOptions.AddArgument("--headless");
            }
            firefoxOptions.PageLoadStrategy = driverOptionsConfig.PageLoadStrategy;
            firefoxOptions.Profile = firefoxProfile;
            var firefoxDriver = new FirefoxDriver(firefoxOptions);
            return firefoxDriver;
        }

        private static void InitParameterFirefoxOption(FirefoxProfile firefoxProfile, DriverOptionsConfig driverOptionsConfig)
        {
            if(driverOptionsConfig.IsDisableImage)
            {
                firefoxProfile.SetPreference("permissions.default.image", 2);
            }
            if(driverOptionsConfig.IsDisableCss)
            {
                firefoxProfile.SetPreference("permissions.default.stylesheet", 2);
            }
            if(driverOptionsConfig.IsDisableNotify)
            {
                firefoxProfile.SetPreference("dom.webnotifications.enabled", false);
            }
            if(driverOptionsConfig.IsDisableFont)
            {
                firefoxProfile.SetPreference("gfx.downloadable_fonts.enabled", false);
            }
            if(driverOptionsConfig.IsDisableFlash)
            {
                firefoxProfile.SetPreference("dom.ipc.plugins.enabled.libflashplayer.so",
                                  false);
            }

            // Work around for FireFox not closing, fix comes from here: https://github.com/mozilla/geckodriver/issues/517
            firefoxProfile.SetPreference("browser.tabs.remote.autostart", false);
            firefoxProfile.SetPreference("browser.tabs.remote.autostart.1", false);
            firefoxProfile.SetPreference("browser.tabs.remote.autostart.2", false);
            firefoxProfile.SetPreference("browser.tabs.remote.force-enable", false);

        }

        public static FirefoxDriver CreateFirefoxDriverWithDefaultProfile()
        {
            var profileManager = new FirefoxProfileManager();
            FirefoxProfile profile = profileManager.GetProfile("default");
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.Profile = profile;
            var firefoxDriver = new FirefoxDriver(firefoxOptions);
            return firefoxDriver;
        }
        public static FirefoxDriver CreateFirefoxDriver(string profilePath, DriverOptionsConfig driverOptionsConfig)
        {
            //var profileManager = new FirefoxProfileManager();
            //FirefoxProfile profile = profileManager.GetProfile("default");

            var profile = new FirefoxProfile(profilePath);       
            InitParameterFirefoxOption(profile, driverOptionsConfig);
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.Profile = profile;
            var firefoxDriver = new FirefoxDriver(firefoxOptions);

            profile.WriteToDisk();
            return firefoxDriver;
        }
    }
}
