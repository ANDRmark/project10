﻿using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL_EF.Repositories
{
    class ThemeRepository : Repository<Theme> , IThemeRepository
    {

        public ThemeRepository(ForumDbContext context):base(context)
        {

        }


        IEnumerable<Theme> IThemeRepository.GetBySection(int sectionId)
        {
            return this.table.Where(t => t.SectionId == sectionId).ToList();
        }

        IEnumerable<Theme> IThemeRepository.SearchThemesByNamePart(string namePart)
        {
            return this.table.Where(t => t.Title.ToLower().IndexOf(namePart.ToLower()) > -1);
        }
    }
}
