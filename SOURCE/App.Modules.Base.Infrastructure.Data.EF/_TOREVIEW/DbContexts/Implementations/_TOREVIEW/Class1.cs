using System;
using App.Modules.Base.Common.Models;
using App.Modules.Base.Common.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Base.Infrastructure.EF
{
    public class BaseModuleDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        DbSet<Person> People;

        public BaseModuleDbContext(DbContextOptions options) : base(options)
        {

        }

        protected BaseModuleDbContext()
        {
        }
    }
}