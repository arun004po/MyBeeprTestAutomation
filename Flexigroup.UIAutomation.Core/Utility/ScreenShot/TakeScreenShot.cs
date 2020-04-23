using System;
using System.IO;
using OpenQA.Selenium;
using System.Configuration;
using OpenQA.Selenium.Interactions;

namespace Flexigroup.UIAutomation.Core
{
    public static class TakeScreenShot
    {
        
        public static string ScreenShot(IWebDriver driver, IWebElement element = null)
        {
            var di = Directory.CreateDirectory(Reporter.extentFolder.Value + "\\Screenshots");
            string fileName = Path.Combine(Reporter.extentFolder.Value + "\\Screenshots", DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
            if (element != null)
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(element);
                actions.Perform();
            }            
            Screenshot screenShot = ((ITakesScreenshot)driver).GetScreenshot();
            screenShot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            return fileName.Remove(0, 16);
        }
       
    }
}
