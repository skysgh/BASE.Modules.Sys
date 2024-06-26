﻿using App.Modules.Base.Shared.Models.TODO.Entities.Enums;

namespace App.Modules.TmpSys.Application.APIs.TODO.Messages.V0100
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityProfileSecurityProfilePermissionAssignmentDto
    {
        private SecurityProfilePermissionDto? permission = null;
        private AssignmentType assignmentType = AssignmentType.Undefined;

        /// <summary>
        /// The Security Profile.
        /// </summary>
        public SecurityProfilePermissionDto Permission { get => permission ?? new SecurityProfilePermissionDto(); set => permission = value; }

        /// <summary>
        /// The <see cref="AssignmentType"/>
        /// </summary>
        public AssignmentType AssignmentType { get => assignmentType; set => assignmentType = value; }
    }

}
