using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Models
{
    public enum LoginResult
    {
        Success,
        Captcha,
        Fail,
        NotFound,
        Error,
        ProxyError
    }
}
