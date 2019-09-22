using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NewEmployeeService.Facebook
{
    public class FacebookBackChannelHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                //implment FacebookBackChannelHandler ( to address the breaking change introduced by Facebook to its API version 2.4)
                //https://graph.facebook.com/v2.3/me?access_token=ABC
                //https://graph.facebook.com/v2.4/me?fields=id,email&access_token=ABC
                request.RequestUri = new Uri(request.RequestUri.AbsolutePath.Replace("?access_token", "&access_token"));
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}