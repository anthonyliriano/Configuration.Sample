using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Configuration.Sample
{
    public class TestApplication
    {
        public Settings AppSettings { get; set; }

        public TestApplication(IOptions<Settings> settings)
        {
            AppSettings = settings.Value;
        }

        public string DumpSettings()
        {
            return JsonSerializer.Serialize(AppSettings, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
