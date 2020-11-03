using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumTests.NotePad
{
	public class NotePad
	{
		//Appium Driver URI 
		private const string appiumDriverURI = "http://127.0.0.1:4723/wd/hub";

		//Application route 
		private const string notepadAppRoute = @"C:\Windows\System32\notepad.exe";

		protected static WindowsDriver<WindowsElement> noteSession;
		protected static WindowsElement editBox;


		public static void StartUp(TestContext context)
		{
			if (noteSession == null)
			{
				AppiumOptions options = new AppiumOptions();
				options.AddAdditionalCapability("app", notepadAppRoute);
				options.AddAdditionalCapability("deviceName", "WindowsPC");
				noteSession = new WindowsDriver<WindowsElement>(new Uri(appiumDriverURI), options);

				Assert.IsNotNull(noteSession);
				Assert.AreEqual("Untitled - Notepad", noteSession.Title);

				noteSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

				editBox = noteSession.FindElementByClassName("Edit");
				Assert.IsNotNull(editBox);
			}
		}

		[TestInitialize]
		public void TestInitialize()
		{
			// Clearing the edit box
			editBox.SendKeys(Keys.Control + "a" + Keys.Control);
			editBox.SendKeys(Keys.Delete);
			Assert.AreEqual(string.Empty, editBox.Text);
		}

		public static void TearDown()
		{
			// Close the application and delete the session
			if (noteSession != null)
			{
				noteSession.Close();

				try
				{
					noteSession.FindElementByName("Don't Save").Click();
				}
				catch { }

				noteSession.Quit();
				noteSession = null;
			}
		}
	}
}
