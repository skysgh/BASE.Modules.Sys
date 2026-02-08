# App.Modules.Sys.Infrastructure.Web.Identity

## Purpose

This assembly isolates all identity/authentication infrastructure to:

1. **Reduce Risk** - Swap identity providers without touching core infrastructure
2. **C&A Compliance** - Clear boundary for security review
3. **Provider Flexibility** - Support multiple identity providers per deployment

## Supported Identity Providers

| Provider | Use Case | FedRAMP | License | Notes |
|----------|----------|---------|---------|-------|
| **Azure AD** | Enterprise SSO | ✅ High | Included | Microsoft Entra ID |
| **Azure AD B2C** | Consumer + Local | ✅ High | Pay-per-MAU | Local accounts + social |
| **Duende IdentityServer** | Self-Hosted | N/A | Commercial | Air-gapped/on-premises |
| **Local Accounts** | Fallback/Offline | N/A | N/A | PBKDF2 password hashing |

## Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                      Client (Angular)                           │
│                 (MSAL / OIDC Client Library)                    │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                    OIDC / OAuth 2.0 Flow
                                │
        ┌───────────┬───────────┼───────────┬───────────┐
        │           │           │           │           │
        ▼           ▼           ▼           ▼           ▼
   Azure AD    Azure AD B2C   Duende    Local     External
  (Enterprise)  (Consumer)    (Self)   Fallback    IdPs
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

### Duende IdentityServer (Self-Hosted)
```json
{
  "DuendeIdentityServer": {
    "Enabled": true,
    "IssuerUri": "https://identity.yourdomain.com",
    "RequireHttpsMetadata": true,
    "SigningCredential": {
      "Type": "KeyVault",
      "KeyVaultUrl": "https://your-vault.vault.azure.net",
      "KeyVaultCertificateName": "identity-signing"
    }
  }
}
```

## Usage

```csharp
// In Program.cs
builder.Services.AddBaseIdentity(builder.Configuration, options =>
{
    options.UseAzureAd = true;              // Enterprise SSO
    options.UseAzureAdB2C = true;           // Consumer + Local
    options.UseDuendeIdentityServer = false; // Self-hosted (optional)
    options.EnableLocalAccounts = true;     // Offline fallback
});
```

## Provider Selection Guide

| Scenario | Recommended Provider |
|----------|---------------------|
| Enterprise customers with Azure AD | Azure AD |
| Consumer-facing with local accounts | Azure AD B2C |
| Government (FedRAMP required) | Azure AD or B2C |
| Air-gapped/on-premises | Duende IdentityServer |
| Offline/fallback | Local Accounts |
| Super admin bootstrap | Local Accounts |
