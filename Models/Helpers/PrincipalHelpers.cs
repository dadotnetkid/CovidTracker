using System.Linq;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Models.Repository;

namespace Helpers
{
    public static class PrincipalHelpers
    {

        public static bool IsInRoles(this IPrincipal principal, params string[] roles)
        {
            Models.Repository.UnitOfWork unitOfWork = new Models.Repository.UnitOfWork();
            var userId = principal.Identity.GetUserId();
            var user = unitOfWork.UsersRepo.Find(m => m.Id == userId);
            if (principal == null)
                return false;
            if (user == null)
                return false;
            if (string.IsNullOrEmpty(userId))
                return false;
            return user.UserRoles.Any(m => roles.Contains(m.Name));
        }

        public static bool UserIsInRoles(string userId, params string[] roles)
        {
            var unitOfWork = new UnitOfWork();

            var user = unitOfWork.UsersRepo.Find(m => m.Id == userId);

            if (user == null)
                return false;
            if (string.IsNullOrEmpty(userId))
                return false;
            return user.UserRoles.Any(m => roles.Contains(m.Name));
        }
        public static bool UserInAction(this IPrincipal principal, string action)
        {
            var unitOfWork = new UnitOfWork();
            var userId = principal.Identity.GetUserId();
            var x = unitOfWork.UsersRepo.Find(m => m.Id == userId);
            if (!unitOfWork.FunctionsRepo.Fetch(m => m.Action == action).Any())
            {
                unitOfWork.FunctionsRepo.Insert(new Models.Functions() { Action = action });
                unitOfWork.Save();
            }
            var role = unitOfWork.UserRolesRepo.Find(m => m.Name == "Administrator");
            if (role != null)
                if (role.Functions.All(m => m.Action != action))
                {
                    role.Functions.Add(new Models.Functions() { Action = action });
                    unitOfWork.Save();
                }
                    
            if (x.UserRoles.Any(c => c.Functions.Any(n => n.Action == action)))
                return true;

            return false;
        }
    }
}
