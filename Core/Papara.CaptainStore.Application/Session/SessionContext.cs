using Microsoft.AspNetCore.Http;

namespace Papara.CaptainStore.Application;

public class SessionContext : ISessionContext
{
    public HttpContext HttpContext { get; set; }
    public Session Session { get; set; }
}