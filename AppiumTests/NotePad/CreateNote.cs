using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppiumTests.NotePad
{
	[TestClass]
	public class CreateNote : NotePad
	{
        [TestMethod]
        public void EditorEnterText()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            editBox.SendKeys("abcdeABCDE 12345");
            Assert.AreEqual(@"abcdeABCDE 12345", editBox.Text);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            StartUp(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}
