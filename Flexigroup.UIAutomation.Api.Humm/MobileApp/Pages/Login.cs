using OpenQA.Selenium;
using Flexigroup.UIAutomation.Core;
using System.Collections.Generic;


namespace Flexigroup.UIAutomation.Api.Humm.MobileApp.Pages
{
    internal class Login : PageObjectBase
    {
        #region Variables  

        public By SkipButton => By.Id("skipButton");
        public By SkipButtonIos => By.Name("link_register");
        public By ContinueButton => By.Id("continueButton");
        public By ContinueButtonIos => By.Name("login_button");
        public By MobileNumber => By.Id("mobileEntry");
        public By MobileNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Tell us your mobile number']");
        public By LoginButton => By.XPath("//*[@text='LOGIN']");
        public By LoginButtonIos => By.Name("Login");
        public By SigninButtonIos => By.Name("Sign in");
        public By SignUpButton => By.XPath("//*[@text='SIGN UP']");
        public By SignUpButtonIos => By.Name("Sign up");
        public By ChangeFlowButton => By.Id("changeFlowButton");
        public By TypeYourPassword => By.Id("passwordEntry");
        public By ReTypeYourPassword => By.Id("passwordConfirmationEntry");
        public By TypeYourPasswordIos => By.XPath("//XCUIElementTypeSecureTextField[@value='Type your password']");
        public By ReTypeYourPasswordIos => By.XPath("//XCUIElementTypeSecureTextField[@value='Re-type your password']");
        public By ConfirmButton => By.XPath("//android.widget.Button[@text='CONFIRM']");
        public By CancelButton => By.XPath("//android.widget.Button[@text='CANCEL']");
        public By EmailId => By.Id("email");
        public By NextButton => By.Id("nextButton");
        public By NextButtonIos => By.Name("Next");
        public By CardHolderName => By.Id("name");
        public By CardNumber => By.Id("number");
        public By CardExpiryDate => By.Id("expiry");
        public By CardCvc => By.Id("cvc");
        public By AddCardButton => By.XPath("//android.widget.Button[contains(@text=,'ADD CARD']");
        public By PickAPIN => By.Id("pinEntry");
        public IWebElement NavigationHome => Driver.FindElement(By.Id("navigation_home"));
        public IWebElement NavigationHomeIos => Driver.FindElement(By.Name("Home"));
        public By ForgotPasswordButton => By.Id("forgotPasswordButton");
        public By ForgotPasswordButtonIos => By.Name("I forgot my password");
        public IWebElement TransactionsButton => Driver.FindElement(By.Id("navigation_transactions"));
        public IWebElement TransactionsButtonIos => Driver.FindElement(By.Name("Transactions"));
        public IWebElement ShopButton => Driver.FindElement(By.Id("navigation_shop"));
        public IWebElement ShopButtonIos => Driver.FindElement(By.Name("Shop"));
        public IWebElement BarcodeButton => Driver.FindElement(By.Id("navigation_barcode"));
        public IWebElement BarcodeButtonIos => Driver.FindElement(By.Name("Barcode"));
        public IWebElement SettingsButton => Driver.FindElement(By.Id("navigation_settings"));
        public IWebElement SettingsButtonIos => Driver.FindElement(By.Name("Settings"));
        public IWebElement AmountIos => Driver.FindElement(By.XPath("//XCUIElementTypeTable/XCUIElementTypeCell[1]/XCUIElementTypeStaticText[3]"));
        public IWebElement Amount => Driver.FindElement(By.Id("amount"));
        public IWebElement GiveUsFeedBackWindow_NotNowButton => Driver.FindElement(By.XPath("//android.widget.Button[@text='NOT NOW']"));


        #endregion

        public Login(IWebDriver driver) : base(driver)
        {
            SignUp signUp = new SignUp(Driver);
        }

        #region Functions  
        public void ClickSkipButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(SkipButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(SkipButton);
                    break;
            }
        }

        public void ForgotPassword()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(ForgotPasswordButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(ForgotPasswordButton);
                    break;
            }
        }

        public void ClickContinueButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(ContinueButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(ContinueButton);
                    break;
            }
        }

        public void EnterMobileNumber(string mobileNumber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(MobileNumberIos, mobileNumber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(MobileNumber, mobileNumber);
                    break;
            }
        }

        public void ClickNext()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(NextButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(NextButton);
                    break;
            }
        }

        public void EnterPassword(string password)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(TypeYourPasswordIos, password);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(TypeYourPassword, password);
                    break;
            }

        }
        public void ConfirmPassword(string password)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(ReTypeYourPasswordIos, password);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(ReTypeYourPassword, password);
                    break;
            }

        }
             
        public void ClickLogin()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(LoginButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(LoginButton);
                    break;
            }

        }
        public void AgreeTermsAndConditionsResetPassword()
        {
            IList<IWebElement> list = Driver.FindElements(By.Name("unchecked"));
            list[0].ClickElement();
            list[1].ClickElement();
        }

        public void ClickingChangeFlowButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(ChangeFlowButton);
                    break;
            }

        }
        public void SelectSignUp()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(SignUpButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(SignUpButton);
                    break;
            }

        }

        public void ClickConfirm()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(ConfirmButton);
                    break;
            }
        }

        public void PickAPin(string pinNUmber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(PickAPIN, pinNUmber);
                    Driver.WaitForElementToBeLocatedAndEnterText(PickAPIN, pinNUmber);
                    break;
            }

        }
        public void ClickGiveUsFeebackNotNowButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    GiveUsFeedBackWindow_NotNowButton.Click();
                    break;                 
            }

        }
    }
}
#endregion