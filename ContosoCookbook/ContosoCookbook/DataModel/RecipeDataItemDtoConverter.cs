using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ContosoCookbook.DataModel
{
    public class RecipeDataItemDtoConverter : CustomCreationConverter<RecipeDataItemDto>
    {
        public override RecipeDataItemDto Create(Type obectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            var recipe = new RecipeDataItemDto(jObject.Value<string>("title"),
                                               jObject.Value<string>("key"),
                                               jObject.Value<int>("preptime"),
                                               jObject.Value<string>("shortTitle"),
                                               jObject.Value<string>("directions"),
                                               jObject.Value<string>("titleImage"),
                                               jObject.Value<string>("backgroundImage"));

            recipe.Group = JsonConvert.DeserializeObject<RecipeDataGroupDto>(jObject.GetValue("group").ToString());

            foreach (var item in jObject.Value<JArray>("ingredients"))
            {
                recipe.Ingredients.Add(item.Value<string>());
            }

            return recipe;
        }
    }
}