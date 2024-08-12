using Microsoft.AspNetCore.Http;

namespace Papara.CaptainStore.Application;
public interface ISessionContext
{
    public HttpContext HttpContext { get; set; }
    public Session Session { get; set; }
}

