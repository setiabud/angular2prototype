using angular2prototype.web.specs.tests.common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace angular2prototype.web.specs.tests.features
{
	[Binding]
	public class NavigateHomeSteps : BaseSteps
	{
		[Given(@"the HomePage")]
		public void GivenTheHomePage()
		{
			
		}

		[Then(@"I see '(.*)' as the title")]
		public void ThenISeeAsTheTitle(string p0)
		{
			Assert.AreEqual(p0, Browser.WebDriver.Title);
		}
	}
}
