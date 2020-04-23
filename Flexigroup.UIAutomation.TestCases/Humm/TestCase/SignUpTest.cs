using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flexigroup.UIAutomation.TestCases.Runner;
using Flexigroup.UIAutomation.Core;
using Flexigroup.UIAutomation.Api.Humm.MobileApp.Workflow;

namespace Flexigroup.UIAutomation.TestCases.Humm
{
    [TestClass]
    public class SignUpTest : TestRunner
    {
        
        /// <summary>
        /// This method is to signup with license
        /// </summary>
        [DataSource(dataSourceSettingName: "HummLicense")]
        [TestCategory("HummSignUp"), TestMethod]
        public void HummSignUpWithLicense()
        {
            Test(() =>
            {
                var signUp = new SignUpWorkflow(Driver, extentTest);
                signUp.DeleteUser(DataLoad.GetData("Username"));
                signUp.EnteringPersonalInformationwithPassword("Auto");
                signUp.EnteringLicenseDetails(DataLoad.GetData("licenseNumber"), 
                DataLoad.GetData("licenseCardNumber"), DataLoad.GetData("licenseExpiryDate"));
                signUp.DoubleCheckingInformation();
                signUp.AddingCardInformation(DataLoad.GetData("cardHolderName"), DataLoad.GetData("cardNumber"), DataLoad.GetData("cardExpiryDate"), DataLoad.GetData("cardCVC"));
                signUp.PickingAPin(DataLoad.GetData("pinNumber"));
                signUp.ValidateSignUp();
                signUp.DeleteUser(DataLoad.GetData("Username"));
            });
        }
        /// <summary>
        /// This method is to signup with license and entering address manually
        /// </summary>
        [DataSource(dataSourceSettingName: "HummLicense")]
        [TestCategory("HummSignUp"), TestMethod]
        public void HummSignUpWithLicenseManualAddress()
        {
            Test(() =>
            {
                var signUp = new SignUpWorkflow(Driver, extentTest);
                signUp.DeleteUser(DataLoad.GetData("Username"));
                signUp.EnteringPersonalInformationwithPassword("manual");
                signUp.EnteringLicenseDetails(DataLoad.GetData("licenseNumber"), DataLoad.GetData("licenseCardNumber"), DataLoad.GetData("licenseExpiryDate"));
                signUp.DoubleCheckingInformation();
                signUp.AddingCardInformation(DataLoad.GetData("cardHolderName"), DataLoad.GetData("cardNumber"), DataLoad.GetData("cardExpiryDate"), DataLoad.GetData("cardCVC"));
                signUp.PickingAPin(DataLoad.GetData("pinNumber"));
                signUp.ValidateSignUp();
                signUp.DeleteUser(DataLoad.GetData("Username"));
            });
        }
        /// <summary>
        /// This method is to signup with passport of the user
        /// </summary>
        [DataSource(dataSourceSettingName: "HummPassport")]
        [TestCategory("HummSignUp"), TestMethod]
        public void HummSignUpWithPassport()
        {
            Test(() =>
            {
                var signUp = new SignUpWorkflow(Driver, extentTest);
                signUp.DeleteUser(DataLoad.GetData("Username"));
                signUp.EnteringPersonalInformationwithPassword("Auto");
                signUp.EnteringPassportDetails(DataLoad.GetData("passportNumber"), DataLoad.GetData("passportGender"), DataLoad.GetData("passportExpiryDate"));
                signUp.DoubleCheckingInformation();
                signUp.AddingCardInformation(DataLoad.GetData("cardHolderName"), DataLoad.GetData("cardNumber"), DataLoad.GetData("cardExpiryDate"), DataLoad.GetData("cardCVC"));
                signUp.PickingAPin(DataLoad.GetData("pinNumber"));
                signUp.ValidateSignUp();
                signUp.DeleteUser(DataLoad.GetData("Username"));
            });
        }
        /// <summary>
        /// This method is to signup with Medicare card of the user
        /// </summary>
        [DataSource(dataSourceSettingName: "HummMedicare")]
        [TestCategory("HummSignUp"), TestMethod]
        public void HummSignUpWithMedicare()
        {
            Test(() =>
            {
                var signUp = new SignUpWorkflow(Driver, extentTest);
                signUp.DeleteUser(DataLoad.GetData("Username"));
                signUp.EnteringPersonalInformationwithPassword("Auto");
                signUp.EnteringMedicareDetails(DataLoad.GetData("name"), DataLoad.GetData("medicareCardNumber"), DataLoad.GetData("medicareColor"), DataLoad.GetData("medicareReferenceNumber"), DataLoad.GetData("medicareExpDate"));
                signUp.DoubleCheckingInformation();
                signUp.AddingCardInformation(DataLoad.GetData("cardHolderName"), DataLoad.GetData("cardNumber"), DataLoad.GetData("cardExpiryDate"), DataLoad.GetData("cardCVC"));
                signUp.PickingAPin(DataLoad.GetData("pinNumber"));
                signUp.ValidateSignUp();
                signUp.DeleteUser(DataLoad.GetData("Username"));
            });
        }

    }

































































































































}

