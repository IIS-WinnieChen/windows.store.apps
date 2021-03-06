﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoCookbook.DataModel
{
    public class RecipeDataItemDto : RecipeDataCommonDto
    {
        public RecipeDataItemDto(string title, string uniqueId, int prepTime, string shortTitle, string directions, string titleImage, string backgroundImage)
        {
            Title = Title;
            UniqueId = uniqueId;
            PrepTime = prepTime; 
            ShortTitle = shortTitle; 
            Directions = directions;
            TitleImagePath = titleImage;
            ImagePath = backgroundImage;
        }

        public int PrepTime
        {
            get;
            set;
        }

        public string Directions
        {
            get;
            set;
        }

        private ObservableCollection<string> _ingredients = new ObservableCollection<string>();
        public ObservableCollection<string> Ingredients
        {
            get { return this._ingredients; }
        }

        public RecipeDataGroupDto Group
        {
            get;
            set;
        }

        public string TitleImagePath
        {
            get;
            set;
        }
    }
}
