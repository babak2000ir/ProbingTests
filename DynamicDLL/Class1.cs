using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicDLL
{
    public class TestClass1
    {
        private bool _Initialized = false;
        public static string GetClassInfo()
        {
            MessageBox.Show("Class1");
            return "Class1";
        }
        public TestClass1()
        {
            _Initialized = true;
        }

        public bool Initialized
        {
            get
            {
                return _Initialized;
            }
            set
            {
                _Initialized = value;
            }
        }
    }
}
