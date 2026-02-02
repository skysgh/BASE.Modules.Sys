namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHasTimestampRecordStateInRecordAuditability :
        IHasTimestampRecordState,
        IHasInRecordAuditability
    {
    }


}