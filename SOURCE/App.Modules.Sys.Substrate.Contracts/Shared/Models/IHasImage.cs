using App.Modules.Sys.Shared.Models.Enums;

namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Contract for entities that have an image as a font or image. 
    /// </summary>
    public interface IHasImage : IHasImageUrlNullable, IHasImageStyleNullable
    {

        
    }


}
