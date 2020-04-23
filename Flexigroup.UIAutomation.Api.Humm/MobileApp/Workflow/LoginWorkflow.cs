using OpenQA.Selenium;
using Flexigroup.UIAutomation.Api.Humm.MobileApp.Pages;
using Flexigroup.UIAutomation.Core;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Flexigroup.UIAutomation.Api.Humm.MobileApp.Workflow
{
    public class LoginWorkflow : WorkflowBase
    {

        public LoginWorkflow(IWebDriver driver, ExtentTest test) : base(driver, test)
        {
            login = new Login(Driver);
            hummClient = new HummClient();
            signUpWorkflow = new SignUpWorkflow(Driver, extentTest);
            signUp = new SignUp(Driver);
        }

        Login login;
        SignUpWorkflow signUpWorkflow;
        HummClient hummClient;
        SignUp signUp;

        #region External Workflow
  
        public void LoginWithMobileNumber(string mobileNumber, string password, string pin)
        {
            login.ClickSkipButton();
            login.ClickContinueButton();
            login.EnterMobileNumber(mobileNumber);
            login.ClickLogin();
            login.EnterPassword(password);
            login.ClickLogin();
            signUpWorkflow.EnteringOtc(mobileNumber);
            signUp.PickAPin(pin);
            Thread.Sleep(2000) ;
            login.ClickGiveUsFeebackNotNowButton();

        }
        public void ValidateLogin()
        {
            ExtentTrue(login.NavigationHome.Displayed);
            ExtentTrue(login.TransactionsButton.Displayed);
            ExtentTrue(login.ShopButton.Displayed);
            ExtentTrue(login.BarcodeButton.Displayed);
            ExtentTrue(login.SettingsButton.Displayed);
            ExtentTrue(login.Amount.Text.Equals(DataLoad.GetData("Amount")));
        }
        public void ResetPassword(string mobileNumber, string password, string pin)
        {
            login.ClickSkipButton();
            login.ClickContinueButton();
            login.EnterMobileNumber(mobileNumber);
            login.ClickLogin();
            login.ForgotPassword();
            signUpWorkflow.EnteringOtc(mobileNumber);
            login.EnterPassword(password);
            login.ConfirmPassword(password);
            login.ClickNext();
            if (TestBase.platform == "iosApp")
            {
                login.AgreeTermsAndConditionsResetPassword();
                signUp.ClickConfirmButtonIos();
            }
            signUp.PickAPin(pin);
        }
        #endregion


    }

}