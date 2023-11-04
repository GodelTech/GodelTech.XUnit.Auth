using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace GodelTech.XUnit.Auth;

/// <summary>
/// JSON Web Token HTTP message handler.
/// </summary>
public class TestJwtTokenHttpMessageHandler : HttpMessageHandler
{
    /// <summary>
    /// Send.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>HttpResponseMessage.</returns>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var content = new StringContent("{}", Encoding.UTF8, "application/json");

        return Task.FromResult(
            new HttpResponseMessage
            {
                Content = content
            }
        );
    }
}
