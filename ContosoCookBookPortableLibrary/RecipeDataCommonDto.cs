using System;
using Newtonsoft.Json;

namespace ContosoCookbook.DataModel
{
    public class RecipeDataCommonDto 
    {
        [JsonProperty("key")]
        public string UniqueId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string ShortTitle
        {
            get;
            set;
        }

        [JsonProperty("backgroundImage")]
        public String ImagePath
        {
            get;
            set;
        }
    }
}