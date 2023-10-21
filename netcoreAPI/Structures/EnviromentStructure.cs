namespace netcoreAPI.Structures
{
    public struct EnviromentSettings
    {
        public const string AppContactName = "Bruno Canalini";
        public const string AppContactMail = "bncanalini@gmail.com";

        public const string SecuritySchemeAuthentication = "Bearer";
        public const string SecuritySchemeName = "Authorization";
        public const string SecuritySchemeBearerFormat = "JWT";
    }

    public struct EnviromentLanguage
    {
        public const string AppName = "NetCore API";
        public const string AppDescription = "NetCore API example, for more information go to https://github.com/rocketon85/netcoreAPI.";
        public const string AppVersionDeprecated = "This API version has been deprecated.";
        public const string AppVersionDeprecatedSunsetOn = "The API will be sunset on";

        public const string SecuritySchemeDescription = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1a2w5d1afffwq5f1rtyuyu1552a3\"";
    }

    public struct UserLanguage
    {
        public const string WrongUserPassword = "Username or password is incorrect";
      
    }
}
