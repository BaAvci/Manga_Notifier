using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Unit_Tests.Crawling
{
    public class LaunchSettingFixture : IDisposable
    {
        public LaunchSettingFixture()
        {
            try 
            { 
                using(var file = File.OpenText("../../../Properties/launchSettings.json"))
                {
                    var reader = new JsonTextReader(file);
                    var jObject = JObject.Load(reader);

                    var variables = jObject.GetValue("profiles")
                        //select a proper profile here
                        .SelectMany(profiles => profiles.Children())
                        .SelectMany(profiles => profiles.Children<JProperty>())
                        .Where(prop => prop.Name == "environmentVariables")
                        .SelectMany(prop => prop.Value.Children<JProperty>())
                        .ToList();

                    foreach(var variable in variables)
                    {
                        Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }

        public void Dispose() { }
    }
}
