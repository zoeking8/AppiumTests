using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace AppiumTests.Calculator
{
	[TestClass]
	public class Addition : Calculator
	{
        private static WindowsElement header;

        [TestMethod]
        public void AdditionTwoNumbers()
        {
            calSession.FindElementByName("One").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Seven").Click();
            calSession.FindElementByName("Equals").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestMethod]
        public void AdditionSameNumbers()
        {
            calSession.FindElementByName("Seven").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Seven").Click();
            calSession.FindElementByName("Equals").Click();
            Assert.AreEqual("14", GetCalculatorResultText());
        }

        [TestMethod]
        public void AdditionAllNumberElements()
        {
            calSession.FindElementByName("Zero").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("One").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Two").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Three").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Four").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Five").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Six").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Seven").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Eight").Click();
            calSession.FindElementByName("Plus").Click();
            calSession.FindElementByName("Nine").Click();
            calSession.FindElementByName("Equals").Click();
            Assert.AreEqual("45", GetCalculatorResultText());
        }


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Open Calculator 
            StartUp(context);

            // Identify calculator by the header
            try
            {
                header = calSession.FindElementByAccessibilityId("Header");
            }
            catch
            {
                header = calSession.FindElementByAccessibilityId("ContentPresenter");
            }

            // Check for calculator mode - needs to be in standard mode
            if (!header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase))
            {
                calSession.FindElementByAccessibilityId("TogglePaneButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var splitViewPane = calSession.FindElementByClassName("SplitViewPane");
                splitViewPane.FindElementByName("Standard Calculator").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Assert.IsTrue(header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase));
            }

            // Locate the calculatorResult element
            calculatorResult = calSession.FindElementByAccessibilityId("CalculatorResults");
            Assert.IsNotNull(calculatorResult);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestInitialize]
        public void Clear()
        {
            calSession.FindElementByName("Clear").Click();
            Assert.AreEqual("0", GetCalculatorResultText());
        }
    }

}
