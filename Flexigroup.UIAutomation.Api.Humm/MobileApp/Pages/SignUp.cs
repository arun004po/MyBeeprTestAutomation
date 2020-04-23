using OpenQA.Selenium;
using Flexigroup.UIAutomation.Core;
using OpenQA.Selenium.Appium.Android;
using System.Threading;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;

namespace Flexigroup.UIAutomation.Api.Humm.MobileApp.Pages
{
    internal class SignUp : PageObjectBase
    {
        
        #region Variables

        public string otc = null;
        public By SkipButton => By.Id("skipButton");
        public By SkipButtonIos => By.Name("link_register");
        public By SearchResultContentForAddress => By.Id("searchResultsContent");
        public By ContinueButton => By.Id("continueButton");
        public By ContinueButtonIos => By.Name("login_button");
        public By MobileNumber => By.Id("mobileEntry");
        public By MobileNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Tell us your mobile number']");
        public By LoginButton => By.XPath("//*[@text='LOGIN']");
        public By SigninButtonIos => By.Name("Sign in");
        public By SignUpButton => By.XPath("//*[@text='SIGN UP']");
        public By SignUpButtonIos => By.Name("Sign up");
        public By ChangeFlowButton => By.Id("changeFlowButton");
        public By TypeYourPassword => By.Id("passwordEntry");
        public By TypeYourPasswordIos => By.XPath("//XCUIElementTypeSecureTextField[@value='Type your password']");
        public By ForgotPassword => By.Id("forgotPasswordButton");
        public By ReTypeYourPassword => By.Id("passwordConfirmationEntry");
        public By ReTypeYourPasswordIos => By.XPath("//XCUIElementTypeSecureTextField[@value='Re-type your password']");
        public By Title => By.Id("personTitle");
        public By TitleMr => By.XPath("//android.widget.CheckedTextView[@text='Mr']");
        public By TitleMrs => By.XPath("//android.widget.CheckedTextView[@text='Mrs']");
        public By TitleMs => By.XPath("//android.widget.CheckedTextView[@text='Ms']");
        public By TitleMiss => By.XPath("//android.widget.CheckedTextView[@text='Miss']");
        public By FirstName => By.Id("firstName");
        public By FirstNameIos => By.XPath("//XCUIElementTypeTextField[@value='First name']");
        public By MiddleName => By.Id("middleName");
        public By MiddleNameIos => By.XPath("//XCUIElementTypeTextField[@value='Middle name']");
        public By LastName => By.Id("lastName");
        public By LastNameIOs => By.XPath("//XCUIElementTypeTextField[@value='Last name']");
        public By ConfirmButton => By.XPath("//android.widget.Button[@text='CONFIRM']");
        public By DoneButtonIos => By.Name("Done");
        public By ConfirmButtonIos => By.Name("Confirm my plan");
        public By CancelButton => By.XPath("//android.widget.Button[@text='CANCEL']");
        public By CloseButtonIos => By.Name("close");
        public By EmailId => By.Id("email");
        public By EmailIdIos => By.XPath("//XCUIElementTypeTextField[@value='Email address']");
        public By DateOfBirth => By.Id("dob");
        public By DateOfBirthIos => By.XPath("//XCUIElementTypeTextField[@value='DD / MM / YYYY']");
        public By NextButton => By.Id("nextButton");
        public By NextButtonIos => By.Name("Next");
        public By AddressText => By.Id("search");
        public By AddressTextIos => By.XPath("//XCUIElementTypeTextField[@value='Type your address']");
        public By AddressSelection => By.XPath("//android.widget.TextView[@index='1']");
        public By AddressSelectionIos => By.XPath("//XCUIElementTypeTable/XCUIElementTypeCell/XCUIElementTypeStaticText");
        public By AddressButton => By.Id("search");
        public By EnterAddressManuallyButton => By.Id("enterManuallyButton");
        public By EnterAddressManuallyButtonIos => By.Name("Enter manually");
        public By LookUpMyAddressButtonIos => By.Name("Look up my address");
        public By Unit => By.Id("unit");
        public By UnitIos => By.XPath("//XCUIElementTypeTextField[@value='Unit / Apartment / Suite / Building']");
        public By StreetNumber => By.Id("streetNumber");
        public By StreetNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Street number']");
        public By StreetName => By.Id("streetName");
        public By StreetNameIos => By.XPath("//XCUIElementTypeTextField[@value='Street name']");
        public By Suburb => By.Id("suburb");
        public By SuburbIos => By.XPath("//XCUIElementTypeTextField[@value='Suburb']");
        public By State => By.Id("state");
        public By StateIos => By.XPath("//XCUIElementTypeTextField[@value='State / Territory']");
        public By AddressState => By.XPath("//android.widget.CheckedTextView[@text='NSW']");
        public By PostCode => By.Id("postcode");
        public By PostCodeIos => By.XPath("//XCUIElementTypeTextField[@value='Postcode']");
        public By WhereDoYouLiveText => By.XPath("//XCUIElementTypeStaticText[@value='Where do you live?']");
        public By StreetType => By.Id("streetType");
        public By StreetTypeIos => By.XPath("//XCUIElementTypeTextField[@value='Street type']");
        public By DriversLicence => By.Id("licence");
        public By DriversLicenceIos => By.Name("Driver Licence");
        public By Passport => By.Id("passport");
        public By PassportIos => By.Name("Passport");
        public By AustralianPassportNumber => By.Id("number");
        public By AustralianPassportNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Australian passport number']");
        public By GenderPassport => By.Id("gender");
        public By GenderPassportIos => By.XPath("//XCUIElementTypeTextField[@value='Gender']");
        public By GenderPassportMale => By.XPath("//android.widget.CheckedTextView[@text='Male']");
        public By GenderPassportFemale => By.XPath("//android.widget.CheckedTextView[@text='Female']");
        public By GenderPassportPreferNotToSay => By.XPath("//android.widget.CheckedTextView[@text='Prefer not to say']");
        public By ExpiryDateAustralianPassport => By.Id("expiry");
        public By ExpiryDateAustralianPassportIos => By.XPath("//XCUIElementTypeTextField[@value='DD / MM / YYYY']");
        public By TellUsYourPassportDetailsText => By.XPath("//XCUIElementTypeStaticText[@value='Tell us your passport details.']");
        public By TellUsYourMedicareDetailsText => By.XPath("//XCUIElementTypeStaticText[@value='Tell us your Medicare details.']");
        public By LicenseNumber => By.Id("number");
        public By LicenseNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Licence number']");
        public By LicenseCardNumber => By.Id("cardNumber");
        public By LicenseExpiryDate => By.Id("expiry");
        public By LicenseExpiryDateIos => By.XPath("//XCUIElementTypeTextField[@value='DD / MM / YYYY']");
        public By TellusyourLicenseDetailsText => By.XPath("//XCUIElementTypeStaticText[@value='Tell us your licence details.']");
        public By StateOfIssueLicenseText => By.Id("state");
        public By Medicare => By.Id("medicare");
        public By MedicareIos => By.Name("Medicare");
        public By MedicareFullName => By.Id("fullName");
        public By MedicareFullNameIos => By.XPath("//XCUIElementTypeTextField[@value='Full name']");
        public By MedicareCardNumber => By.Id("number");
        public By MedicareCardNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Medicare card number']");
        public By MedicareColor => By.Id("colour");
        public By MedicareColorIos => By.XPath("//XCUIElementTypeTextField[@value='Colour']");
        public By GreenMedicareColor => By.XPath("//android.widget.CheckedTextView[@text='Green']");
        public By BlueMedicareColor => By.XPath("//android.widget.CheckedTextView[@text='Blue']");
        public By YellowMedicareColor => By.XPath("//android.widget.CheckedTextView[@text='Yellow']");
        public By MedicareReferenceNumber => By.Id("refNo");
        public By MedicareReferenceNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Reference number']");
        public By MedicareExpiryDate => By.Id("expiry");
        public By MedicareExpiryDateIos => By.XPath("//XCUIElementTypeTextField[@value='MM / YYYY']");
        public By StateOfIssueLicenseRadioButtonNSW => By.XPath("//android.widget.CheckedTextView[@text='NSW']");
        public By AccessSeekerCheck => By.Id("accessSeekerCheck");
 
        public By TermsAndPolicyCheck => By.Id("termsAndPolicyCheck");
        public By StatusCheck => By.Id("statusCheck");
        public By CardHolderName => By.Id("name");
        public By CardHolderNameIos => By.XPath("//XCUIElementTypeTextField[@value='Cardholder name']");
        public By CardNumber => By.Id("number");
        public By CardNumberIos => By.XPath("//XCUIElementTypeTextField[@value='Card number']");
        public By CardExpiryDate => By.Id("expiry");
        public By CardExpiryDateIos => By.XPath("//XCUIElementTypeTextField[@value='MM / YY']");
        public By CardCvc => By.Id("cvc");
        public By CardCvcIos => By.XPath("//XCUIElementTypeTextField[@value='CVC']");
        public By AddCardButton => By.XPath("//android.widget.Button[@text='ADD CARD']");
        public By AddCardButtonIos => By.Name("Add Card");
        public By AddCardButtonText => By.XPath("//XCUIElementTypeStaticText[@value='Add a card']");
        public By PickAPIN => By.Id("pinEntry");
        public By PickAPINIos => By.XPath("//XCUIElementTypeStaticText[@value='Pick a PIN']");
        public By ReTypeYourPinIos => By.XPath("//XCUIElementTypeStaticText[@value='Retype your PIN']");
        public By NavigationHome => By.Id("navigation_home");
        public By OtcEntry => By.Id("otcEntry");
        public By OtcEntryIos => By.XPath("//XCUIElementTypeScrollView/XCUIElementTypeOther/XCUIElementTypeTextField");
        #endregion

        public SignUp(IWebDriver driver) : base(driver)
        {

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

        public void ClickOtcEntry()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(OtcEntryIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(OtcEntry);
                    break;
            }
        }
        public void EnterOtcEntry(string mobileNumber, string otc)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(OtcEntryIos, otc);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(OtcEntry, otc);
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
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(LoginButton);
                    break;
            }

        }
        public void ClickingChangeFlowButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
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

        public void ClickTitle()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(Title);
                    break;
            }
        }
        public void SelectTitleMr()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(TitleMr);
                    break;
            }
        }
        public void SelectTitleMrs()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(TitleMrs);
                    break;
            }
        }
        public void SelectTitleMiss()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(TitleMiss);
                    break;
            }
        }
        public void SelectTitleMs()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(TitleMs);
                    break;
            }
        }
        public void ClickConfirm()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(ConfirmButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(ConfirmButton);
                    break;
            }
        }
        public void ClickConfirmButtonIos()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(ConfirmButtonIos);
                    break;
                case "Android":
                    break;
            }
        }

        public void EnterFirstName(string firstName)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(FirstNameIos, firstName);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(FirstName, firstName);
                    break;
            }
        }

        public void EnterMiddleName(string middleName)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(MiddleNameIos, middleName);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(MiddleName, middleName);
                    Driver.Swipe(0, 300, 0, 50, 10);
                    break;
            }
        }
        public void EnterLastName(string lastName)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(LastNameIOs, lastName);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(LastName, lastName);
                    break;
            }
        }

        public void EnterDateOfBirth(string dob)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(DateOfBirthIos);
                    Driver.WaitForElementToBeLocatedAndEnterText(DateOfBirthIos, dob);
                    ClickNext();
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(DateOfBirth);
                    Driver.WaitForElementToBeLocatedAndEnterText(DateOfBirth, dob);
                    ClickNext();
                    break;
            }
        }

        public void EnterEmailAddress(string emailAddress)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(EmailIdIos);
                    Driver.WaitForElementToBeLocatedAndEnterText(EmailIdIos, emailAddress);
                    ClickNext();
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(EmailId);
                    Driver.WaitForElementToBeLocatedAndEnterText(EmailId, emailAddress);
                    ClickNext();
                    break;
            }
        }

        public void EnterAddress(string address)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(AddressTextIos, address);
                    Driver.WaitForElementToBeLocatedAndClick(AddressSelectionIos);
                    ClickNext();

                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(AddressButton, address);
                    Driver.WaitForElementToBeLocatedAndClick(SearchResultContentForAddress);
                    ClickNext();
                    break;
            }
        }
        public void ClickEnterAddressManuallyButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(EnterAddressManuallyButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(EnterAddressManuallyButton);
                    break;
            }
        }
        public void EnterUnitNumber(string unit)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(UnitIos, unit);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(Unit, unit);
                    break;
            }
        }

        public void EnterStreetNumber(string streetNumber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(StreetNumberIos, streetNumber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(StreetNumber, streetNumber);
                    break;
            }
        }

        public void EnterStreetName(string streetName)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(StreetNameIos, streetName);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(StreetName, streetName);
                    break;
            }
        }
        public void EnterStreetType(string streetType)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(StreetTypeIos, streetType);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(StreetType, streetType);
                    break;
            }
        }

        public void EnterSuburb(string suburb)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(SuburbIos, suburb);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(Suburb, suburb);
                    break;
            }
        }

        public void SelectState()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(StateIos, "NSW");
                    Driver.WaitForElementToBeLocatedAndClick(CloseButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(State);
                    Driver.WaitForElementToBeLocatedAndClick(AddressState);
                    ClickConfirm();
                    break;
            }
        }
        public void EnterPostCode(string postcode)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(PostCodeIos, postcode);
                    Driver.WaitForElementToBeLocatedAndEnterText(WhereDoYouLiveText, postcode);
                    ClickNext();
                    break;
                case "Android":
                    ((AppiumDriver<IWebElement>)Driver).HideKeyboard();
                    Driver.WaitForElementToBeLocatedAndEnterText(PostCode, postcode);
                    ((AppiumDriver<IWebElement>)Driver).HideKeyboard();
                    ClickNext();
                    break;
            }
        }

        public void ClickDrivingLicenseButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(DriversLicenceIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(DriversLicence);
                    break;

            }
        }

        public void ClickStateOfIssueLicense()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(StateOfIssueLicenseText);
                    break;
            }
        }
        public void SelectDrivingLicenseStateOfIssueForNSW()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(StateOfIssueLicenseRadioButtonNSW);
                    break;
            }
        }
        public void EnterDrivingLicenseNumber(string licenseNumber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(LicenseNumberIos, licenseNumber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(LicenseNumber, licenseNumber);
                    break;
            }
        }

        public void EnterLicenseCardNumber(string licenseCardNumber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(LicenseCardNumber, licenseCardNumber);
                    break;
            }
        }
        public void EnterDrivingLicenseExpiryDate(string licenseExpiryDate)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(LicenseExpiryDateIos, licenseExpiryDate);
                    Driver.WaitForElementToBeLocatedAndClick(TellusyourLicenseDetailsText);
                    ClickNext();
                    break;
                case "Android":
                    ((AndroidDriver<IWebElement>)Driver).HideKeyboard();
                    Driver.WaitForElementToBeLocatedAndEnterText(LicenseExpiryDate, licenseExpiryDate);
                    ((AndroidDriver<IWebElement>)Driver).HideKeyboard();
                    ClickNext();
                    break;
            }
        }
        public void ClickPassport()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(PassportIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(Passport);
                    break;
            }
        }
        public void EnterAustralianPassportNumber(string passportNUmber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(AustralianPassportNumberIos, passportNUmber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(AustralianPassportNumber, passportNUmber);
                    break;
            }
        }
        public void EnterPassportExpiryDate(string passportExpiryDate)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(ExpiryDateAustralianPassportIos, passportExpiryDate);
                    Driver.WaitForElementToBeLocatedAndEnterText(TellUsYourPassportDetailsText, passportExpiryDate);
                    ClickNext();
                    break;
                case "Android":
                    ((AppiumDriver<IWebElement>)Driver).HideKeyboard();
                    Driver.WaitForElementToBeLocatedAndEnterText(ExpiryDateAustralianPassport, passportExpiryDate);
                    ClickNext();
                    break;
            }
        }

        public void ClickGenderPassport()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(GenderPassportIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(GenderPassport);
                    break;
            }
        }

        public void SelectMaleGenderPassport()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(GenderPassportMale);
                    break;
            }
        }
        public void SelectFemaleGenderPassport()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(GenderPassportFemale);
                    break;
            }
        }
        public void SelectGenderPassportPreferNotToSay()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(GenderPassportPreferNotToSay);
                    break;
            }
        }

        public void ClickMedicare()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(MedicareIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(Medicare);
                    break;
            }
        }
        public void EnterMedicareFullName(string medicareName)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareFullNameIos, medicareName);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareFullName, medicareName);
                    break;
            }
        }
        public void EnterMedicareCardNumber(string medicareCardNumber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareCardNumberIos, medicareCardNumber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareCardNumber, medicareCardNumber);
                    break;
            }
        }

        public void ClickMedicareColor()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(MedicareColorIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(MedicareColor);
                    break;
            }
        }
        public void SelectMedicareColor(string medicareColor)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    if (medicareColor == "Green")
                    {
                        Driver.WaitForElementToBeLocatedAndClick(GreenMedicareColor);
                    }
                    else if (medicareColor == "Blue")
                    {
                        Driver.WaitForElementToBeLocatedAndClick(BlueMedicareColor);
                    }
                    else
                    {
                        Driver.WaitForElementToBeLocatedAndClick(YellowMedicareColor);
                    }
                    Driver.WaitForElementToBeLocatedAndClick(ConfirmButton);
                    break;
            }
        }

        public void EnterMedicareReferenceNumber(string referenceNumber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareReferenceNumberIos, referenceNumber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareReferenceNumber, referenceNumber);
                    break;
            }
        }

        public void EnterMedicareExpiryDate(string medicareExpiryDate)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(TellUsYourMedicareDetailsText);
                    Driver.WaitForElementToBeLocatedAndClick(MedicareExpiryDateIos);
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareExpiryDateIos, medicareExpiryDate);
                    Driver.WaitForElementToBeLocatedAndClick(TellUsYourMedicareDetailsText);
                    ClickNext();
                    break;
                case "Android":
                    ((AppiumDriver<IWebElement>)Driver).HideKeyboard();
                    Driver.WaitForElementToBeLocatedAndEnterText(MedicareExpiryDate, medicareExpiryDate);
                    ((AppiumDriver<IWebElement>)Driver).HideKeyboard();
                    ClickNext();
                    break;
            }
        }
        public void DoubleCheckingInformationIos()
        {
            Driver.ScrollDown();
            IList<IWebElement> list = Driver.FindElements(By.Name("unchecked"));
                foreach (IWebElement element in list)
            {
                element.ClickElement();
            }       
        }
        public void SelectAccessSeeker()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(AccessSeekerCheck);
                    break;
            }
        }

        public void SelectTermsAndPolicy()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(TermsAndPolicyCheck);
                    break;
            }
        }
        public void SelectStatus()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    break;
                case "Android":
                    IWebElement element = Driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector()).scrollIntoView(resourceId(\"com.shophumm:id/statusCheck\"));"));
                    element.Click();
                    break;
            }
        }
        public void EnterCardHolderName(string cardHolderName)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(CardHolderNameIos, cardHolderName);
                    break;
                case "Android":
                    Thread.Sleep(2000);
                    Driver.WaitForElementToBeLocatedAndEnterText(CardHolderName, cardHolderName);
                    break;
            }
        }
        public void EnterCardNumber(string cardNumber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(CardNumberIos, cardNumber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(CardNumber, cardNumber);
                    break;
            }
        }

        public void EnterCardExpiryDate(string cardExpiryDate)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(CardExpiryDateIos, cardExpiryDate);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(CardExpiryDate, cardExpiryDate);
                    break;
            }
        }
        public void EnterCardCVC(string cardCVC)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(CardCvcIos, cardCVC);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(CardCvc, cardCVC);
                    break;
            }
        }
        public void ClickAddCardButton()
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndClick(AddCardButtonIos);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndClick(AddCardButton);
                    break;
            }

        }

        public void PickAPin(string pinNUmber)
        {
            switch (TestBase.mobileOS)
            {
                case "IOS":
                    Driver.WaitForElementToBeLocatedAndEnterText(PickAPINIos, pinNUmber);
                    Driver.WaitForElementToBeLocatedAndEnterText(ReTypeYourPinIos, pinNUmber);
                    break;
                case "Android":
                    Driver.WaitForElementToBeLocatedAndEnterText(PickAPIN, pinNUmber);
                    Driver.WaitForElementToBeLocatedAndEnterText(PickAPIN, pinNUmber);
                    break;
            }



        }
    }
}
#endregion




