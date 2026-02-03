using App.Modules.Sys.Application.Models.Identity;
using App.Modules.Sys.Domain.Identity;
using App.Modules.Sys.Shared.ObjectMaps;

namespace App.Modules.Sys.Application.ObjectMaps.Identity
{
    /// <summary>
    /// Maps User entity to UserViewModel.
    /// Pure transformation - no business logic, no DI.
    /// </summary>
    /// <remarks>
    /// Security note:
    /// - Does NOT filter sensitive data here
    /// - Security/masking applied later at API boundary (controller/middleware)
    /// - Mapper stays pure and reusable
    /// 
    /// Mapping approach:
    /// - Convention-based: Properties with same names auto-map
    /// - No custom configuration needed for this simple case
    /// - For complex mappings, override ConfigureMapping() and use CreateMap()
    /// </remarks>
    public class UserToUserViewModelMap : ObjectMapBase<User, UserViewModel>
    {
        // Empty class - pure convention mapping
        // User.Id ? UserViewModel.Id
        // User.Email ? UserViewModel.Email
        // User.IsActive ? UserViewModel.IsActive
        // User.IsSystemAdmin ? UserViewModel.IsSystemAdmin
        // User.CreatedAt ? UserViewModel.CreatedAt
        // User.LastLoginAt ? UserViewModel.LastLoginAt
        
        // For complex mappings, uncomment and use fluent builder:
        // protected override void ConfigureMapping()
        // {
        //     CreateMap()
        //         .Ignore(vm => vm.SomeCalculatedField)
        //         .MapFrom(vm => vm.DisplayName, u => u.Email);
        // }
    }
}


