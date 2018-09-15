using AutoIt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebDriver.Utils
{
    public class AutoITWebDriverResult
    {
        private static readonly SemaphoreSlim _flag = new SemaphoreSlim(1, 1);
        readonly IntPtr _winHandle;

        public EventHandler OpenNewTagByAutoItNotify { get; set; }

        public AutoITWebDriverResult(IntPtr program)
        {
            _winHandle = program;
        }
        public bool IsError { get; set; } = false;
        public bool IsFlagSet { get; set; } = false;
        public string MessageError { get; set; }

        public AutoITWebDriverResult SetFlag()
        {
            if (IsFlagSet == false)
            {
                IsFlagSet = true;
                _flag.Wait();
            }
            return this;
        }
        public AutoITWebDriverResult ReleaseFlag()
        {
            if(IsFlagSet == true)
            {
                IsFlagSet = false;
                _flag.Release();
            }
            return this;
        }

        public AutoITWebDriverResult FocusBrowser()
        {
            if (IsError) return this;
            AutoItX.WinActivate(_winHandle);
            Task.Delay(1000).Wait();

            return this;
        }

        public AutoITWebDriverResult OpenNewTabWebDriver()
        {
            if (IsError == true) return this;
            if(IsFlagSet == false)
                AutoItX.ControlSend(_winHandle, IntPtr.Zero, "^{t}");
            else AutoItX.Send("^{t}");
            Task.Delay(1000).Wait();
            return this;
        }
        public AutoITWebDriverResult SendTextToAddressBar(string text)
        {
            if (IsError) return this;
            if(IsFlagSet == false)
            {
                AutoItX.ControlSend(_winHandle, IntPtr.Zero, "^{l}");
                SendKeyForControl(text);
                AutoItX.ControlSend(_winHandle, IntPtr.Zero, "{ENTER}");
                Task.Delay(1000).Wait();
            }
            else
            {
                AutoItX.Send("^{l}");
                SendKeys(text);
                AutoItX.Send("{ENTER}");
                Task.Delay(1000).Wait();
            }
            //AutoItX.ControlSend(_winHandle, IntPtr.Zero, "^{l}");
            
            //AutoItX.ControlSend(_winHandle, IntPtr.Zero, "{ENTER}");

            return this;
        }
        private void SendKeys(string title)
        {
            //AutoItX.Send(title);
            //AutoItX.ControlSend(_winHandle, IntPtr.Zero, title);
            foreach (var c in title)
            {
                //AutoItX.ControlSend(_winHandle, IntPtr.Zero, c.ToString());
                AutoItX.Send(c.ToString());
                Task.Delay(200).Wait();
            }
        }
        private void SendKeyForControl(string title)
        {
            foreach (var c in title)
            {
                AutoItX.ControlSend(_winHandle, IntPtr.Zero, c.ToString());
                //AutoItX.Send(c.ToString());
                Task.Delay(200).Wait();
            }
        }




    }
}
