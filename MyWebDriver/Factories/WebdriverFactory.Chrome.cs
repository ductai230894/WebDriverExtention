using MyWebDriver.Models;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Factories
{
    public partial class WebdriverFactory
    {
        private static void InitParameterChromeOption(ChromeOptions options, ChromeOptionConfig chromeOptionConfig)
        {
            if(chromeOptionConfig.IsDisableFlash)
            {
                options.AddArguments("--disable-bundled-ppapi-flash");
            }
            if(chromeOptionConfig.IsDisableFont)
            {
                options.AddArguments("--disable-remote-fonts");
            }
            if(chromeOptionConfig.IsDisableImage)
            {
                options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            }
            if(chromeOptionConfig.IsHeadless)
            {
                options.AddArgument("--headless");
            }
            if(chromeOptionConfig.IsDisableNotify)
            {
                options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
                options.AddArgument("--disable-notifications");
            }
            if(chromeOptionConfig.IsDisableCss)
            {
                options.AddUserProfilePreference("profile.default_content_setting_values.stylesheet", 2);
            }
            if(chromeOptionConfig.NullProxy == true)
            {
                options.Proxy = null;
            }
            else if (!String.IsNullOrEmpty(chromeOptionConfig.IpProxy) && chromeOptionConfig.PortProxy != 0)
            {
                if (chromeOptionConfig.IsSocks5Proxy)
                    options.AddArguments("--proxy-server=socks5://" + chromeOptionConfig.IpProxy + ":" + chromeOptionConfig.PortProxy);
                else
                    options.AddArguments("--proxy-server=http://" + chromeOptionConfig.IpProxy + ":" + chromeOptionConfig.PortProxy);
                options.AddArgument("ignore-certificate-errors");
            }
            if(!string.IsNullOrEmpty(chromeOptionConfig.UserAgent))
            {
                options.AddArguments($"user-agent={chromeOptionConfig.UserAgent}");
            }
            options.AddArguments("--no-proxy-server");
            options.AddArguments("--ignore-certificate-errors-spki-list");
            options.AddArguments("--ignore-ssl-errors");
            options.AddArguments("disable-infobars");
            options.AddArgument("--disable-plugins"); // disable flash
            options.AddArgument("--log-level=3");
            options.AddUserProfilePreference("disable-save-password-bubble", true);
        }
       [Obsolete("Function này hiện tại không còn hỗ trợ")]
        private static void InitParameterChromeOption(ChromeOptions options, bool hidden = false)
        {
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
            options.AddArguments("disable-infobars");
            options.AddArgument("--disable-plugins"); // disable flash
            options.AddArgument("--log-level=3");

            if(hidden == true)
            {
                options.AddArgument("--headless");
            }
        }

        public static ChromeDriver CreateChromeDriver(ChromeOptionConfig chromeOptionConfig)
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = chromeOptionConfig.PageLoadStrategy;
            InitParameterChromeOption(options, chromeOptionConfig);

            //hide cammand
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = chromeOptionConfig.IsHiddenCommandLine;
            
            ChromeDriver chromeDriver = new ChromeDriver(service, options);
            
            return chromeDriver;
        }

       



        [Obsolete("Function này hiện tại không còn được hỗ trợ")]
        public static ChromeDriver CreateGoogleChromeDriver(bool isHidden = false)
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = OpenQA.Selenium.PageLoadStrategy.None;
            InitParameterChromeOption(options, isHidden);
            //options.AddArgument("--test-type");
            //options.AddArgument("--silent"); // disable silent
            ChromeDriver chromeDriver = new ChromeDriver(options);
            return chromeDriver;
        }

        public static ChromeDriver CreateChromeDriver(string profilePath, 
            ChromeOptionConfig chromeOptionConfig )
        {
            if (!Directory.Exists(profilePath))
            {
                throw new Exception("Không tìm thấy thư mục");
            }
            var directory = Path.GetDirectoryName(profilePath);
            var nameProject = Path.GetFileName(profilePath);
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = chromeOptionConfig.PageLoadStrategy;
            if(chromeOptionConfig != null)
            {
                InitParameterChromeOption(options, chromeOptionConfig);
            }
            

            //hide cammand
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = chromeOptionConfig.IsHiddenCommandLine;
            //InitProfile
            var userdatadir = $"user-data-dir={directory}";
            var profiledirectory = $"profile-directory={nameProject}";
            options.AddArgument(userdatadir);
            options.AddArgument(profiledirectory);
            ChromeDriver chromeDriver = new ChromeDriver(service, options);

            return chromeDriver;

        }

        [Obsolete("Phương thức này hiện tại đã ko còn hỗ trợ")]
        public static ChromeDriver CreateGoogleChromeDriver(string profilePath, bool isHidden = false)
        {
            if(!Directory.Exists(profilePath))
            {
                throw new Exception("Không tìm thấy thư mục");
            }
            var directory = Path.GetDirectoryName(profilePath);
            var nameProject = Path.GetFileName(profilePath);
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = OpenQA.Selenium.PageLoadStrategy.None;
            InitParameterChromeOption(options, isHidden);

            //hide cammand
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            //InitProfile
            var userdatadir = $"user-data-dir={directory}";
            var profiledirectory = $"profile-directory={nameProject}";
            options.AddArgument(userdatadir);
            options.AddArgument(profiledirectory);
            ChromeDriver chromeDriver = new ChromeDriver(service, options);

            return chromeDriver;

        }

       
    }
}
