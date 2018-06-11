using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformSpider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformSpider.Tests
{
    [TestClass()]
    public class WebWorkerTests
    {
        [TestMethod()]
        public void GuidSubTest()
        {
            ParamTool pt = new ParamTool();
            string val = pt.GetVl5x("1234");
             
            Assert.Fail();
        }
    }
}