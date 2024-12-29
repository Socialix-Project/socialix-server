using Microsoft.AspNetCore.Mvc;

namespace Socialix.Common.API
{
    /// <summary>
    /// ApiBaseController
    /// </summary>
    /// <typeparam name="TRquest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class ApiBaseController<TRquest, TResponse> : ControllerBase
    {
        public abstract TResponse ErrorCheck(TRquest request, TResponse response);
        public abstract TResponse Exec(TRquest request, TResponse response);
    }
}
