namespace SocialMedia.DI.Registry.ActionsFactory.ExternalLogins
{
    public class ExternalLoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }
        public string ExternalAccessToken { get; set; }

        public virtual string ToQueryString()
        {
            return string.Format("provider={0}&external_user_name={1}",
                                LoginProvider,
                                UserName);
        }
    }

    public class TwitterLoginData : ExternalLoginData
    {
        public string ScreenName { get; set; }
        public string UserId { get; set; }
        public string ExternalAccessTokenSecret { get; set; }
    }
}