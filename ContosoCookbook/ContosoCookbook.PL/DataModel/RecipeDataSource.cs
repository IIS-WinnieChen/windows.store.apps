﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Net.Http;
using Windows.Data.Json;
using Windows.ApplicationModel;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage;
using System.Net.Http.Headers;
using ContosoCookbook.DataModel;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace ContosoCookbook.Data
{
    /// <summary>
    /// Base class for <see cref="RecipeDataItem"/> and <see cref="RecipeDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class RecipeDataCommon : ContosoCookbook.Common.BindableBase
    {
        internal static Uri _baseUri = new Uri("ms-appx:///");

        public RecipeDataCommon(String uniqueId, String title, String shortTitle, String imagePath)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._shortTitle = shortTitle;
            this._imagePath = imagePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _shortTitle = string.Empty;
        public string ShortTitle
        {
            get { return this._shortTitle; }
            set { this.SetProperty(ref this._shortTitle, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;

        public Uri ImagePath
        {
            get
            {
                return new Uri(RecipeDataCommon._baseUri, this._imagePath);
            }
        }

        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(RecipeDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public string GetImageUri()
        {
            return _imagePath;
        }
    }

    /// <summary>
    /// Recipe item data model.
    /// </summary>
    public class RecipeDataItem : RecipeDataCommon
    {
        public RecipeDataItem()
            : base(String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }

        public RecipeDataItem(String uniqueId, String title, String shortTitle, String imagePath, int preptime, String directions, ObservableCollection<string> ingredients, RecipeDataGroup group)
            : base(uniqueId, title, shortTitle, imagePath)
        {
            this._preptime = preptime;
            this._directions = directions;
            this._ingredients = ingredients;
            this._group = group;
        }

        private int _preptime = 0;
        public int PrepTime
        {
            get { return this._preptime; }
            set { this.SetProperty(ref this._preptime, value); }
        }

        private string _directions = string.Empty;
        public string Directions
        {
            get { return this._directions; }
            set { this.SetProperty(ref this._directions, value); }
        }

        private ObservableCollection<string> _ingredients;
        public ObservableCollection<string> Ingredients
        {
            get { return this._ingredients; }
            set { this.SetProperty(ref this._ingredients, value); }
        }

        private RecipeDataGroup _group;
        public RecipeDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }

        private ImageSource _tileImage;
        private string _tileImagePath;

        public Uri TileImagePath
        {
            get
            {
                return new Uri(RecipeDataCommon._baseUri, this._tileImagePath);
            }
        }

        public ImageSource TileImage
        {
            get
            {
                if (this._tileImage == null && this._tileImagePath != null)
                {
                    this._tileImage = new BitmapImage(new Uri(RecipeDataCommon._baseUri, this._tileImagePath));
                }
                return this._tileImage;
            }
            set
            {
                this._tileImagePath = null;
                this.SetProperty(ref this._tileImage, value);
            }
        }

        public void SetTileImage(String path)
        {
            this._tileImage = null;
            this._tileImagePath = path;
            this.OnPropertyChanged("TileImage");
        }
    }

    /// <summary>
    /// Recipe group data model.
    /// </summary>
    public class RecipeDataGroup : RecipeDataCommon
    {
        public RecipeDataGroup()
            : base(String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }

        public RecipeDataGroup(String uniqueId, String title, String shortTitle, String imagePath, String description)
            : base(uniqueId, title, shortTitle, imagePath)
        {
        }

        private ObservableCollection<RecipeDataItem> _items = new ObservableCollection<RecipeDataItem>();
        public ObservableCollection<RecipeDataItem> Items
        {
            get { return this._items; }
        }

        public IEnumerable<RecipeDataItem> TopItems
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

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private ImageSource _groupImage;
        private string _groupImagePath;

        public ImageSource GroupImage
        {
            get
            {
                if (this._groupImage == null && this._groupImagePath != null)
                {
                    this._groupImage = new BitmapImage(new Uri(RecipeDataCommon._baseUri, this._groupImagePath));
                }
                return this._groupImage;
            }
            set
            {
                this._groupImagePath = null;
                this.SetProperty(ref this._groupImage, value);
            }
        }

        public int RecipesCount
        {
            get
            {
                return this.Items.Count;
            }
        }

        public void SetGroupImage(String path)
        {
            this._groupImage = null;
            this._groupImagePath = path;
            this.OnPropertyChanged("GroupImage");
        }
    }

    /// <summary>
    /// Creates a collection of groups and items.
    /// </summary>
    public sealed class RecipeDataSource
    {
        //public event EventHandler RecipesLoaded;

        private static RecipeDataSource _recipeDataSource = new RecipeDataSource();

        private ObservableCollection<RecipeDataGroup> _allGroups = new ObservableCollection<RecipeDataGroup>();
        public ObservableCollection<RecipeDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<RecipeDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _recipeDataSource.AllGroups;
        }

        public static RecipeDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _recipeDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static RecipeDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _recipeDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static  void PopulateRecipes(IEnumerable<RecipeDataItemDto> recipeDataItemDtos)
        {
            foreach (var recipeDto in recipeDataItemDtos)
            {
                var recipeGroup = GetGroup(recipeDto.Group.UniqueId);
                if (recipeGroup  == null)
                {
                    recipeGroup = new RecipeDataGroup(recipeDto.Group.UniqueId, recipeDto.Group.Title, recipeDto.Group.ShortTitle, recipeDto.Group.ImagePath, recipeDto.Group.Description);
                    recipeGroup.SetImage(recipeDto.Group.ImagePath);
                    recipeGroup.SetGroupImage(recipeDto.Group.GroupImagePath);
                    _recipeDataSource.AllGroups.Add(recipeGroup );
                }

                var recipe = new RecipeDataItem(recipeDto.UniqueId, 
                                    recipeDto.Title, 
                                    recipeDto.ShortTitle, 
                                    recipeDto.ImagePath, 
                                    recipeDto.PrepTime, 
                                    recipeDto.Directions, 
                                    new ObservableCollection<string>(recipeDto.Ingredients.ToList()), 
                                    recipeGroup);
                
                recipeGroup .Items.Add(recipe);

                recipe.SetImage(recipeDto.ImagePath);
                recipe.SetTileImage(recipeDto.TitleImagePath);
            }
        }

        /*
        public static async Task LoadLocalDataAsync()
        {
            // Retrieve recipe data from Recipes.txt
            var file = await Package.Current.InstalledLocation.GetFileAsync("Data\\Recipes.txt");
            var result = await FileIO.ReadTextAsync(file);

            // Parse the JSON recipe data
            var recipes = JsonArray.Parse(result);

            // Convert the JSON objects into RecipeDataItems and RecipeDataGroups
            JsonConvert.DeserializeObject<List<RecipeDataItem>>(result);
        }
        */
    }

}
