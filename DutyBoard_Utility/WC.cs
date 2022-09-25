namespace DutyBoard_Utility
{
    public static class WC
    {
        public const string Success = "Success";
        public const string Error = "Error";
#if DEBUG
        public const string TokenTG0 = "8lZxMPtiF8Xjhk78wbag68hW1hPIUHqFweiIedS4oOE=";
        public const string TokenTG1 = "krf/ZwycKJLE51Yb1iobRw==";
        public const string TokenTG2 = "vPXdGKZnFBx3ObT5q9DwHQ==";
        public const string UrlTG = "https://fb45-85-174-198-67.eu.ngrok.io/";
#else

        public const string TokenTG0 = "Pt/Dj7+CE9bIPrryLZXD/BM2sopPWmkU5CudWAdwc2c=";
        public const string TokenTG1 = "QRcI8sE12+N614vVp037yw==";
        public const string TokenTG2 = "+qEtuhVooF2dIr/n1ZrAGQ==";
        public const string UrlTG = "https://dimmigg.somee.com/";
#endif

        public static string Path { get; set; }
    }
}
