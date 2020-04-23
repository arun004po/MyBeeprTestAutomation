using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using AventStack.ExtentReports;
using System.Threading;
using System.Reflection;
using log4net;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Flexigroup.UIAutomation.Core
{
    public static class SeleniumExtensions
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(WorkflowBase));

        public static void InitialiseExtentTest(ExtentTest test)
        {
            _test = test;
        }

        private static ExtentTest _test;

        public static bool VerifyPageUrl(this IWebDriver driver, string pageUrl)
        {
            Logger.Error("Page url is " + driver.Url);
            if (driver.Url.Contains(pageUrl))
            {
                _test.Log(Status.Pass, "Page URL is " + pageUrl);
                return driver.Url.Contains(pageUrl);
            }
            else
            {
                _test.Log(Status.Fail, "Page URL " + pageUrl + " Not Verified.  Actual url is " + driver.Url);
                return false;
            }

        }

        #region Dropdown
        public static void SelectByValue(this SelectElement input, string value)
        {
            input.SelectByValue(value);
            _test.Log(Status.Pass, "Selected " + value);
        }

        public static void SelectByText(this SelectElement input, string text)
        {
            input.SelectByText(text);
            _test.Log(Status.Pass, "Selected " + text);
        }

        #endregion

        #region Click

        public static IWebDriver ClickndWait(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var element = driver.FindElement(by);

            if (element != null)
            {
                _test.Log(Status.Pass, ("Clicking And Waiting For Element " + element.Text + " " + element.GetAttribute("name")));
                element.Click();
                System.Threading.Thread.Sleep((int)timeout.TotalMilliseconds);
            }

            return driver;
        }

        public static IWebDriver ClickndWait(this IWebDriver driver, IWebElement element, TimeSpan timeout)
        {
            if (element != null)
            {
                _test.Log(Status.Pass, ("Clicking And Waiting For Element " + element.Text + " " + element.GetAttribute("name")));
                element.Click();
                System.Threading.Thread.Sleep((int)timeout.TotalMilliseconds);
            }

            return driver;
        }

        public static void Click(this IWebElement element, bool ignoreStaleElementException = false)
        {
            try
            {
                _test.Log(Status.Pass, ("Clicking Element " + element.Text + " " + element.GetAttribute("name")));
                element.Click();
            }
            catch (StaleElementReferenceException ex)
            {
                if (!ignoreStaleElementException)
                    throw ex;
            }
        }

       
        public static IWebElement ClickWhenAvailable(this IWebDriver driver, By by)
        {
            return ClickWhenAvailable(driver, by, Constants.DefaultTimeout);
        }

   
        public static IWebElement ClickWhenAvailable(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var element = driver.FindElement(by);

            WaitUntilClickable(driver,
                                by,
                                timeout,
                                d => { _test.Log(Status.Pass, ("Clicking Element " + element.Text + " " + element.GetAttribute("name"))); element.Click(); },
                                e => { _test.Log(Status.Fail, ($"Element '{element}' not clickable.")); throw new InvalidOperationException($"Unable to click element."); });



            return element;
        }

        public static void ClickElement(this IWebElement element)
        {
            try
            {
                _test.Log(Status.Pass, ("Clicking Element " + element.Text + " " + element.GetAttribute("name")));
                element.Click();
            }
            catch (Exception)
            {
                _test.Log(Status.Fail, ($"Element '{element}' not clickable."));
                throw new Exception($"Element '{element}' not clickable.");
            }
        }

        public static void ClickWithRetry(this IWebDriver driver, IWebElement element)
        {
            var count = 0;
            var finished = false;
            WaitForElement(driver, element);
            do
            {
                try
                {
                    if (count == 11)
                    {
                        _test.Log(Status.Fail, ($"Waited 5 seconds for element '{element}' to click."));
                        throw new TimeoutException($"Waited 5 seconds for element '{element}'.");
                    }
                    _test.Log(Status.Pass, ("Clicking Element " + element.Text + " " + element.GetAttribute("name")));
                    element.Click();
                    finished = true;
                }
                catch (TargetInvocationException)
                {
                    Thread.Sleep(500);
                    count += 1;
                }
                catch (InvalidOperationException)
                {
                    Thread.Sleep(500);
                    count += 1;
                }

            } while (!finished);
        }

        #endregion Click

        #region Double Click

        public static void DoubleClick(this IWebDriver driver, IWebElement element, bool ignoreStaleElementException = false)
        {
            try
            {
                Actions actions = new Actions(driver);
                actions.DoubleClick(element).Perform();
            }
            catch (StaleElementReferenceException ex)
            {
                if (!ignoreStaleElementException)
                    throw ex;
            }
        }

        public static void DoubleClick(this IWebDriver driver, By by, bool ignoreStaleElementException = false)
        {
            try
            {
                var element = driver.FindElement(by);
                driver.DoubleClick(element, ignoreStaleElementException);
            }
            catch (StaleElementReferenceException ex)
            {
                if (!ignoreStaleElementException)
                    throw ex;
            }
        }

        #endregion

        #region Script Execution

        [DebuggerNonUserCode()]
        public static object ExecuteScript(this IWebDriver driver, string script, params object[] args)
        {
            var scriptExecutor = (driver as IJavaScriptExecutor);

            if (scriptExecutor == null)
                throw new InvalidOperationException(
                    $"The driver type '{driver.GetType().FullName}' does not support Javascript execution.");

            return scriptExecutor.ExecuteScript(script, args);
        }

        [DebuggerNonUserCode()]
        public static JObject GetJsonObject(this IWebDriver driver, string @object)
        {
            @object = SanitizeReturnStatement(@object);

            var results = ExecuteScript(driver, $"return JSON.stringify({@object});").ToString();

            return JObject.Parse(results);
        }

        [DebuggerNonUserCode()]
        public static JArray GetJsonArray(this IWebDriver driver, string @object)
        {
            @object = SanitizeReturnStatement(@object);

            var results = ExecuteScript(driver, $"return JSON.stringify({@object});").ToString();

            return JArray.Parse(results);
        }

        [DebuggerNonUserCode()]
        

        private static string SanitizeReturnStatement(string script)
        {
            if (script.EndsWith(";"))
            {
                script = script.TrimEnd(script[script.Length - 1]);
            }

            if (script.StartsWith("return "))
            {
                script = script.TrimStart("return ".ToCharArray());
            }

            return script;
        }

        #endregion Script Execution

        #region Browser Options

        [DebuggerNonUserCode()]
        public static void ResetZoom(this IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.TagName("body"));
            element.SendKeys(Keys.Control + "0");
        }

        #endregion Browser Options

        #region Screenshot

        [DebuggerNonUserCode()]
        public static Screenshot TakeScreenshot(this IWebDriver driver)
        {
            var screenshotDriver = (driver as ITakesScreenshot);

            if (screenshotDriver == null)
                throw new InvalidOperationException(
                    $"The driver type '{driver.GetType().FullName}' does not support taking screenshots.");

            return screenshotDriver.GetScreenshot();
        }

        [DebuggerNonUserCode()]
        public static string LogScreenshot(this IWebDriver driver)
        {
            string fileName = Path.Combine(ConfigurationManager.AppSettings["ScreenshotDirectory"], DateTime.Now.ToString("yyyyddMHHmmss") + ".png");
            Screenshot screenShot = ((ITakesScreenshot)driver).GetScreenshot();
            screenShot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            return fileName;
        }

        [DebuggerNonUserCode()]
        public static Bitmap TakeScreenshot(this IWebDriver driver, By by)
        {
            var screenshot = TakeScreenshot(driver);
            var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));

            // Measure the location of a specific element
            IWebElement element = driver.FindElement(by);
            var crop = new Rectangle(element.Location, element.Size);

            return bmpScreen.Clone(crop, bmpScreen.PixelFormat);
        }

        #endregion Screenshot

        #region Elements

        public static T GetAttribute<T>(this IWebElement element, string attributeName)
        {
            string value = element.GetAttribute(attributeName) ?? string.Empty;

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
        }

        public static string GetAuthority(this IWebDriver driver)
        {
            string url = driver.Url;                // get the current URL (full)
            Uri currentUri = new Uri(url);          // create a Uri instance of it
            string baseUrl = currentUri.Authority;  // just get the "base" bit of the URL

            return baseUrl;
        }

        public static string GetBodyText(this IWebDriver driver)
        {
            return driver.FindElement(By.TagName("body")).Text;
        }

        public static string GetElementText(this IWebElement element)
        {
            return element.Text;
        }


        public static bool HasElement(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElements(by).Count > 0;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool IsElementVisible(this IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException e)
            {
                Logger.Error("Is element visible exceptio " + e);
                return false;
            }
        }

        public static bool IsVisible(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool IsVisible(this IWebElement element, By by)
        {
            try
            {
                return element.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool IsVisible(this IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void SetVisible(this IWebDriver driver, By by, bool visible)
        {
            IWebElement element = driver.FindElement(by);
            if (visible)
                driver.ExecuteScript($"document.getElementById('{element.GetAttribute("Id")}').setAttribute('style', 'display: inline;')");
            else
                driver.ExecuteScript($"document.getElementById('{element.GetAttribute("Id")}').setAttribute('style', 'display: none;')");
        }

      
        public static void EnterText(this IWebDriver driver, By locator, string value)
        {
            driver.WaitUntilClickable(locator);
            var element = driver.FindElement(locator);
            element.Clear();
            element.SendKeys(value);
            _test.Log(Status.Pass, "Entered " + value);
        }
        public static void EnterText(this IWebElement element, string value)
        {

            element.Clear();
            element.SendKeys(value);
            _test.Log(Status.Pass, "Entered " + value);
        }
        public static void SendKeys(this IWebElement element, string value, bool clear)
        {
            if (clear)
            {
                element.Clear();
            }

            element.SendKeys(value);
            _test.Log(Status.Pass, "Entered " + value);
        }

      
        public static bool AlertIsPresent(this IWebDriver driver)
        {
            return AlertIsPresent(driver, new TimeSpan(0, 0, 2));
        }

    
        public static bool AlertIsPresent(this IWebDriver driver, TimeSpan timeout)
        {
            var returnvalue = false;

            WebDriverWait wait = new WebDriverWait(driver, timeout);

            try
            {
                wait.Until(ExpectedConditions.AlertIsPresent());

                returnvalue = true;
            }
            catch (NoSuchElementException)
            {
                returnvalue = false;
            }
            catch (WebDriverTimeoutException)
            {
                returnvalue = false;
            }

            return returnvalue;

        }

        public static IWebDriver LastWindow(this IWebDriver driver)
        {
            return driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public static string GetElementText(this IWebDriver driver, By by)
        {
            // Get the element text but handle an exception an retry a certain number of times. This can be because on the Notes page the
            // notes in the list become stale as the new note is submitted

            var tryCount = 0;
            while (true)
            {
                try
                {
                    var items = driver.FindElements(by);
                    var item = items[0];
                    var text = item.Text;
                    return text;
                }
                catch (Exception)
                {
                    if (tryCount == 10)
                        throw;
                    Thread.Sleep(500);
                    tryCount += 1;
                }
            }
        }

        #endregion Elements

        #region Waits

        public static bool WaitFor(this IWebDriver driver, Predicate<IWebDriver> predicate)
        {
            return WaitFor(driver, predicate, Constants.DefaultTimeout);
        }

        public static bool WaitFor(this IWebDriver driver, Predicate<IWebDriver> predicate, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);

            var result = wait.Until(d => predicate(d));

            return result;
        }

        public static bool WaitForPageToLoad(this IWebDriver driver)
        {
            return WaitForPageToLoad(driver, Constants.DefaultTimeout.Seconds);
        }

        public static bool WaitForTransaction(this IWebDriver driver)
        {
            return WaitForTransaction(driver, Constants.DefaultTimeout.Seconds);
        }

        //public static bool WaitForPageToLoad(this IWebDriver driver, TimeSpan timeout)
        //{
        //    object readyState = WaitForScript(driver, "if (document.readyState) return document.readyState;", timeout);

        //    if (readyState != null)
        //        return readyState.ToString().ToLower() == "complete";

        //    return false;
        //}

        public static bool WaitForPageToLoad(this IWebDriver driver, int maxWaitTimeInSeconds)
        {
            string state = string.Empty;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));

                //Checks every 500 ms whether predicate returns true if returns exit otherwise keep trying till it returns ture
                wait.Until(d =>
                {

                    try
                    {
                        state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    catch (NoSuchWindowException)
                    {
                        //when popup is closed, switch to last windows
                        driver.SwitchTo().Window(driver.WindowHandles.Last(driver));
                    }
                    //In IE7 there are chances we may get state as loaded instead of complete
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase));

                });
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (WebDriverException)
            {
                if (driver.WindowHandles.Count == 1)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }
                state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase)))
                    throw;
            }
            return true;
        }

        public static bool WaitForTransaction(this IWebDriver driver, int maxWaitTimeInSeconds)
        {
            bool state = false;
            try
            {
                //Poll every half second to see if UCI is idle
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500));
                wait.Until(d =>
                {
                    try
                    {
                        //Check to see if UCI is idle
                        state = (bool)driver.ExecuteScript("return window.UCWorkBlockTracker.isAppIdle()", "");
                    }
                    catch (TimeoutException)
                    {

                    }
                    catch (NullReferenceException)
                    {

                    }

                    return state;
                });
            }
            catch (Exception)
            {

            }

            return state;
        }
        public static string Last(this System.Collections.ObjectModel.ReadOnlyCollection<string> handles, IWebDriver driver)
        {
            return handles[handles.Count - 1];
        }
        public static object WaitForScript(this IWebDriver driver, string script)
        {
            return WaitForScript(driver, script, Constants.DefaultTimeout);
        }

        public static object WaitForScript(this IWebDriver driver, string script, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);

            wait.Until((d) =>
            {
                try
                {
                    object returnValue = ExecuteScript(driver, script);

                    return returnValue;
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                catch (WebDriverException)
                {
                    return null;
                }
            });

            return null;
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by)
        {
            return WaitUntilAvailable(driver, by, Constants.DefaultTimeout, null, null);
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout)
        {
            return WaitUntilAvailable(driver, by, timeout, null, null);
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, string exceptionMessage)
        {
            return WaitUntilAvailable(driver, by, Constants.DefaultTimeout, null, d =>
            {
                throw new InvalidOperationException(exceptionMessage);
            });
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout, string exceptionMessage)
        {
            return WaitUntilAvailable(driver, by, timeout, null, d =>
            {
                throw new InvalidOperationException(exceptionMessage);
            });
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback)
        {
            return WaitUntilAvailable(driver, by, timeout, successCallback, null);
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback, Action<IWebDriver> failureCallback)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            bool? success;
            IWebElement returnElement = null;

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                returnElement = wait.Until(d => d.FindElement(by));

                success = true;
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            catch (WebDriverTimeoutException)
            {
                success = false;
            }

            if (success.HasValue && success.Value && successCallback != null)
                successCallback(driver);
            else if (success.HasValue && !success.Value && failureCallback != null)
                failureCallback(driver);

            return returnElement;
        }

     
        public static bool WaitUntilVisible(this IWebDriver driver, By by)
        {
            return WaitUntilVisible(driver, by, Constants.DefaultTimeout, null, null);
        }

  
        public static bool WaitUntilVisible(this IWebDriver driver, By by, TimeSpan timeout)
        {
            return WaitUntilVisible(driver, by, timeout, null, null);
        }

        public static bool WaitUntilVisible(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback)
        {
            return WaitUntilVisible(driver, by, timeout, successCallback, null);
        }

    
        public static bool WaitUntilVisible(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback, Action<IWebDriver> failureCallback)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            bool? success;

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));

                success = true;
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            catch (WebDriverTimeoutException)
            {
                success = false;
            }

            if (success.HasValue && success.Value && successCallback != null)
                successCallback(driver);
            else if (success.HasValue && !success.Value && failureCallback != null)
                failureCallback(driver);

            return success.Value;
        }

      
        public static bool WaitUntilClickable(this IWebDriver driver, By by)
        {
            return WaitUntilClickable(driver, by, Constants.DefaultTimeout, null, null);
        }

     
        public static bool WaitUntilClickable(this IWebDriver driver, By by, TimeSpan timeout)
        {
            return WaitUntilClickable(driver, by, timeout, null, null);
        }

      
        public static bool WaitUntilClickable(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback)
        {
            return WaitUntilClickable(driver, by, timeout, successCallback, null);
        }

     
        public static bool WaitUntilClickable(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback, Action<IWebDriver> failureCallback)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            bool? success;

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(InvalidElementStateException));

            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(by));

                success = true;
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            catch (WebDriverTimeoutException)
            {
                success = false;
            }

            if (success.HasValue && success.Value && successCallback != null)
                successCallback(driver);
            else if (success.HasValue && !success.Value && failureCallback != null)
                failureCallback(driver);

            return success.Value;
        }

        public static void WaitForElements(this IWebDriver driver, By by)
        {
            WaitWithRetry(by, (b) =>
            {
                var var = driver.FindElements(by);
                return var != null && var.Count > 0;
            });
        }

        public static void WaitForElement(this IWebDriver driver, By by)
        {
            WaitWithRetry(by, (b) =>
            {
                var var = driver.FindElement(by);
                return var != null;
            });
        }

        public static void WaitForRowCount(this IWebDriver driver, By by, int count)
        {
            WaitWithRetry(by, (b) =>
            {
                var var = driver.FindElements(by);
                return var.Count == count;
            });
        }

        public static void WaitForDropDownCount(this SelectElement selectElement, int count)
        {
            WaitWithRetry(selectElement, (b) => (b.Options.Count == count));
        }

        public static void WaitForElement(this IWebDriver driver, IWebElement element)
        {
            WaitWithRetry(element, (e) =>
            {
                var wait = new WebDriverWait(driver, Constants.DefaultTimeout);
                wait.Until(d => e.Displayed);
                return true;
            });
        }

        public static void WaitForElementAttributeChange(this IWebDriver driver, IWebElement element, string attribute, string newValue)
        {
            WaitWithRetry(element, (e) =>
            {
                var wait = new WebDriverWait(driver, Constants.DefaultTimeout);
                wait.Until(d => e.GetAttribute(attribute).Contains(newValue));
                return true;
            });
        }
        public static void WaitNoElements(this IWebDriver driver, By by)
        {
            Wait(by, (b) =>
            {
                var var = driver.FindElements(by);
                return var == null || var.Count == 0;
            });
        }

        public static void WaitForElementEnabled(this IWebDriver driver, IWebElement element)
        {
            WaitWithRetry(element, (e) =>
            {
                var wait = new WebDriverWait(driver, Constants.DefaultTimeout);
                wait.Until(d => e.Enabled);
                return true;
            });
        }

        private static void WaitWithRetry<T>(T element, Func<T, bool> search)
        {
            var count = 0;
            var finished = false;
            do
            {
                try
                {
                    if (search(element))
                        finished = true;
                    else
                        Thread.Sleep(1000);
                    count += 1;
                    if (count == 10)
                        throw new TimeoutException($"Waited 10 seconds for element '{element}'.");
                }
                catch (NoSuchElementException)

                {
                    Thread.Sleep(1000);
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(1000);
                }
                catch (InvalidElementStateException)
                {
                    Thread.Sleep(1000);
                }
                catch (TargetInvocationException)
                {
                    Thread.Sleep(1000);
                }
            } while (!finished);
        }

        private static void Wait<T>(T element, Func<T, bool> search)
        {
            var count = 0;
            var finished = false;
            do
            {
                if (search(element))
                    finished = true;
                else
                    Thread.Sleep(1000);
                count += 1;
                if (count == 10)
                    throw new TimeoutException($"Waited 10 seconds for element '{element}'.");
            } while (!finished);
        }


        public static bool WaitDropdownPopulated(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, Constants.DefaultTimeout);
            return wait.Until(drv =>
            {
                SelectElement element = new SelectElement(drv.FindElement(by));
                if (element.Options.Count >= 2)
                {
                    return true;
                }
                return false;
            }
            );
        }

        public static bool WaitDropdownPopulated(this IWebDriver driver, SelectElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, Constants.DefaultTimeout);
            return wait.Until(drv =>
            {
                if (element.Options.Count >= 2)
                {
                    return true;
                }
                return false;
            }
            );
        }
        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            try
            {
                var element = driver.FindElement(by);
                if (element == null)
                    return false;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void WaitUntilPageIsNot(this IWebDriver driver, string[] pageList)
        {
            bool finished = false;
            string currentPage = string.Empty;
            var count = 0;
            do
            {
                try
                {
                    var url = new Uri(driver.Url).Segments;
                    currentPage = url[1];
                }
                catch (WebDriverTimeoutException)
                {
                    // do nothing
                }
                count += 1;
                if (count == 120)
                    throw new TimeoutException($"Waited 120 seconds in WaitUntilPageIsNot {string.Join(",", pageList)}");
                if (string.IsNullOrEmpty(currentPage))
                    Thread.Sleep(1000);
                else if (pageList.Contains(currentPage))
                    Thread.Sleep(1000);
                else
                    finished = true;
            } while (!finished);
        }

        public static void WaitUntilPageIs(this IWebDriver driver, string page)
        {
            bool finished = false;
            string currentPage = string.Empty;
            var count = 0;
            do
            {
                try
                {
                    var url = new Uri(driver.Url).Segments;
                    currentPage = url[1];
                }
                catch (WebDriverTimeoutException)
                {
                    // do nothing
                }
                count += 1;
                if (count == 120)
                    throw new TimeoutException($"Waited 120 seconds in WaitUntilIs {page}");
                if (string.IsNullOrEmpty(currentPage))
                    Thread.Sleep(1000);
                else if (page.Equals(currentPage))
                    Thread.Sleep(1000);
                else
                    finished = true;
            } while (!finished);
        }

        public static void WaitUntilSourceDoesntContain(this IWebDriver driver, string match)
        {
            bool finished = false;
            var count = 0;
            do
            {
                var source = driver.PageSource;
                count += 1;
                if (count == 120)
                    throw new TimeoutException($"Waited 120 seconds in WaitUntilSourceDoesntContain {match}");
                if (source.Contains(match))
                    Thread.Sleep(1000);
                else
                    finished = true;
            } while (!finished);
        }
        public static void WaitForElementToBeLocatedAndClick(this IWebDriver driver, By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, Constants.DefaultTimeout);
            IWebElement element = wait.Until(Driver => driver.FindElement(locator));
            element.Click();
        }
        public static void WaitForElementToBeLocated(this IWebDriver driver, By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, Constants.DefaultTimeout);
            IWebElement element = wait.Until(Driver => driver.FindElement(locator));
        }

        public static void WaitForElementToBeLocatedAndEnterText(this IWebDriver driver, By locator, string text)
        {
            WebDriverWait wait = new WebDriverWait(driver,Constants.DefaultTimeout);
            IWebElement element = wait.Until(Driver => driver.FindElement(locator));
            element.EnterText(text);
        }

        #endregion Waits

        #region Args / Tracing

        public static string GetSessionId(this IWebDriver driver)
        {
            // return the value of the sid in the url
            var url = driver.Url;
            var t1 = "sid=";
            var t2 = "&";
            var txtIndexStart = url.IndexOf(t1, StringComparison.Ordinal);
            if (txtIndexStart == -1)
                return null;
            var txtIndexEnd = url.IndexOf(t2, txtIndexStart, StringComparison.Ordinal);
            string txt;
            if (txtIndexEnd == -1)
                txt = url.Substring(txtIndexStart + t1.Length);
            else
            {
                txt = url.Substring(txtIndexStart + t1.Length, txtIndexEnd - txtIndexStart - t1.Length);
            }
            return txt;
        }
        public static void Swipe(this IWebDriver driver ,int startX, int startY, int endX, int endY, int duration)
        {
             ITouchAction touchAction = new TouchAction((IPerformsTouchActions)driver)                
            .Press(startX, startY)
            .MoveTo(endX, endY)
            .Release();
            touchAction.Perform();
        }
        public static void ScrollDownAndroid( this IWebDriver driver)
        {
            IWebElement element = driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector()).scrollIntoView(resourceId(\"com.shophumm:id/statusCheck\"));"));
            element.Click();
            // Using Touch Action Classes
            //ITouchAction touchAction = new TouchAction((IPerformsTouchActions)driver);

            //// get the proper location of where to start to swipe from
            //Size size = driver.Manage().Window.Size;
            //int startx = (int)(size.Width / 2);
            //int starty = (int)(size.Height * 0.8);
            //int endy = (int)(size.Height * 0.2);
            //// perform the swipe
            //driver.Swipe(startx, starty, startx, endy, 3000);
        }
        /// <summary>
        /// 
        /// This library is only for ios
        /// </summary>
        /// <param name="driver"></param>
        public static void ScrollDown(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var scrollObject = new Dictionary<String, String>();
            scrollObject.Add("direction", "down");
            js.ExecuteScript("mobile: scroll", scrollObject);
        }
        public static void ScrollUp(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var scrollObject = new Dictionary<String, String>();
            scrollObject.Add("direction", "up");
            js.ExecuteScript("mobile: scroll", scrollObject);
        }

        public static void Tap(this IWebDriver driver, int x, int y)
    {
            ITouchAction touchAction = new TouchAction((IPerformsTouchActions)driver);
            touchAction.Tap(x, y).Perform(); 
    }
    public static string ToTraceString(this FindElementEventArgs e)
        {
            try
            {
                if (e.Element != null)
                {
                    return string.Format("{4} - [{0},{1}] - <{2}>{3}</{2}>", e.Element.Location.X, e.Element.Location.Y, e.Element.TagName, e.Element.Text, e.FindMethod);
                }
                else
                {
                    return e.FindMethod.ToString();
                }
            }
            catch (Exception)
            {
                return e.FindMethod.ToString();
            }
        }

        #endregion Args / Tracing
    }
}