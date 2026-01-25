using App.Base.Infrastructure.Services;
using App.Base.Infrastructure.Storage.Db.EF.DbContexts.Implementations.Base;
using App.Base.Infrastructure.Storage.Db.EF.Interceptors;
using Lamar.Scanning.Conventions;
using Microsoft.EntityFrameworkCore;

namespace App
{
    /// <summary>
    /// Scanner to teach the IoC to 
    /// find and register all implementions
    /// of 
    /// <see cref="IDbCommitPreCommitProcessingStrategy"/>
    /// which is the contract for processors/handlers that 
    /// are invoked by
    /// <see cref="IDbContextPreCommitService"/>
    /// when invoked in the 
    /// <see cref="DbContext.SaveChanges()"/>
    /// method 
    /// of 
    /// <see cref="ModuleDbContextBase"/>
    /// </summary>
    public static class IServiceScannerExtensions 
    {
        /// <summary>
        /// Teach Lamar to 
        /// scan all Assemblies for implementations of 
        /// <see cref="IDbCommitPreCommitProcessingStrategy"/>
        /// so that a base <c>DbCommit.SaveChanges</c>
        /// can invoke them.
        /// </summary>
        /// <param name="assemblyScanner"></param>
        public static void ScanForImplementationsOfIDbCommitPreCommitProcessingStrategies(this IAssemblyScanner assemblyScanner)
        {
            // Add all Pre-Commit Processors (these kick in just as you
            // Commit a DbContext, and ensure specific fields are 
            // automatically filled in)
            assemblyScanner.AddAllTypesOf<IDbCommitPreCommitProcessingStrategy>().NameBy(x => x.Name.Replace("Widget", "")); ;
        }

    }
}
