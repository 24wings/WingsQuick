
using Microsoft.Extensions.Configuration;

namespace Wings.Admin.Services
{
    public class ConfigService
    {

        private IConfiguration Configuration { get; }
        public ConfigService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string url { get { return Configuration.GetValue<string>("url"); } }

    }
}