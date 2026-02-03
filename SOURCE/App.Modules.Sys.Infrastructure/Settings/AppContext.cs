namespace App.Base.Infrastructure.Configuration
{
    /// <summary>
    /// Context information about the 
    /// <see cref="AppInformation"/>.
    /// </summary>
    public class AppContext
    {

        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|
        /// <summary>
        /// Gets the name of the machine under which process
        /// is running.
        /// </summary>
        public string? MachineName { get; set; }

        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|

        /// <summary>
        /// Gets the Network Domain Name under which the base process thread User is running.
        /// </summary>
        public string? ThreadUserDomainName { get; set; }
        /// <summary>
        /// Gets the base process thread User is running.
        /// </summary>
        public string? ThreadUserName { get; set; }

        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|
        /// <summary>
        /// Gets the name of the OS Platform
        /// Example: <c>Win32NT</c>
        /// </summary>
        public string? OSPlatform { get; set; }

        /// <summary>
        /// Gets the version of the OS platform.
        /// Example: <c>10.0.19044.0</c>
        /// </summary>
        public Version? OSPlatformVersion { get; set; }

        /// <summary>
        /// Gets the version of the OS platform.
        /// Example: <c>Microsoft Windows NT 10.0.19044.0</c>
        /// </summary>
        public string? OSPlatformVersionString { get; set; }

        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|


        /// <summary>
        /// Chip Architecture Enumeration
        /// </summary>
        public System.Runtime.InteropServices.Architecture Architecture { get; set; }

        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|

        /// <summary>
        /// Version of .NET
        /// </summary>
        public Version? FrameworkVersion { get; set; }

        /// <summary>
        /// Displayable Title description of the Framwork
        /// <para>
        /// eg: "NetCoreApp, Version=v2.1"
        /// </para>
        /// </summary>
        public string? FrameworkTitle { get; set; }
        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|



        /// <summary>
        /// Gets the Path to the EXE of the Process.
        /// <para>
        /// eg: <c>C:\NOSYNC\REPOS\BASE.Jump.Dev\SOURCE\App.Service.Host\bin\debug\net7.0\App.Service.Host.exe</c> 
        /// (notice similarity to <see cref="BaseDirectoryPath"/>)
        /// </para>
        /// </summary>
        public string? ProcessPath { get; set; }

        /// <summary>
        /// Gets whether the Process is running as 64 bit.
        /// </summary>
        public bool ProcessIs64 { get; set; }

        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|




        /// <summary>
        /// Gets the Name of the Application
        /// <para>
        /// Example: <c>App.Service.Host.Web.Dev</c> (The *Host* Assembly)
        /// </para>
        /// </summary>
        public string? ApplicationName { get; set; }


        /// <summary>
        /// Name of the Environment.
        /// <para>
        /// Example: <c>Development</c>
        /// </para>
        /// </summary>
        public string? ApplicationEnvironmentName
        {
            get => _applicationEnvironmentName;
#pragma warning disable IDE0027 // Use expression body for accessor
            set
            {
                //if (!string.IsNullOrEmpty(_environmentName))
                //{
                //    new DevelopmentException($"Resetting {nameof(EnvironmentName)}.");
                //}
                _applicationEnvironmentName = value;
            }
#pragma warning restore IDE0027 // Use expression body for accessor
        }
#pragma warning disable IDE0032 // Use auto property
        private string? _applicationEnvironmentName;
#pragma warning restore IDE0032 // Use auto property

        // =========|=========|=========|=========|=========|=========|
        // =========|=========|=========|=========|=========|=========|

        /// <summary>
        /// Gets the ContentRootPath
        /// <para>
        /// Example: <c>"C:\NOSYNC\REPOS\BASE.Jump.Dev\SOURCE\App.Service.Host" (No slash on end).</c>
        /// </para>
        /// </summary>
        public string? ContentRootPath
        {
            get => _contentRootPath;
            set
            {
                if (!string.IsNullOrEmpty(_contentRootPath))
                {
#pragma warning disable CA2201 // Do not raise reserved exception types
                    throw new Exception($"Resetting {nameof(ContentRootPath)}.");
#pragma warning restore CA2201 // Do not raise reserved exception types
                }
                _contentRootPath = value;
            }
        }
        private string? _contentRootPath;


        /// <summary>
        /// Gets the Directory used to resolve
        /// default DLLs.
        /// <para>
        /// ContentRoot + bin + Debug + frameworktypeandVersion. 
        /// </para>
        /// <para>
        /// Example: <c>"//C:\NOSYNC\REPOS\BASE.Jump.Dev\SOURCE\App.Service.Host\bin\Debug\net7.0"</c> (Slash removed from end)
        /// </para>
        /// </summary>
        public string? BaseDirectoryPath
        {
            get => _BaseDirectoryPath;
            set
            {
                if (!string.IsNullOrEmpty(_BaseDirectoryPath))
                {
#pragma warning disable CA2201 // Do not raise reserved exception types
                    throw new Exception($"Resetting {nameof(BaseDirectoryPath)}.");
#pragma warning restore CA2201 // Do not raise reserved exception types
                }
                _BaseDirectoryPath = value;
            }
        }
        private string? _BaseDirectoryPath;


        /// <summary>
        /// Gets the Directory used to resolve
        /// default DLLs.
        /// <para>
        /// Example: <c>"C:\NOSYNC\REPOS\BASE.Jump.Dev\SOURCE\App.Service.Host\wwwroot"</c> (no slash on end)
        /// and wwwroot is updatable via Configuration, later.
        /// </para>
        /// </summary>
        public string? WebRootPath { get; set; }

        /// <summary>
        /// 
        /// <para>
        /// Example: <c>"C:\REPOS\BASE.Jump.Dev\SOURCE\App.Host\MODULES"</c>
        /// </para>
        /// </summary>
        public string? ModuleDirectoryPath { get; set; }

        /// <summary>
        /// 
        /// <para>
        /// Example: <c>"C:\REPOS\BASE.Jump.Dev\SOURCE\App.Service.Host\COMPONENTS"</c>
        /// </para>
        /// </summary>
        public string? ComponentDirectoryPath { get; set; }
    }
}