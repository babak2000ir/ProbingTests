using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDLL
{
	class TestClass2
	{
        private bool _Initialized = false;
        public static string GetClassInfo()
        {
            return "Class2";
        }
        public TestClass2()
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
