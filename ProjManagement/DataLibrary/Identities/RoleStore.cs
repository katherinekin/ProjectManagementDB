using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary.Identities
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity role store iterfaces
    /// </summary>
    public class RoleStore<TRole>
    where TRole : IdentityRole
    {
        public IQueryable<TRole> Roles
        {
            get { throw new NotImplementedException(); }
        }
    }
}