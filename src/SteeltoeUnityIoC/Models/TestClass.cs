using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteeltoeUnityIoC
{
    public class TestClass : ITestClass
    {
        public string DoSomethingCrazy()
        {
            return "doing something crazy";
        }
    }
}