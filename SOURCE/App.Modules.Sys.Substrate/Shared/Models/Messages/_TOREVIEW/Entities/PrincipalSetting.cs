using App.Modules.Sys.Shared.Models.Entities.Base;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities
{

    /// <summary>
    /// System entity (not exposed to the system's exterior) for
    /// a Setting associated to a Principal.
    /// <para>
    /// TODO: Describe purpose better
    /// </para>
    /// </summary>
    public class PrincipalSetting : SettingBase
    {
        /// <summary>
        /// Id of the Principal/User who this Setting is for.
        /// </summary>
        public virtual Guid UserFK { get; set; }
    }
}