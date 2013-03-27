using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ContosoCookbook.DataModel
{
    public class RecipeDataAgent
    {
        public async Task<IEnumerable<RecipeDataItemDto>> GetRecipeDataItems()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://contosorecipes8.blob.core.windows.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.MaxResponseContentBufferSize = 1024 * 1024; // Read up to 1 MB of data
            var response = await client.GetAsync(("AzureRecipesRP"));
            var jsonTypeFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> 
                                {
                                    new RecipeDataItemDtoConverter()
                                }
                }
            };
            jsonTypeFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            return await response.Content.ReadAsAsync<List<RecipeDataItemDto>>(new[] { jsonTypeFormatter });;
        }
    }
}
