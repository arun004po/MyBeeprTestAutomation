    using OpenQA.Selenium;
using log4net;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.IO;
using System;

namespace Flexigroup.UIAutomation.Core
{
    public abstract class WorkflowBase
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(WorkflowBase));
        private static string _extentMode;

        public IWebDriver Driver { get; set; }
        public readonly ExtentTest extentTest;

        public WorkflowBase(IWebDriver driver, ExtentTest test)
        {
            Driver = driver;
            extentTest = test;
        }

        public static void InitializeExtentValue(string extentMode)
        {
            _extentMode = extentMode;
        }
        public void ExtentPass(string logMessage)
        {
            switch (_extentMode)
            { 
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);                                       
                    try
                    {
                        extentTest.Log(Status.Pass, logMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                    }
                    catch (IOException e)
                    {
                        extentTest.Log(Status.Pass, logMessage);
                        Logger.Info(e);
                    }
                    break;
                case "Fast":
                    extentTest.Log(Status.Pass, logMessage);
                    break;
            }
        }

        public void ExtentPass(string logMessage, string expectedValue)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    try
                    {
                        extentTest.Log(Status.Pass, logMessage + "Expected Value " + expectedValue, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                    }
                    catch (IOException e)
                    {
                        extentTest.Log(Status.Pass, logMessage + "Expected Value " + expectedValue);
                        Logger.Info(e);
                    }
                    break;
                case "Fast":
                    extentTest.Log(Status.Pass, logMessage + "Expected Value " + expectedValue);
                    break;
            }
        }
        public void ExtentFail(string logMessage)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    try
                    {
                        extentTest.Log(Status.Fail, logMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                    }
                    catch (IOException e)
                    {
                        extentTest.Log(Status.Fail, logMessage);
                        Logger.Info(e);
                    }
                    break;
                case "Fast":
                    extentTest.Log(Status.Fail, logMessage);
                    break;
            }
        }

        public void ExtentFail(string logMessage, string expectedValue)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    try
                    {
                        extentTest.Log(Status.Fail, logMessage + " Expected Value " + expectedValue, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                        
                    }
                    catch (IOException e)
                    {
                        extentTest.Log(Status.Fail, logMessage + " Expected Value " + expectedValue);
                        Logger.Info(e);
                    }
                    break;
                case "Fast":
                    extentTest.Log(Status.Fail, logMessage);
                    break;
            }
        }

        public void ExtentFailWithAssertion(string logMessage)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    try
                    {
                        extentTest.Log(Status.Fail, logMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                    }
                    catch (IOException e)
                    {
                        extentTest.Log(Status.Fail, logMessage);
                        Logger.Info(e);
                    }
                    break;
                case "Fast":
                    extentTest.Log(Status.Fail, logMessage);
                    break;
            }
            Assert.Fail(logMessage);
        }
        public void ExtentFailWithAssertion(string logMessage, string expectedValue)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    try
                    {
                        extentTest.Log(Status.Fail, logMessage + " Expected Value " + expectedValue, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                    }
                    catch (IOException e)
                    {
                        extentTest.Log(Status.Fail, logMessage + " Expected Value " + expectedValue);
                        Logger.Info(e);
                    }
                    break;
                case "Fast":
                    extentTest.Log(Status.Fail, logMessage);
                    break;
            }
            Assert.Fail(logMessage);
        }

        

        /**
        * Check an elements text is the same as expected value
        *
        * @param details  text to be added to the test report
        * @param element  element that you want to get the text for
        * @param expected expected value of the element
        */
        public void CheckElementText(IWebElement element, string details, string expected)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    if (element.Text.Equals(expected))
                    {                        
                        try
                        {
                            extentTest.Log(Status.Pass, details + "Expected: " + expected + "    Actual: " + element.Text, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Pass, details + "Expected: " + expected + "    Actual: " + element.Text);
                            Logger.Error(e);
                        }
                    }
                    else
                    {
                        try
                        {
                            extentTest.Log(Status.Fail, details + "Expected: " + expected + "    Actual: " + element.Text, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Fail, details + "Expected: " + expected + "    Actual: " + element.Text);
                            Logger.Error(e);
                        }
                    }
                    break;
                case "Fast":
                    if (element.Text.Equals(expected))
                    {
                        extentTest.Log(Status.Pass, details + "Expected: " + expected + "    Actual: " + element.Text);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, details + "Expected: " + expected + "    Actual: " + element.Text);
                    }
                    break;
                default:
                    if (element.Text.Equals(expected))
                    {
                        extentTest.Log(Status.Pass, details + "Expected: " + expected + "    Actual: " + element.Text);
                    }
                    else
                    {
                         extentTest.Log(Status.Fail, details + "Expected: " + expected + "    Actual: " + element.Text);
                    }
                    break;
            }
        }
        public void ExtentTrue(bool value, string logDescription = null)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    if (value)
                    {
                        try
                        {
                            extentTest.Log(Status.Pass, logDescription + " Asserting true: " + value.ToString(), MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                            Assert.IsTrue(value);
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Pass, logDescription + " Asserting true: " + value.ToString());
                            Logger.Error(e);
                            Assert.IsTrue(value);
                        }
                    }
                    else
                    {
                        try
                        {
                            extentTest.Log(Status.Fail, logDescription + " Asserting true: " + value.ToString(), MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                            Assert.IsTrue(value);
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Fail, logDescription + " Asserting true: " + value.ToString());
                            Logger.Error(e);
                            Assert.IsTrue(value);
                        }
                    }
                    break;
                case "Fast":
                    if (value)
                    {
                        extentTest.Log(Status.Pass, logDescription + " Asserting true: " + value.ToString());
                        Assert.IsTrue(value);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, logDescription + " Asserting true: " + value.ToString());
                        Assert.IsTrue(value);
                    }
                    break;
                default:
                    if (value)
                    {
                        extentTest.Log(Status.Pass, logDescription + " Asserting true: " + value.ToString());
                        Assert.IsTrue(value);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, logDescription + " Asserting true: " + value.ToString());
                        Assert.IsTrue(value);
                    }
                    break;
            }

        }


        /**
        * Check a string is the same as expected value with soft assertion
        *
        * @param details  text to be added to the test report
        * @param actual   String to compare
        * @param expected expected value of the string
        */
        public void CheckText(string expected, string actual, string logDescription = null)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    if (actual.Equals(expected))
                    {
                        try
                        {
                            extentTest.Log(Status.Pass, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());

                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Pass, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual);
                            Logger.Error(e);
                        }
                    }
                    else
                    {                        
                        try
                        {
                            extentTest.Log(Status.Fail, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Fail, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual);
                            Logger.Error(e);
                        }
                    }
                    break;
                case "Fast":
                    if (actual.Equals(expected))
                    {
                        extentTest.Log(Status.Pass, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual);
                    }
                    break;
                default:
                    if (actual.Equals(expected))
                    {    
                        extentTest.Log(Status.Pass, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual);
                    }
                    else
                    {                        
                        extentTest.Log(Status.Fail, logDescription + " Checking Text. Expected: " + expected + "    Actual: " + actual);
                    }
                    break;
            }            

        }

        /**
        * Check a string is the same as expected value with hard assertion
        *
        * @param details  text to be added to the test report
        * @param actual   String to compare
        * @param expected expected value of the string
        */
        public void VerifyElementText(IWebElement element, string expected)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    if (element.Text.Equals(expected))
                    {
                        try
                        {
                            extentTest.Log(Status.Pass, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                            Assert.AreEqual(expected, element.Text);
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Pass, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text);
                            Assert.AreEqual(expected, element.Text);
                            Logger.Error(e);
                        }
                    }
                    else
                    {
                        try
                        {
                            extentTest.Log(Status.Fail, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                            Assert.AreEqual(expected, element.Text);
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Fail, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text);
                            Assert.Equals(expected, element.Text);
                            Logger.Error(e);
                        }
                    }
                    break;
                case "Fast":
                    if (element.Text.Equals(expected))
                    {
                        extentTest.Log(Status.Pass, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text);
                        Assert.AreEqual(expected, element.Text);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text);
                        Assert.AreEqual(expected, element.Text);
                    }
                    break;
                default:
                    if (element.Text.Equals(expected))
                    {
                        extentTest.Log(Status.Pass, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text);
                        Assert.AreEqual(expected, element.Text);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, "Verifying Element Text. Expected: " + expected + "    Actual: " + element.Text);
                        Assert.AreEqual(expected, element.Text);
                    }
                    break;
            }
        }

        /**
        * Check a string is the same as expected value with hard assertion
        *
        * @param details  text to be added to the test report
        * @param actual   String to compare
        * @param expected expected value of the string
        */
        public void VerifyText(string expected, string actual, string logDescription = null)
        {
            switch (_extentMode)
            {
                case "Normal":
                    var screenShot = TakeScreenShot.ScreenShot(Driver);
                    if (actual.Equals(expected))
                    {                        
                        try
                        {
                            extentTest.Log(Status.Pass, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                            Assert.AreEqual(expected, actual);
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Pass, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual);
                            Assert.AreEqual(expected, actual);
                            Logger.Error(e);
                        }
                    }
                    else
                    {                        
                        try
                        {
                            extentTest.Log(Status.Fail, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot).Build());
                            Assert.AreEqual(expected, actual);
                        }
                        catch (IOException e)
                        {
                            extentTest.Log(Status.Fail, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual);
                            Assert.AreEqual(expected, actual);
                            Logger.Error(e);
                        }
                    }
                    break;
                case "Fast":
                    if (actual.Equals(expected))
                    {
                        extentTest.Log(Status.Pass, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual);
                        Assert.AreEqual(expected, actual);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual);
                        Assert.AreEqual(expected, actual);
                    }
                    break;
                default:
                    if (actual.Equals(expected))
                    {
                        extentTest.Log(Status.Pass, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual);
                        Assert.AreEqual(expected, actual);
                    }
                    else
                    {
                        extentTest.Log(Status.Fail, logDescription + " Verifying Text From Element. Expected: " + expected + "    Actual: " + actual);
                        Assert.AreEqual(expected, actual);
                    }
                    break;
            }
        }

        /**
        * Check if an element is on the screen
        *
        * @param element     Element to check if displayed
        * @param elementName String description of the element for logging
        */

    }
}
