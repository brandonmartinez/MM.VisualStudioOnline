using System;
using System.Configuration;

namespace MM.VisualStudioOnline.IntegrationTests.Helpers
{
    internal static class Configuration
    {
        private static readonly Lazy<string> _visualStudioOnlineUserName =
            new Lazy<string>(() => ConfigurationManager.AppSettings["MM.VisualStudioOnline.UserName"]);

        private static readonly Lazy<string> _visualStudioOnlinePassword =
            new Lazy<string>(() => ConfigurationManager.AppSettings["MM.VisualStudioOnline.Password"]);

        private static readonly Lazy<string> _visualStudioOnlineAccountName =
            new Lazy<string>(() => ConfigurationManager.AppSettings["MM.VisualStudioOnline.AccountName"]);

        public static string VisualStudioOnlineUserName
        {
            get
            {
                return _visualStudioOnlineUserName.Value;
            }
        }

        public static string VisualStudioOnlinePassword
        {
            get
            {
                return _visualStudioOnlinePassword.Value;
            }
        }

        public static string VisualStudioOnlineAccountName
        {
            get
            {
                return _visualStudioOnlineAccountName.Value;
            }
        }
    }
}