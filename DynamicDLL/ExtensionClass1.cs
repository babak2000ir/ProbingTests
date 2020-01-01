using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensibilityLibrary;

namespace DynamicDLL
{
    class ExtensionClass1 : IMyExtension
    {
        public object Anything { get; set; }

        public string GetDateTime()
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetDayOfMonth(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetYear(DateTime.Now);
        }

        public string GetName()
        {
            return "Persian Date";
        }
    }
}
