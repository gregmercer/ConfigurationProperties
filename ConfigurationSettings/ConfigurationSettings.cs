using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ConfigurationSettings
{
    public class ConfigurationSettingsProperty
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class ConfigurationSettingsProperties
    {
        public List<ConfigurationSettingsProperty> properties { get; set; }
    }

    public class ConfigurationSettings
    {
        public string PROPERTY_LINK_HELP_SUPPORT = "link_HelpAndSupport";
        public string PROPERTY_LINK_PRIVACY_POLICY = "link_PrivacyPolicy";
        public string PROPERTY_LINK_SUBMIT_FEEDBACK = "link_SubmitFeedback";

        private string configurationSettingsUrl = "https://bitbucket.org/stanfordgsb/gsbgo-config/raw/master/main.json";

        private ConfigurationSettingsProperties csProperties = null;
        private Dictionary<string, string> propDictionary =  new Dictionary<string, string>();

        private static ConfigurationSettings _instance = null;
        private ConfigurationSettings()
        {
        }

        public static ConfigurationSettings getInstance()
        {
            if (_instance == null) {
                _instance = new ConfigurationSettings();
                _instance.fetchProperties().Wait();
                return _instance;
            }
            return _instance;
        }

        public async Task<string> fetchProperties()
        {
            if (csProperties == null)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    var result = await httpClient.GetStringAsync(configurationSettingsUrl);
                    csProperties = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationSettingsProperties>(result);
                    propDictionary = new Dictionary<string, string>();
                    foreach(ConfigurationSettingsProperty property in csProperties.properties)
                    {
                        propDictionary.Add(property.name, property.value);
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return "";
        }

        public string getProperty(string propertyName)
        {
            if (_instance.propDictionary.ContainsKey(propertyName) == true)
            {
                return _instance.propDictionary[propertyName];
            }
            return "";
        }
    }
}
