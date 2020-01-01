﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDLL2
{
    class ExtensionClass2 : IMyExtension
    {
        public object Anything { get; set; }

        public string GetDateTime()
        {
            JapaneseCalendar jc = new JapaneseCalendar();
            return jc.GetDayOfMonth(DateTime.Now) + "//" + jc.GetMonth(DateTime.Now) + "//" + jc.GetYear(DateTime.Now) + " of " + jc.GetEra(DateTime.Now);
        }

        public string GetName()
        {
            return "Japanese Date";
        }
    }
}