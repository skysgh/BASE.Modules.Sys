using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Shared.ObjectMaps;

namespace App.Modules.Sys.Application.ReferenceData.Mapping;

/// <summary>
/// Maps SystemLanguage domain entity to SystemLanguageDto.
/// Convention-based mapping - all properties match by name.
/// </summary>
public class SystemLanguageToSystemLanguageDtoMap : ObjectMapBase<SystemLanguage, SystemLanguageDto>
{
    /// <summary>
    /// Configure mapping rules.
    /// Pure convention-based - all properties map automatically by name.
    /// </summary>
    protected override void ConfigureMapping()
    {
        // All properties match by name - no custom mapping needed
        // Code → Code
        // Name → Name
        // NativeName → NativeName
        // Description → Description
        // IconUrl → IconUrl
        // IsActive → IsActive
        // IsDefault → IsDefault
        // SortOrder → SortOrder
        // Id → Id
    }
}
