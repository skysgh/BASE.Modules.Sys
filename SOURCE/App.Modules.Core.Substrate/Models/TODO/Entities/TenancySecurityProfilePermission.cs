﻿namespace App.Modules.TmpSys.Shared.Models.TODO.Entities
{
    using App.Modules.TmpSys.Substrate.Models.Contracts;
    using App.Modules.TmpSys.Substrate.tmp.Models.Entities.Base;

    /// <summary>
    /// A Permission to assign to a Tenancy Security Profile
    /// <para>
    /// TODO: Improve documentation to explain purpose
    /// </para>
    /// </summary>
    public class TenancySecurityProfilePermission : TenantFKAuditedRecordStatedTimestampedGuidIdEntityBase, IHasTitleAndDescription
    {
        /// <summary>
        /// The Title of the Permission
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Title { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// The Description of the Permission
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

}

