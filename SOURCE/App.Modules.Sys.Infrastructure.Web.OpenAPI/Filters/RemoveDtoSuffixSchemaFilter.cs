using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.Modules.Sys.Infrastructure.Web.OpenAPI.Filters;

/// <summary>
/// Removes "Dto" suffix from type names in OpenAPI/Swagger documentation (Swashbuckle 10+).
/// Keeps code readable (SmokeTestResultDto) while API shows clean names (SmokeTestResult).
/// </summary>
/// <remarks>
/// FOR SWASHBUCKLE 10.x + MICROSOFT.OPENAPI 2.x
/// 
/// NOTE: Swashbuckle uses Microsoft.OpenApi.Models types - this is by design.
/// Swashbuckle is a generator that produces OpenAPI documents using Microsoft.OpenApi library.
/// 
/// WHY: 
/// - Internal code uses "Dto" for clarity (vs domain entities)
/// - External consumers don't care about internal naming
/// - API documentation should show business names, not technical suffixes
/// 
/// USAGE (Program.cs):
/// <code>
/// builder.Services.AddSwaggerGen(options =>
/// {
///     options.SchemaFilter&lt;RemoveDtoSuffixSchemaFilter&gt;();
///     options.CustomSchemaIds(type => 
///     {
///         var name = type.Name;
///         return name.EndsWith("Dto", StringComparison.OrdinalIgnoreCase) 
///             ? name.Substring(0, name.Length - 3)
///             : name;
///     });
/// });
/// </code>
/// 
/// EXAMPLE:
/// - Code: SmokeTestResultDto → API: SmokeTestResult
/// - Code: UserProfileDto → API: UserProfile
/// </remarks>
public class RemoveDtoSuffixSchemaFilter : ISchemaFilter
{
    private const string DtoSuffix = "Dto";

    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Get the type being documented
        var type = context.Type;
        
        // Only process types ending with "Dto" (case-insensitive for flexibility)
        // Handles: "Dto", "DTO", "dto" (though PascalCase "Dto" is standard)
        if (!type.Name.EndsWith(DtoSuffix, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        // Remove suffix (preserve original length, not hardcoded 3)
        var cleanName = type.Name.Substring(0, type.Name.Length - DtoSuffix.Length);
        
        // Update schema title (displayed in Swagger UI)
        // Note: In Swashbuckle 10.x, OpenApiSchema is the concrete mutable type
        schema.Title = cleanName;
        
        // Optional: Add extension data to show original type name (for debugging)
        // schema.Extensions["x-internal-type"] = new OpenApiString(type.FullName);
    }
}

