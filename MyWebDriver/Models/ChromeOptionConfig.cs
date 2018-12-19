using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Models
{
    [Obsolete("Chuyen qua sai DriverOptionsConfig")]
    public class ChromeOptionConfig
    {
        public bool IsDisableImage { get; set; }
        public bool IsDisableJavascript { get; set; }
        public bool IsDisableFont { get; set; }
        public bool IsDisableFlash { get; set; }
        public bool IsHeadless { get; set; }
        public bool IsDisableNotify { get; set; }
        public bool IsHiddenCommandLine { get; set; }
        [Obsolete("Thuộc tính này chưa hỗ trợ")]
        public bool IsDisableCss { get; set; }
        public PageLoadStrategy PageLoadStrategy { get; set; }
        public TimeSpan PageLoadTimeOut { get; set; }

        public string IpProxy { get; set; }
        public uint PortProxy { get; set; }
        public string UserAgent { get; set; }
        public bool IsSocks5Proxy { get;  set; }
        public bool NullProxy { get; set; } = false;
    }

    public class DriverOptionsConfig
    {
        public bool IsDisableImage { get; set; }
        public bool IsDisableJavascript { get; set; }
        public bool IsDisableFont { get; set; }
        public bool IsDisableFlash { get; set; }
        public bool IsHeadless { get; set; }
        public bool IsDisableNotify { get; set; }
        public bool IsHiddenCommandLine { get; set; }
        [Obsolete("Thuộc tính này chưa hỗ trợ tren google driver")]
        public bool IsDisableCss { get; set; }
        public PageLoadStrategy PageLoadStrategy { get; set; }
        public TimeSpan PageLoadTimeOut { get; set; }

        public string IpProxy { get; set; }
        public uint PortProxy { get; set; }

    }
}
