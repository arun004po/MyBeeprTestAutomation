using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Flexigroup.UIAutomation.Core
{
    public class Reporter
    {
        public ExtentReports extent;

        public static ThreadLocal<string> extentFolder = new ThreadLocal<string>();
        public static string date;

        public ExtentReports InitialiseExtentReports()
        {
            extentFolder.Value = ".\\Logs\\" + DateTime.Now.ToString("yyyyMMdd");     
            date = DateTime.Now.ToString("yyyyMMddHHmmss");
           // extentFolder.Value = extentReportBaseFolder + "\\" + date + "\\";
            var di = Directory.CreateDirectory(extentFolder.Value);

            // initialize the HtmlReporter
           // var htmlReporter = new ExtentHtmlReporter(extentFolder.Value + "\\" + date + ".html");
            var htmlReporter = new ExtentV3HtmlReporter(extentFolder.Value + "\\" + date + ".html");
            htmlReporter.LoadConfig("extent-config.xml");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            return extent;
        }
             
        public string FlushExtentReport()
        {
            extent.Flush();
            return extentFolder.Value + "\\" + date + ".html";
        }
    }
}
