namespace TheBrickVault.Components.Services
{
    public class RebrickableSettings
    {
        public string ApiKey { get; set; }
        public RebrickableSettings()
        {
            ApiKey = string.Empty;
        }

        public string GetApiKey()
        {

            return ApiKey;
        }



        //Intellisense suggested the block below. I don't know if I need it or not.  Will keep for now.

        //public string BaseUrl { get; set; }
        //public string SetsEndpoint { get; set; }
        //public string PartsEndpoint { get; set; }
        //public string InstructionsEndpoint { get; set; }
        //public string ImagesEndpoint { get; set; }
        //public string PartsListEndpoint { get; set; }
    }
}
