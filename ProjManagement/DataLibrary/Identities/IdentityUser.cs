namespace DataLibrary.Identities
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    /// <summary>
    /// Class that implements the ASP.NET Identity
    /// IUser interface
    /// </summary>
    public class IdentityUser : IdentityUser<string,
                                IdentityUserLogin,
                                IdentityUserRole,
                                IdentityUserClaim>, IUser
    {
        public int Employee_ID { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}