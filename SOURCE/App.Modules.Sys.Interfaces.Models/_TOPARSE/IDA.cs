// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
using System.Security.Claims;

namespace App.Modules.Sys.Substrate.Models.Messages
{

    /// <summary>
    /// TODO: Describe
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="userId"></param>
    /// <param name="identity"></param>
    public class AuthenticationSuccessMessage(string userId, ClaimsIdentity identity)
    {
        /// <summary>
        /// TODO: Describe
        /// </summary>
        public string UserId { get; set; } = userId;

        /// <summary>
        /// The Identity created from the claims, but not
        /// yet set on the thread.
        /// </summary>
        public ClaimsIdentity Identity { get; set; } = identity;
    }


    /// <summary>
    /// TODO: Describe
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="name"></param>
    /// <param name="signedInUserNameIdentifier"></param>
    public class AuthorizationCodeReceivedMessage(string name, string signedInUserNameIdentifier)
    {

        /// <summary>
        /// TODO: Describe
        /// </summary>
        public string? Name { get; set; } = name;


        /// <summary>
        /// The NameIdentifier of the Identity built from the returned IdP Credentials
        /// But not yet turned into an Thread Identity (certainly not yet turned into
        /// a BearerToken or older style cookie.
        /// </summary>
        public string? SignedInUserNameIdentifier { get; set; } = signedInUserNameIdentifier;
    }

    /// <summary>
    /// TODO: Describe
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="error"></param>
    /// <param name="errorDescription"></param>
    /// <param name="errorUri"></param>
    public class AuthenticationErrorMessage(string error, string errorDescription, string errorUri)
    {

        /// <summary>
        /// TODO: Describe
        /// </summary>
        public string Error { get; set; } = error;
        /// <summary>
        /// TODO: Describe
        /// </summary>
        public string ErrorDescription { get; set; } = errorDescription;
        /// <summary>
        /// TODO: Describe
        /// </summary>
        public string ErrorUri { get; set; } = errorUri;
    }

}
