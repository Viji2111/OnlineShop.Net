using DataAccessLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;

namespace PresentationLayer.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public readonly IUserRepository _IuserRepository;
        public AuthorizationFilter(IUserRepository iuserRepository)
        {
            _IuserRepository = iuserRepository;
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {

            var userid1 = context.HttpContext.User.FindFirst(c => c.Type == "ID").Value;
            int userid = int.Parse(userid1.ToString());
            UserCredentials userCred = await _IuserRepository.GetUser(userid);
            if (userCred == null)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
