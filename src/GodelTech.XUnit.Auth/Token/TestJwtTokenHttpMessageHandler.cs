using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.XUnit.Auth.Token;

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
