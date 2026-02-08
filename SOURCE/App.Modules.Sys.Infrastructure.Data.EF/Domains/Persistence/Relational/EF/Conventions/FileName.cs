using System;
using System.Collections.Generic;
using System.Text;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Conventions
{
    /// <summary>
    /// A convention to define a usable Db Table Name 
    /// name for a given entity
    /// (essentially adds an <c>s</c>, <c>ies</c>)
    /// depending on the ending of the entity name.
    /// <para>
    /// TODO: Admittedly it's pretty primitive!!!
    /// </para>
    /// <para>
    /// An implementation of 
    /// <see cref="IModelBuilderConvention"/>
    /// to make defining models easier and more predictable 
    /// when
    /// <see cref="DbContext.OnModelCreating(ModelBuilder)"/>
    /// invokes 
    /// <see cref="IModelBuilderOrchestrator"/>
    /// which inturn invokes instances of
    /// <see cref="IHasAppModuleDbContextModelBuilderInitializer"/>
    /// such as this class.
    /// </para>
    /// </summary>
    public class DefaultTableAndSchemaNamingConvention : IModelBuilderConvention
    {
        /// <summary>
        /// Given an Entity, it adds to the builder
        /// the table name of the model to be defined.
        /// <para>
        /// Generally the first Convention invoked
        /// from within an
        /// <see cref="IHasAppModuleDbContextModelBuilderInitializer"/>
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="schema"></param>
        public static void Define<T>(
            ModelBuilder modelBuilder,
            string schema = DbSchemaSchemaNameConstants.Default)
            where T : class
        {
            string name = typeof(T).Name.SimplePluralise();


            modelBuilder.Entity<T>().ToTable(name, schema);

        }
    }

}