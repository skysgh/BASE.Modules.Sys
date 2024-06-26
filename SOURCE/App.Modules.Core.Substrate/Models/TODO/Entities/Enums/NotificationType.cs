﻿namespace App.Modules.TmpSys.Shared.Models.TODO.Entities.Enums
{
    /// <summary>
    /// 
    /// <para>
    /// TODO: Enums are evil (offset issue of Interface Localization)
    /// </para>
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Uninitialised
        /// </summary>
        Undefined,
        /// <summary>
        /// Its just a notification (message from System).
        /// </summary>
        Notification,
        /// <summary>
        /// Message from someone (User)
        /// </summary>
        Message,
        /// <summary>
        /// A Task to be achieved.
        /// </summary>
        Task
    }
}