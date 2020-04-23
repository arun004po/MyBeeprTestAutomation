using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flexigroup.UIAutomation.TestCases.Runner;
using Flexigroup.UIAutomation.Core;
using Flexigroup.UIAutomation.Api.Humm.MobileApp.Workflow;

namespace Flexigroup.UIAutomation.TestCases.Humm
{
    [TestClass]
    public class LoginTest : TestRunner
    {
     
        /// <summary>
        /// This test is to login to humm application
        /// </summary>
        [DataSource(dataSourceSettingName: "HummLogin")]
        [TestCategory("Humm"), TestMethod]
        public void HummLogin()
        {
            Test(() =>
            {
                //hummClient.CreateHummUser("0426177360", "Password1", "Dan", "JOURNEYA", "Humphrey", "1993-05-15", "nicholas.cooper1@test.com"); 
                var login = new LoginWorkflow(Driver, extentTest);
                login.LoginWithMobileNumber(DataLoad.GetData("Username"), DataLoad.GetData("Password"), DataLoad.GetData("pinNumber"));
                login.ValidateLogin();
            });
        }
        /// <summary>
        /// This test is to reset passwod to login using forgot password link
        /// </summary>
        [DataSource(dataSourceSettingName: "HummLogin")]
        [TestCategory("Humm"), TestMethod]
        public void ResetPassword()
        {
            Test(() =>
            {
                var login = new LoginWorkflow(Driver, extentTest);
                login.ResetPassword(DataLoad.GetData("Username"), DataLoad.GetData("Password"), DataLoad.GetData("pinNumber"));
            });
        }
    }
}
