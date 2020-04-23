using System;

namespace Flexigroup.UIAutomation.Core
{
    public static class Constants
    {
        public static readonly TimeSpan DefaultTimeout = new TimeSpan(0, 0, 30);
        public static readonly TimeSpan ImplicitWait = new TimeSpan(0, 0, 30);
        public static readonly TimeSpan PageLoad = new TimeSpan(0, 0, 30);
        public const int DefaultRetryDelay = 5000;
        public const int DefaultRetryAttempts = 2;
        public const int DefaultThinkTime = 2000;
    }
}