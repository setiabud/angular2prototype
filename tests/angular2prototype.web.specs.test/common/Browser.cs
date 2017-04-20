using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace angular2prototype.web.specs.tests.common
{
	public class ConstantsUtils
	{
		public static string Url = "http://localhost:58184/home";
		public static string ScreenShotLocation = "c:\\temp\\";
	}

	public class Browser
	{
		private const string screenShotSuffix = ".png";
		private static IWebDriver webDriver;
		private static TimeSpan timeSpan = TimeSpan.FromSeconds(15);

		public static void GoTo(string url)
		{
			webDriver.Url = url;
		}

		public static IWebDriver InitWebDriver()
		{
			webDriver = new ChromeDriver();
			webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
			return webDriver;
		}

		public static IWebDriver WebDriver
		{
			get { return webDriver; }
		}

		public static void Teardown()
		{
			webDriver.Quit();
		}

		public static void TakeScreenshot(string name)
		{
			string filename = ConstantsUtils.ScreenShotLocation + name + "_" + DateTime.Now.Ticks + screenShotSuffix;
			Console.WriteLine("Take Screenshot - store in file: " + filename);
			Screenshot screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
			screenshot.SaveAsFile(filename, ScreenshotImageFormat.Png);
		}
	}
}
