using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace angular2prototype.web.specs.tests.common
{
	[Binding]
	public class BaseSteps
	{
		/// <summary>
		/// Open browser
		/// </summary>
		[BeforeTestRun]
		public static void Setup()
		{
			Console.WriteLine("Setup");
			Browser.InitWebDriver();
		}

		/// <summary>
		/// Close browser
		/// </summary>
		[AfterTestRun]
		public static void TearDown()
		{
			Console.WriteLine("Teardown");
			Browser.Teardown();
		}

		[BeforeScenario]
		public static void ResetToHome()
		{
			Browser.WebDriver.Navigate().GoToUrl(ConstantsUtils.Url);
		}

		/// <summary>
		/// Take screenshot in case the test has failed
		/// </summary>
		[AfterScenario]
		public static void TakeScreenshot()
		{
			if (ScenarioContext.Current.TestError != null)
			{
				Browser.TakeScreenshot(ScenarioContext.Current.ScenarioInfo.Title);
			}
		}

		/// <summary>
		/// Opens the specified page
		/// </summary>
		/// <param name="url">URL</param>
		[Given(@"I am on the page ""(.*)""")]
		public void IAmOnPage(string url)
		{
			Browser.GoTo(url);
		}

		/// <summary>
		/// Inserts the specified value in the specified HTML Element. 
		/// The mapping between the specified "field" to the HTML element id 
		/// is read from the App.config file.
		/// </summary>
		/// <param name="value">Value</param>
		/// <param name="field">Name of the HTML Element</param>
		[When(@"I enter the value""(.*)"" in ""(.*)""")]
		public void WhenIEnterValueIn(string value, string field)
		{
			By by = GetBy(field);
			if (by != null)
			{
				IWebElement e = Browser.WebDriver.FindElement(by);
				e.Click();
				e.Clear();
				e.SendKeys(value);
			}
		}

		/// <summary>
		/// Clicks the specified button. 
		/// The mapping between the specified "buttonName" to the HTML element id 
		/// is read from the App.config file.
		/// </summary>
		/// <param name="buttonName">Button Name</param>
		[When(@"I click the button ""(.*)""")]
		public void WhenIClickButton(string buttonName)
		{
			By by = GetBy(buttonName);
			if (by != null)
			{
				Browser.WebDriver.FindElement(by).Click();
			}
		}

		/// <summary>
		/// Clicks the link witch contains the specified link text.
		/// </summary>
		/// <param name="linkText">Link text</param>
		[When(@"I click the link ""(.*)""")]
		public void WhenIClickLink(string linkText)
		{
			Browser.WebDriver.FindElement(By.LinkText(linkText)).Click();
		}

		/// <summary>
		/// Asserts whether the specified text is displayed on the page.
		/// </summary>
		/// <param name="text">Erwarteter Text</param>
		[Then(@"the text ""(.*)"" is displayed")]
		public void ThenTheTextIsDisplayed(string text)
		{
			Assert.IsTrue(Browser.WebDriver.PageSource.Contains(text), "The page does not contain the text: " + text);
		}

		/// <summary>
		/// Reads from the App.config file the value of the specified parameter key.
		/// The value is splitted (separater is ':'). The first part contains the 
		/// locator type (e.g. xpath or id) and the second part contains the value of the locator.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private By GetBy(string key)
		{
			string element = ConfigurationManager.AppSettings[key];
			By by = null;
			if (element != null)
			{
				string[] elements = element.Split(':');
				switch (elements[0].ToLower())
				{
					case "xpath":
						by = By.XPath(elements[1]);
						break;
					case "tagname":
						by = By.TagName(elements[1]);
						break;
					case "name":
						by = By.Name(elements[1]);
						break;
					case "cssselector":
						by = By.CssSelector(elements[1]);
						break;
					default:
						by = By.Id(elements[1]);
						break;
				}
			}
			return by;
		}
	}
}
