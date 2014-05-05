using System;
using System.Net;
using System.Threading.Tasks;

namespace MvxAutoComplete.TestApp.Core
{
    public static class Extensions
    {
        internal static Task<WebResponse> GetResponseAsync(this WebRequest request, TimeSpan timeout)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
            {
                var t = Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse,
                    request.EndGetResponse,
                    null);

                if (!t.Wait(timeout)) throw new TimeoutException();

                return t.Result;
            });
        }
    }
}
