using System;
using System.Threading.Tasks;

namespace ConfigurationSettings
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConfigurationSettings cs = ConfigurationSettings.getInstance();
            string helpUrl = cs.getProperty(cs.PROPERTY_LINK_HELP_SUPPORT);

            Console.WriteLine($"helpUrl = {helpUrl}");
        }
    }
}
