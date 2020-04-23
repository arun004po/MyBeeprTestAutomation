using System;
using OpenQA.Selenium;
using Flexigroup.UIAutomation.Api.Humm.MobileApp.Pages;
using Flexigroup.UIAutomation.Core;
using OpenQA.Selenium.Appium;
using AventStack.ExtentReports;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Android;

namespace Flexigroup.UIAutomation.Api.Humm.MobileApp.Workflow
{
    public class SignUpWorkflow : WorkflowBase
    {

        public SignUpWorkflow(IWebDriver driver, ExtentTest test) : base(driver, test)
        {
            signUP = new SignUp(Driver);
            hummClient = new HummClient();
            login = new Login(Driver);
        }
        SignUp signUP;
        HummClient hummClient;
        Login login;
        string otc;
        SignUpWorkflow signUpWorkFlow;
        #region External Workflow

        public void SignUpWithMobileNumber(string mobileNumber)
        {
            signUP.ClickSkipButton();
            signUP.ClickContinueButton();
            Thread.Sleep(1000);
            signUP.ClickingChangeFlowButton();
            signUP.EnterMobileNumber(mobileNumber);
            signUP.SelectSignUp();
            if (TestBase.platform.Equals("iosApp"))
            {
                signUP.SelectSignUp();
            }
        }
        public void EnteringOtc(string mobileNumber)
        {
            signUP.ClickOtcEntry();
            otc = hummClient.GetUserDetails(mobileNumber, "VerificationCode");
            signUP.EnterOtcEntry(mobileNumber, otc);
        }

        public void EnteringandConfirmingPassword(string password)
        {
            signUP.EnterPassword(password);
            signUP.ConfirmPassword(password);
            signUP.ClickNext();

        }
        public void SelectTitle(string title)
        {
            signUP.ClickTitle();
            if (title == "Mr")
            {
                signUP.SelectTitleMr();
            }
            else if (title == "Mrs")
            {
                signUP.SelectTitleMrs();
            }
            else if (title == "Miss")
            {
                signUP.SelectTitleMiss();
            }
            else
            {
                signUP.SelectTitleMs();
            }
        }
        public void DeleteUser(string mobileNumber)
        {
            hummClient.DeleteUser(mobileNumber);
        }
        public void EnteringPersonalInformation(string title, string name, string middleName, string lastName, string dob, string emailId, string address)
        {
            SelectTitle(title);
            if (TestBase.platform == "AndroidApp")
            {
                signUP.ClickConfirm();
            }
            signUP.EnterFirstName(name);
            signUP.EnterMiddleName(middleName);
            signUP.EnterLastName(lastName);
            if (TestBase.platform == "AndroidApp")
            {
                ((AppiumDriver<IWebElement>)Driver).HideKeyboard();
                Driver.Swipe(0, 300, 0, 50, 10);
            }
            signUP.ClickNext();
            signUP.EnterDateOfBirth(dob);
            signUP.EnterEmailAddress(emailId);
        }

        public void EnterAddress(string addressType, string address, string unit, string streetNumber, string streetName, string streetType, string suburb, string state, string postcode)
        {
            if (addressType == "manual")
            {
                signUP.ClickEnterAddressManuallyButton();
                signUP.EnterUnitNumber(unit);
                signUP.EnterStreetNumber(streetNumber);
                signUP.EnterStreetName(streetName);
                signUP.EnterStreetType(streetType);
                signUP.EnterSuburb(suburb);
                signUP.SelectState();
                signUP.EnterPostCode(postcode);
            }
            else
            {
                signUP.EnterAddress(address);
            }
        }
        /// <summary>
        /// This is a common method to enter personal information with password
        /// </summary>-
        /// <param name="addressType"></param>
        public void EnteringPersonalInformationwithPassword(string addressType)
        {
            SignUpWithMobileNumber(DataLoad.GetData("Username"));
            EnteringOtc(DataLoad.GetData("Username"));
            EnteringandConfirmingPassword(DataLoad.GetData("password"));
            EnteringPersonalInformation(DataLoad.GetData("title"), DataLoad.GetData("name"), DataLoad.GetData("middleName"), DataLoad.GetData("lastName"), DataLoad.GetData("dob"), DataLoad.GetData("emailId"), DataLoad.GetData("address"));
            EnterAddress(addressType, DataLoad.GetData("address"), DataLoad.GetData("unit"), DataLoad.GetData("streetNumber"), DataLoad.GetData("streetName"), DataLoad.GetData("streetType"), DataLoad.GetData("suburb"), DataLoad.GetData("state"), DataLoad.GetData("postcode"));
        }

        public void EnteringLicenseDetails(string licenseNumber, string licenseCardNumber, string licenseExpiryDate)
        {
            signUP.ClickDrivingLicenseButton();
            signUP.ClickStateOfIssueLicense();
            signUP.SelectDrivingLicenseStateOfIssueForNSW();
            if (TestBase.platform == "AndroidApp")
            {
                signUP.ClickConfirm();
            }
            signUP.EnterDrivingLicenseNumber(licenseNumber);
            signUP.EnterLicenseCardNumber(licenseCardNumber);
            signUP.EnterDrivingLicenseExpiryDate(licenseExpiryDate);
        }
        public void EnteringMedicareDetails(string fullName, string medicareCardNumber, string medicareColor, string referenceNumber, string medicareExpiryDate)
        {
            signUP.ClickMedicare();
            signUP.EnterMedicareFullName(fullName);
            signUP.EnterMedicareCardNumber(medicareCardNumber);
            signUP.ClickMedicareColor();
            signUP.SelectMedicareColor(medicareColor);
            signUP.EnterMedicareReferenceNumber(referenceNumber);
            signUP.EnterMedicareExpiryDate(medicareExpiryDate);
        }
        public void EnteringPassportDetails(string passportNUmber, string gender, string passportExpiryDate)
        {
            signUP.ClickPassport();
            signUP.EnterAustralianPassportNumber(passportNUmber);
            signUP.ClickGenderPassport();
            if (TestBase.platform == "AndroidApp")
            {
                if (gender == "Male")
                {
                    signUP.SelectMaleGenderPassport();
                }
                else if (gender == "Female")
                {
                    signUP.SelectFemaleGenderPassport();
                }
                else
                {
                    signUP.SelectGenderPassportPreferNotToSay();
                }

                signUP.ClickConfirm();
            }
            signUP.EnterPassportExpiryDate(passportExpiryDate);
        }

        public void DoubleCheckingInformation()
        {
            if (TestBase.platform == "iosApp")
            {
                signUP.DoubleCheckingInformationIos();
                signUP.ClickConfirmButtonIos();
            }
            else
            {               
                signUP.SelectAccessSeeker();                               
                signUP.SelectTermsAndPolicy();
                signUP.SelectStatus();
                signUP.ClickConfirm();
            }

        }
        public void ValidateSignUp()
        {
            if (TestBase.platform.Equals("iosApp"))
            {
                ExtentTrue(login.NavigationHomeIos.Displayed);
                ExtentTrue(login.TransactionsButtonIos.Displayed);
                ExtentTrue(login.ShopButtonIos.Displayed);
                ExtentTrue(login.BarcodeButtonIos.Displayed);
                ExtentTrue(login.SettingsButtonIos.Displayed);
                string actual = login.AmountIos.Text;
                string expected = DataLoad.GetData("Amount");
                Assert.AreEqual(actual, expected);
            }
            else
            {
                ExtentTrue(login.NavigationHome.Displayed);
                ExtentTrue(login.TransactionsButton.Displayed);
                ExtentTrue(login.ShopButton.Displayed);
                ExtentTrue(login.BarcodeButton.Displayed);
                ExtentTrue(login.SettingsButton.Displayed);
                string actual = login.Amount.Text;
                string expected = DataLoad.GetData("Amount");
                Assert.AreEqual(actual, expected);

            }
        }

        public void AddingCardInformation(string cardHolderName, string cardNumber, string cardExpiryDate, string cardCVC)
        {
          
            signUP.EnterCardHolderName(cardHolderName);
            signUP.EnterCardNumber(cardNumber);
            signUP.EnterCardExpiryDate(cardExpiryDate);
            signUP.EnterCardCVC(cardCVC);
            if (TestBase.platform.Equals("AndroidApp"))
            {
                ((AppiumDriver<IWebElement>)Driver).HideKeyboard();
            }
            signUP.ClickAddCardButton();
        }

        public void PickingAPin(string pinNumber)
        {
            signUP.PickAPin(pinNumber);
        }
        #endregion

    }
}
