using Microsoft.AspNetCore.Http;
using Papara.CaptainStore.Application;

namespace Papara.CaptainStore.Persistence.Registrations
{
    public static class SessionContextFactory
    {
        public static ISessionContext CreateSessionContext(IHttpContextAccessor contextAccessor)
        {
            var context = contextAccessor.HttpContext;
            var sessionContext = new SessionContext();
            sessionContext.Session = JwtManager.GetSession(context);
            sessionContext.HttpContext = context;
            return sessionContext;
        }
    }
}
