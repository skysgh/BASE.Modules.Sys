using App.Modules.Sys.Domain.Domains.Identity;
using App.Modules.Sys.Interfaces.Models.Identity;
using App.Modules.Sys.Shared.ObjectMaps;

namespace App.Modules.Sys.Application.Domains.Identity.Identity
{
    /// <summary>
    /// Maps User entity to UserDto (API contract).
    /// Demonstrates fluent builder with full IntelliSense.
    /// </summary>
    /// <remarks>
    /// This example shows custom mapping configuration:
    /// - Converting DateTime to ISO 8601 string
    /// - Ignoring navigation properties
    /// - Custom field transformations
    /// 
    /// Note: Security/masking still applied at controller level!
    /// </remarks>
    public class UserToUserDtoMap : ObjectMapBase<User, UserDto>
    {
        /// <inheritdoc/>
        protected override void ConfigureMapping()
        {
            CreateMap()
                // Convert DateTime to ISO 8601 string
                .Transform(
                    dest => dest.CreatedAt,
                    src => src.CreatedAt.ToString("o"))
                
                // Convert nullable DateTime to string
                .Transform(
                    dest => dest.LastLoginAt,
                    src => src.LastLoginAt?.ToString("o"));
                
                // TODO: Fix DTO mappings after Email removed from User
                // Email now lives in UserIdentity (provider-specific)
                // IsSystemAdmin replaced with System.Admin permission
                
            // All other properties map by convention
            // User.Id ? UserDto.Id
            // User.IsActive ? UserDto.IsActive
            // User.CreatedAt ? UserDto.CreatedAt
        }
    }
}
