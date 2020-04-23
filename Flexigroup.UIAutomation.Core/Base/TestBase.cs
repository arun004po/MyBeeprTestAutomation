using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AventStack.ExtentReports;
using log4net;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using System.IO;
using OpenQA.Selenium.Appium.Service.Options;
using System.Threading;
using OpenQA.Selenium.Edge;

namespace Flexigroup.UIAutomation.Core
{
    [TestClass]
    public abstract class TestBase
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(TestBase));
        readonly static string userName = ConfigurationManager.AppSettings["user"];
        readonly static string accessKey = ConfigurationManager.AppSettings["key"];
        public static IWebDriver Driver { get; set; }
        public TestContext TestContext { get; set; }
        public ExtentTest extentTest;
        public static ThreadLocal<bool> isTestEnabled = new ThreadLocal<bool>();
        public static AppiumLocalService appiumLocalService;
        public static string mobileOS = null;
        public static bool dataDrivenTest;
        public static string extentMode = null;
        public static string runningDriverType = null;        
        public static bool closeBrowserAfterTest;
        public static string platform = null;
        public static string environment = null;     
        private static string _deviceUuid = null;
        private static string _iosBsAppId = null;
        private static string _androidBsAppId = null;
        private static string _deviceName = null;        
        private static string _platformVersion = null;
        private static string _appLocation = null;
        private static string _appPackage = null;
        private static string _appActivity = null;
        private static bool _chromeHeadless;
        private static bool _chromeIncognito;
        private static ExtentReports _extentReport;        
        private static Reporter _Reporter;        
        

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext TestContext)
        {
            //setup runtime variables from run settings
            mobileOS = TestContext.Properties["mobileOS"].ToString();
            environment = TestContext.Properties["environment"].ToString();
            platform = TestContext.Properties["platform"].ToString();
            bool.TryParse(TestContext.Properties["dataDrivenTest"].ToString(), out dataDrivenTest);
            _deviceUuid = TestContext.Properties["deviceUuid"].ToString();
            _iosBsAppId = TestContext.Properties["iosBrowsterStackAppId"].ToString();
            _androidBsAppId = TestContext.Properties["androidBrowsterStackAppId"].ToString();
            _deviceName = TestContext.Properties["deviceName"].ToString();            
            _platformVersion = TestContext.Properties["platformVersion"].ToString();
            _appLocation = TestContext.Properties["appLocation"].ToString();
            _appPackage = TestContext.Properties["appPackage"].ToString();
            _appActivity = TestContext.Properties["appActivity"].ToString();
            bool.TryParse(TestContext.Properties["CloseBrowserAfterEachTest"].ToString(), out closeBrowserAfterTest);
            bool.TryParse(TestContext.Properties["ChromeModeHeadless"].ToString(), out _chromeHeadless);
            bool.TryParse(TestContext.Properties["ChromeModeIncognito"].ToString(), out _chromeIncognito);
            

            //Initialise Extent Reports
            _Reporter = new Reporter();
            _extentReport = _Reporter.InitialiseExtentReports();
            _extentReport.AddSystemInfo("OS", TestContext.Properties["applicationOs"].ToString());
            _extentReport.AddSystemInfo("Host Name", TestContext.Properties["hostName"].ToString());
            _extentReport.AddSystemInfo("Environment", TestContext.Properties["environment"].ToString());
            _extentReport.AddSystemInfo("User Name", TestContext.Properties["executionUsername"].ToString());
            WorkflowBase.InitializeExtentValue(TestContext.Properties["ExtentMode"].ToString());

            //Create log4net directory
            var log4LogFileDir = ".\\Logs\\CaptureLogs\\";
            var log4CreateDir = Directory.CreateDirectory(log4LogFileDir);

            //If local mobile automation then start appium server
            if (platform.ToUpper() != "DESKTOP")
            {
                if (environment.ToUpper() != "BROWSERSTACK")
                    StartAppium();
            }
        }

        [AssemblyCleanup]
        public static void TestSuiteCleanup()
        {
            var path = _Reporter.FlushExtentReport();     


            //Close browser at end of test run
            if (closeBrowserAfterTest == false)
                if (platform.ToUpper() != "DESKTOP")
                {
                    logger.Info("in the assembly cleanup != desktop");
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
                    Driver?.Close();
                    Driver?.Quit();                    
                }           
        }


        #region Logging
        public static void StartTestCase(string testcasename)
        {
            logger.Info("****************************************************************************************");
            logger.Info("$$$$$$$$$$$$$$$$$$$$$               " + testcasename + "       $$$$$$$$$$$$$$$$$$$$$$$$$");
            logger.Info("****************************************************************************************");
        }

        public static void EndTestCase(string testcasename)
        {
            logger.Info("****************************************************************************************");
            logger.Info("XXXXXXXXXXXXXXXXXXXXXXX" + testcasename + "-E---N---D-" + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            logger.Info("****************************************************************************************");
        }
        public void LogInfo(string text)
        {
            var msg = string.Empty;
            if (TestContext.DataRow != null)
                msg = "[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] ";
            msg += text;
            logger.Info(msg);
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        public void LogError(string text)
        {
            var msg = string.Empty;
            if (TestContext.DataRow != null)
                msg = "[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] ";
            msg += text;
            logger.Error(msg);
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }

        public void LogError(string text, Exception e)
        {
            var msg = string.Empty;
            if (TestContext.DataRow != null)
                msg = "[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] ";
            msg += text;
            logger.Error(text, e);
            msg += "Exception:" + e;
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
        }
        #endregion


        protected void Test(Action action)
        {
            try
            {
                if (isTestEnabled.Value)
                {
                    action();
                }
                else
                {
                    const string msg = "Test is not " +
                        "enabled in input file. This line of input file is being ignored.";
                    LogInfo(msg);
                    Assert.Inconclusive(msg);
                }
            }
            catch (Exception e) when (!(e is AssertInconclusiveException))
            {
                try
                {
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    extentTest.Log(Status.Fail, "Unhandled Exception " + e, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                }
                catch (Exception)
                {
                    extentTest.Log(Status.Fail, "Unhandled Exception " + e);
                }             
                LogError("Unhandled Exception", e);      
                throw;
            }
        }

        //start appium local service
        public static void StartAppium()
        {
            var appiumLogBasedir = ".\\Logs\\Appium\\";
            var appiumLogCreateDir = Directory.CreateDirectory(appiumLogBasedir);
            var fileName = appiumLogBasedir + "\\appiumLog.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            var logFile = new FileInfo(fileName);
            var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
  
            appiumLocalService = new AppiumServiceBuilder().WithIPAddress("127.0.0.1").WithLogFile(logFile).Build();
            appiumLocalService.Start();
        }

        //entry point for runner to start data driven test 
        public bool Start()
        {
            var isEnabled = DataLoad.GetData("IsEnabled");
            if (isEnabled != null && isEnabled.ToUpper() == "FALSE")
                return false;
                                    extentTest = _extentReport.CreateTest("[Iteration: " + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow) + "] - " + DataLoad.GetData("Description"));
            SeleniumExtensions.InitialiseExtentTest(extentTest);
            log4net.Config.XmlConfigurator.Configure();

            var driverType = GetDriverType();
            if (!(closeBrowserAfterTest))
            {
                if (runningDriverType == null || runningDriverType != driverType)
                {
                    Driver?.Close();
                    StartDriver(driverType);
                }
                    
            }                
            else StartDriver(driverType);
            return true;
        }

        //Start non data driven test
        public void Start(string testName, string driverType)
        {
            extentTest = _extentReport.CreateTest(testName);
            SeleniumExtensions.InitialiseExtentTest(extentTest);
            log4net.Config.XmlConfigurator.Configure();
            StartDriver(driverType);
        }

        //Get the driver type.  Driven from runsettings.  If Desktop set browser type tasken from data sheet
        public string GetDriverType()
        {
            switch (platform.ToUpper())
            {
                case "DESKTOP":
                    var driver = DataLoad.GetData("Browser");
                    if (string.IsNullOrEmpty(driver))
                        driver = "CHROME";
                    return driver;
                case "ANDROIDWEB":
                    return "ANDROIDWEB";
                case "ANDROIDAPP":
                    return "ANDROIDAPP";
                case "IOSWEB":
                    return "IOSWEB";
                case "IOSAPP":
                    return "IOSAPP";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //Star the driver that is read from the get driver type method
        private void StartDriver(string driverType)
        {
            Driver = GetDriver(driverType);
            runningDriverType = driverType;
        }

        //Setup options/capabilities for the selected driver type
        private IWebDriver GetDriver(string browser)
        {
            try
            {
                switch (browser.ToUpper())
                {
                    case "CHROME":
                        ChromeOptions options = new ChromeOptions();
                        Console.Out.WriteLine("Setting Chrome Options");
                        options.AddArguments("--start-maximized");
                        if (_chromeHeadless)
                            options.AddArguments("--headless");
                        if (_chromeIncognito)
                            options.AddArguments("--incognito");
                        Driver = new ChromeDriver(options);
                        Driver.Manage().Timeouts().PageLoad = Constants.PageLoad;
                        Driver.Manage().Timeouts().ImplicitWait = Constants.ImplicitWait;
                        Console.Out.WriteLine("Created Driver");
                        break;
                    case "IE":
                        var ieOptions = new InternetExplorerOptions()

                        {
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            IgnoreZoomLevel = true
                        };
                        Driver = new InternetExplorerDriver(ieOptions);
                        Driver.Manage().Timeouts().PageLoad = Constants.PageLoad;
                        Driver.Manage().Timeouts().ImplicitWait = Constants.ImplicitWait;
                        break;
                    case "FIREFOX":
                        var ffOptions = new FirefoxOptions();
                        Driver = new FirefoxDriver(ffOptions);
                        Driver.Manage().Timeouts().PageLoad = Constants.PageLoad;
                        Driver.Manage().Timeouts().ImplicitWait = Constants.ImplicitWait;
                        break;
                    case "EDGE":
                        var edgeOptions = new EdgeOptions();
                        Driver = new EdgeDriver(edgeOptions);
                        Driver.Manage().Timeouts().PageLoad = Constants.PageLoad;
                        Driver.Manage().Timeouts().ImplicitWait = Constants.ImplicitWait;
                        break;
                    case "ANDROIDWEB":
                        var desiredCaps = new AppiumOptions();
                        desiredCaps.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");
                        desiredCaps.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);
                        desiredCaps.AddAdditionalCapability(MobileCapabilityType.PlatformName, mobileOS);
                        desiredCaps.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, _platformVersion);                       
                        desiredCaps.AddAdditionalCapability(AndroidMobileCapabilityType.NativeWebScreenshot, true);
                        desiredCaps.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
                        desiredCaps.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
                        desiredCaps.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 700000);
                        if (environment.ToUpper() != "BROWSERSTACK")
                        {
                            desiredCaps.AddAdditionalCapability(MobileCapabilityType.Udid, _deviceUuid);
                            Driver = new AndroidDriver<IWebElement>(appiumLocalService, desiredCaps);
                        }                            
                        else
                        {
                            desiredCaps.AddAdditionalCapability("browserstack.user", userName);
                            desiredCaps.AddAdditionalCapability("browserstack.key", accessKey);
                            desiredCaps.AddAdditionalCapability("real_mobile", "true");
                            Driver = new AndroidDriver<IWebElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), desiredCaps);
                        }
                        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(2);
                        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        break;
                    case "ANDROIDAPP":
                        var androidAppCaps = new AppiumOptions();
                        androidAppCaps.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);
                        androidAppCaps.AddAdditionalCapability(MobileCapabilityType.PlatformName, mobileOS);
                        androidAppCaps.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, _platformVersion);
                        androidAppCaps.AddAdditionalCapability(AndroidMobileCapabilityType.NativeWebScreenshot, true);
                        androidAppCaps.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
                        //androidAppCaps.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
                        androidAppCaps.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
                        androidAppCaps.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 700000);
                        androidAppCaps.AddAdditionalCapability("autoAcceptAlerts", "true");
                        if (environment.ToUpper() != "BROWSERSTACK")
                        {
                            androidAppCaps.AddAdditionalCapability(MobileCapabilityType.Udid, _deviceUuid);
                            androidAppCaps.AddAdditionalCapability(MobileCapabilityType.App, _appLocation);
                            androidAppCaps.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, _appPackage);
                            androidAppCaps.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, _appActivity);
                            androidAppCaps.AddAdditionalCapability("NO_UPDATE_NOTIFIER", "true");
                            Driver = new AndroidDriver<IWebElement>(appiumLocalService, androidAppCaps);
                        }                           
                        else
                        {
                            androidAppCaps.AddAdditionalCapability("browserstack.user", userName);
                            androidAppCaps.AddAdditionalCapability("browserstack.key", accessKey);
                            androidAppCaps.AddAdditionalCapability("app", _androidBsAppId);
                            androidAppCaps.AddAdditionalCapability("real_mobile", "true");
                            androidAppCaps.AddAdditionalCapability("browserstack.debug", "true");
                            Driver = new AndroidDriver<IWebElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), androidAppCaps);
                        }
                        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        
                        break;
                    case "IOSAPP":
                        var iosAppCaps = new AppiumOptions();
                        iosAppCaps.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);
                        iosAppCaps.AddAdditionalCapability(MobileCapabilityType.PlatformName, mobileOS);
                        iosAppCaps.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, _platformVersion);
                        iosAppCaps.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
                        iosAppCaps.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
                        if (environment.ToUpper() != "BROWSERSTACK")
                        {
                            iosAppCaps.AddAdditionalCapability(MobileCapabilityType.Udid, _deviceUuid);
                            iosAppCaps.AddAdditionalCapability(IOSMobileCapabilityType.AppName, _appPackage);
                            Driver = new IOSDriver<IWebElement>(appiumLocalService, iosAppCaps);}
                        else
                        {
                            iosAppCaps.AddAdditionalCapability("browserstack.user", userName);
                            iosAppCaps.AddAdditionalCapability("browserstack.key", accessKey);
                            iosAppCaps.AddAdditionalCapability("app", "bs://92355f0bf590f50ebb393ff7933d6bb52874a692");
                            iosAppCaps.AddAdditionalCapability("app", _iosBsAppId);
                            iosAppCaps.AddAdditionalCapability("real_mobile", "true");
                            iosAppCaps.AddAdditionalCapability("browserstack.debug", "true");
                            iosAppCaps.AddAdditionalCapability("wdaLaunchTimeout", "240000");
                            iosAppCaps.AddAdditionalCapability("wdaConnectionTimeout", "240000");
                            Driver = new IOSDriver<IWebElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), iosAppCaps);
                        }
                        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        iosAppCaps.AddAdditionalCapability("autoAcceptAlerts", "true");
                        break;
                    case "IOSWEB":
                        var iosWebCaps = new AppiumOptions();
                        iosWebCaps.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);
                        iosWebCaps.AddAdditionalCapability(MobileCapabilityType.PlatformName, mobileOS);
                        iosWebCaps.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, _platformVersion);
                        iosWebCaps.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
                        iosWebCaps.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
                        iosWebCaps.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
                        if (environment.ToUpper() != "BROWSERSTACK")
                        {
                            iosWebCaps.AddAdditionalCapability(MobileCapabilityType.Udid, _deviceUuid);
                            iosWebCaps.AddAdditionalCapability(IOSMobileCapabilityType.AppName, _appPackage);
                            Driver = new IOSDriver<IWebElement>(appiumLocalService, iosWebCaps);
                        }
                        else
                        {
                            iosWebCaps.AddAdditionalCapability("browserstack.user", userName);
                            iosWebCaps.AddAdditionalCapability("browserstack.key", accessKey);
                            iosWebCaps.AddAdditionalCapability("real_mobile", "true");
                            iosWebCaps.AddAdditionalCapability("browserstack.debug", "true");
                            iosWebCaps.AddAdditionalCapability("wdaLaunchTimeout", "240000");
                            iosWebCaps.AddAdditionalCapability("wdaConnectionTimeout", "240000");
                            Driver = new IOSDriver<IWebElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), iosWebCaps);
                        }
                        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(2);
                        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        break;
                    default:
                        extentTest.Log(Status.Error, "Platform Not Set");
                        throw new ArgumentOutOfRangeException();
                }
                return Driver;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Couldnt start driver" + e);
                extentTest.Log(Status.Error, "Unable to load browser driver " + e);
                throw;
            }
        }

        //Gets the application url.  COmbination of a base URL in app settings and a resource path from the data sheet
        public string GetApplicationUrl(string appBaseUrl, string key = "ApplicationUrl", params object[] args)
        {
            var baseUrl = ConfigurationManager.AppSettings[appBaseUrl];

            if (string.IsNullOrEmpty(baseUrl))
            {
                extentTest.Log(Status.Error, "AppSettings['BaseApplicationUr'] is not set");
                throw new ArgumentException("AppSettings['BaseApplicationUr'] is not set");
            }
            string subUrl;
            if (appBaseUrl.ToUpper() == "IFOL")
                subUrl = string.Empty;
            else subUrl = DataLoad.GetData(key);
            var fullUrl = baseUrl + subUrl;
            if (args != null && args.Length > 0)
                fullUrl = string.Format(fullUrl, args);
            logger.Info("This is the url " + fullUrl);
            return fullUrl;
        }
        
        public static void KillRunningProcesses()
        {
            KillAllProcessesOfName("node");
        }

        private static void KillAllProcessesOfName(string name)
        {
            bool finished;

            do
            {
                finished = !KillProcess(name);
            } while (!finished);
        }

        private static bool KillProcess(string processName)
        {
            var processes = System.Diagnostics.Process.GetProcesses();
            var process = processes.FirstOrDefault(x => x.ProcessName == processName);


            if (process != null)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch (Exception)
                {
                    // can get an exception if the process has already died etc. In this situation
                    // return true to go around the loop again and attempt again
                }
                return true;
            }
            return false;
        }
    }
}
