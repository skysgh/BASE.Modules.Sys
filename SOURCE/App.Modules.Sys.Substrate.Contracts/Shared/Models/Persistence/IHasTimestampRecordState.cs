using App.Modules.Sys.Shared.Models;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHasTimestampRecordState :
        IHasTimestamp,
        IHasRecordState
    {

    }


}