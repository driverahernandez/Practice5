namespace Practice5_WebAPI.Authority
{
    // Model for users that will get registered against the authority
    // users of APIs are Applications.
    public class WebApp
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}
