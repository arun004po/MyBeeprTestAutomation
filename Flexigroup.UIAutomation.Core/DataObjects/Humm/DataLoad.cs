using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Flexigroup.UIAutomation.Core
{
    public static class DataLoad
    {
        private static Random _random = new Random();
        private static TestContext _testContext;

        public static void InitialiseTextContext(TestContext testContext)
        {
            _testContext = testContext;
        }
        public static string GetData(string data) // use random name like Selenium{random:6}
         {
            if (_testContext == null || _testContext.DataRow == null) return null;
            var value = _testContext.DataRow[data].ToString();
            if (value.Contains("{random:"))
            {
                var match = Regex.Match(value, @"\d+").Value;
                var random = GetRandom(int.Parse(match));
                value = Regex.Replace(value, @"\d+", "");
                value = value.Replace("{random:", random);
                value = value.Replace("}", "");
            }
            return value;
        }

        public static string GetRandom(int length)
        {
            var random = string.Empty;
            for (var i = 0; i < length; i++)
            {
                random += GetLetter();
            }
            return random;
        }

        private static char GetLetter()
        {
            var num = _random.Next(0, 26);
            var letter = (char)('a' + num);
            return letter;
        }

        public static DateTime RandomDate(int startYear, int endYear)
        {
            DateTime start = new DateTime(startYear, 1, 1);
            DateTime end = new DateTime(endYear, 1, 1);
            int range = (end - start).Days;
            return start.AddDays(_random.Next(range));

        }

        public static string GetExpiryDate(int length)
        {
            string month = _random.Next(01, 13).ToString("D2");
            string year = _random.Next(21, 23).ToString("D2");
            return month + year;
        }
        public static int RandomNumber()
        {
            int r = 0;
            int i;
            for (i = 1; i < 11; i++)
            {
                r += _random.Next(0, 1000);
            }
            return r;
        }
        public static int RandomNumber(int number)
        {
            number = 0;
            int i;
            for (i = 1; i < 11; i++)
            {
                number += _random.Next(0, 10000);
            }
            return number;
        }


        public static void Replace(StringBuilder xml, string key, string value)
        {
            xml.Replace("{" + key + "}", value);
        }

        public static string FixStreet(string street)
        {
            switch (street.ToUpper())
            {
                case "ST":
                    return "Street";
                case "RD":
                    return "Road";
                case "CT":
                    return "Court";
                case "AVE":
                    return "Avenue";
                case "PL":
                    return "Place";
                case "DR":
                    return "Drive";
                case "LA":
                    return "Lane";
                case "BNK":
                    return "Bank";
                case "RI":
                    return "Rise";
                case "ESP":
                    return "Esplanade";
                default:
                    return "Street";
            }
        }
    }
}

