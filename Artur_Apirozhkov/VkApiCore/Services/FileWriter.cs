using Newtonsoft.Json;

namespace Artur_Apirozhkov.VkApiCore.Services
{
    public class FileWriter
    {
        public void WriteJson(string filePath, object data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }
            File.WriteAllText(filePath, json);
        }
    }
}
