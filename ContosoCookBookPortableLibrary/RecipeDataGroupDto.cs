using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace ContosoCookbook.DataModel
{
    public class RecipeDataGroupDto : RecipeDataCommonDto
    {
        private ObservableCollection<RecipeDataItemDto> _items = new ObservableCollection<RecipeDataItemDto>();
        public ObservableCollection<RecipeDataItemDto> Items
        {
            get { return this._items; }
        }

        public IEnumerable<RecipeDataItemDto> TopItems
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            get { return this._items.Take(12); }
        }

        public string Description
        {
            get;
            set;
        }

        [JsonProperty("groupImage")]
        public string GroupImagePath
        {
            get;
            set;
        }

        public int RecipesCount
        {
            get
            {
                return this.Items.Count;
            }
        }
    }
}