using OpenQA.Selenium;
using log4net;

namespace Flexigroup.UIAutomation.Core
{
    public abstract class PageObjectBase
    {
        protected PageObjectBase(IWebDriver driver)
        {
            Driver = driver;
        }

        public static readonly ILog Logger = LogManager.GetLogger(typeof(PageObjectBase));
     
        public IWebDriver Driver { get; set; }
       
  
    }
}