using RestSharp;
using System;
using Newtonsoft.Json.Linq;
using System.Net;
using log4net;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Flexigroup.UIAutomation.Core
{
    public class HummClient
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(HummClient));
        IDictionary<string, string> posRequest;

        public HummClient()
        {
        }

        public string GetUserDetails(string mobileNumber, string userDetail)
        {
            var client = new RestClient("https://test2-buyerapi.shophumm.co.nz");
            var request = new RestRequest("/api/test/v1/getuserdetails");
            request.AddParameter("mobile", mobileNumber);
            var response = client.Get(request);
            HttpStatusCode statusCode = response.StatusCode;

            if ((int)statusCode != 200)
            {
                throw new Exception($"Error after calling Get User Details.  Status code is {statusCode.ToString()}");
            }

            JObject o = JObject.Parse(response.Content);
            var userVariable = o[userDetail].ToString();
            return userVariable;
        }

        

        

        public void DeleteUser(string mobileNumber)
        {
            var client = new RestClient("https://test2-buyerapi.shophumm.co.nz");
            string resource = "api/test/v1/deleteuser?mobile=" + mobileNumber;
            var request = new RestRequest(resource);
            var response = client.Post(request);
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode.Equals(500))
            {
                throw new Exception($"Error after calling Delete User.  Status code is {statusCode.ToString()}");
            }

        }

       


        
        }

}
