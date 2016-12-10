using System.Net;

namespace aspnetevilfilter.Models
{
    enum EvilHttpStatusCode
    {
        InternalServerError = HttpStatusCode.InternalServerError,
        Teapot = 418,
        Fox = 419,
    }
}
