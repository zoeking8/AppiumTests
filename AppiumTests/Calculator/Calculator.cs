using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumTests.Calculator
{
    public class Calculator
    {
        public static WindowsElement calculatorResult;

        //Appium Driver URI 
        private const string appiumDriverURI = "http://127.0.0.1:4723/wd/hub";

        //Application Key 
        private const string calApp = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";

        protected static WindowsDriver<WindowsElement> calSession;

        public static void StartUp(TestContext context)
        {
            if (calSession == null)
            {
                AppiumOptions options = new AppiumOptions();
                options.AddAdditionalCapability("app", calApp);
                options.AddAdditionalCapability("deviceName", "WindowsPC");
                calSession = new WindowsDriver<WindowsElement>(new Uri(appiumDriverURI), options);
                Assert.IsNotNull(calSession);

                calSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
                calSession.FindElementByName("Clear").Click();
                Assert.AreEqual("0", GetCalculatorResultText());
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (calSession != null)
            {
                calSession.Quit();
                calSession = null;
            }
        }
        public string GetCalculatorResultText()
        {
            return calculatorResult.Text.Replace("Display is", string.Empty).Trim();
        }

    }
}