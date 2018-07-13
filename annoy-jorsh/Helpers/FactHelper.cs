using System;
using System.Threading.Tasks;
using Flurl.Http;

namespace annoyjorsh.Helpers
{
    public static class FactHelper
    {
        public static async Task<string> getCatFact(){
            var response = await "https://catfact.ninja/facts?limit=1".GetJsonAsync();
            return response.data[0].fact;
        }
        public static async Task<string> getDogFact()
        {
            var response = await "http://dog-api.kinduff.com/api/facts?number=1".GetJsonAsync();
            return response.facts[0];
        }
    }
}
