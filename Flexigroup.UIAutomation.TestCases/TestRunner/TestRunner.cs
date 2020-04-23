using Flexigroup.UIAutomation.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;


namespace Flexigroup.UIAutomation.TestCases.Runner
{   
    [TestClass]
    public class TestRunner : TestBase
    {        

        [TestInitialize]
        public void TestInitialize()
        {
            DataLoad.InitialiseTextContext(TestContext);
            if (dataDrivenTest)
                isTestEnabled.Value = Start();            
        }
      
        [TestCleanup]
        public void TestCleanup()
        {
            //Close browser app after each test if selected in runsettings file
            if (closeBrowserAfterTest == true)
                if (platform.ToUpper() != "DESKTOP")
                {
                    
                    if (platform.ToUpper() == "ANDROIDAPP")
                        ((AndroidDriver<IWebElement>)Driver)?.CloseApp();
                    else if (platform.ToUpper() == "IOSAPP")
                        ((IOSDriver<IWebElement>)Driver)?.CloseApp();                    
                    else Driver?.Quit();
                    if (environment.ToUpper() != "BROWSERSTACK")
                    {
                        appiumLocalService.Dispose();
                        KillRunningProcesses();
                    }
                }
                else
                {
                    
                    Driver?.Quit();
                }
        }

        [AssemblyInitialize]
        public new static void AssemblyInit(TestContext testContext)
        {
            // call the base AssemblyInitialize - Waiting on fix by MS so this is not needed
           Flexigroup.UIAutomation.Core.TestBase.AssemblyInit(testContext);
        }

        [AssemblyCleanup]
        public new static void TestSuiteCleanup()
        {
            // call the base AssemblyCleanup - Waiting on fix by MS so this is not needed
            Flexigroup.UIAutomation.Core.TestBase.TestSuiteCleanup();            
        }
       
    }
}
