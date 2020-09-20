using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Models.Repository;

namespace Models
{
    [MetadataType(typeof(UsersMeta))]
    public partial class Users : IUser<string>
    {
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public bool IsSelectedUserInDocuments { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [NotMapped]
        private string _userRole;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [NotMapped]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public string FullName => $"{FirstName} {LastName}";


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [NotMapped]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public string UserRole
        {
            get
            {
                if (_userRole == null)
                    _userRole = string.Join(",", UserRoles.Select(m => m.Name));
                return _userRole;
            }

            set => _userRole = value;
        }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public string Password { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Users, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("FullName", this.FullName));
            userIdentity.AddClaim(new Claim("Email", this.Email));
            userIdentity.AddClaim(new Claim("UserRoles", this.UserRole));
            return userIdentity;
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Users, string> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FullName", this.FullName));
            userIdentity.AddClaim(new Claim("Email", this.Email));
            // Add custom user claims here
            return userIdentity;
        }

    }


    public class UsersMeta
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}