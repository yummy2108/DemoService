using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace DemoService.Models
{
    public class MyService
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}