﻿using Recommendation_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommendation_Service.Data.Interfaces
{
    public interface ICategoryRepository
    {
        void CreateNewCategory(Category category);
        List<Category> GetAllCategories();
    }
}
