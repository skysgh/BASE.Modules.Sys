# App.Modules.Sys.Infrastructure.Web.Identity

## Purpose

This assembly isolates all identity/authentication infrastructure to:

1. **Reduce Risk** - Swap identity providers without touching core infrastructure
2. **C&A Compliance** - Clear boundary for security review
3. **Provider Flexibility** - Support multiple identity providers per deployment

## Supported Identity Providers

| Provider | Use Case | FedRAMP | Notes |
|----------|----------|---------|-------|
| **Azure AD** | Enterprise SSO | ✅ High | Microsoft Entra ID |
| **Azure AD B2C** | Consumer + Local | ✅ High | Local accounts + social |
| **Local Accounts** | Fallback/Offline | N/A | PBKDF2 password hashing |

## Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                      Client (Angular)                           │
│                 (MSAL / OIDC Client Library)                    │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                    OIDC / OAuth 2.0 Flow
                                │
        ┌───────────────────────┼───────────────────────┐
        │                       │                       │
        ▼                       ▼                       ▼
   Azure AD              Azure AD B2C            Local Fallback
   (Enterprise)          (Consumer)              (Offline/Admin)
```

## Configuration

### Azure AD (Enterprise)
```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "CallbackPath": "/signin-oidc"
  }
}
```

### Azure AD B2C (Consumer + Local)
```json
{
  "AzureAdB2C": {
    "Instance": "https://your-tenant.b2clogin.com",
    "Domain": "your-tenant.onmicrosoft.com",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "SignUpSignInPolicyId": "B2C_1_SignUpSignIn",
    "CallbackPath": "/signin-oidc"
  }
}
```

## Usage

```csharp
// In Program.cs
builder.Services.AddBaseIdentity(builder.Configuration, options =>
{
    options.UseAzureAd = true;           // Enterprise SSO
    options.UseAzureAdB2C = true;        // Consumer + Local
    options.EnableLocalAccounts = true;  // Offline fallback
});
```

## Files

- `Providers/` - Identity provider implementations
- `Configuration/` - Configuration models and extensions
- `Services/` - Identity services (password hashing, token management)
