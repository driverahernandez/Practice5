namespace Practice5_WebAPI.Authority
{
    public static class AppRepository
    {
        // Repository of registered users. For now, only user is 
        // the MVC web application in this solution. 
        public static List<WebApp> applications = new List<WebApp>()
        {
            new WebApp
            {
                ClientId = "MVCWebAppPractice7",
                Secret = "123ABC456"
            }
        };
        

    }
}