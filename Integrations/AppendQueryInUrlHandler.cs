
using System.Web;

namespace Integrations
{
    public class AppendQueryInUrlHandler(ILogger<AppendQueryInUrlHandler>logger) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri is not  null)
            {
                string url = request.RequestUri.ToString ();
                var uriBuilder = new UriBuilder (url);
                var query =HttpUtility.ParseQueryString (uriBuilder.Query);
                query["myQuery"] = "nitish";
                uriBuilder.Query=query.ToString ();
                url = uriBuilder.ToString ();

                logger.LogInformation(url);
                request.RequestUri = new Uri (url);
            }
            //request.Headers.Add()

            return base.SendAsync(request, cancellationToken);
        }
    }
}
