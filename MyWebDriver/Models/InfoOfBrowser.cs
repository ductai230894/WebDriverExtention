using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Models
{
    public class InfoOfBrowser
    {
        public Point LocationOfBrowserOnScreen { get; set; }
        public Size SizeOfBrowser { get; set; }
        public int HeightInnerHtml { get; set; }
    }
}
