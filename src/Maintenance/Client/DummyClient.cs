using DummyServer;
using System.IO;
using System.Threading.Tasks;

namespace Maintenance.Client
{
    internal class DummyClient
    {
        private static ImageEndpoint _dummyEndpoint = ImageEndpoint.Instance;

        public static async Task<Stream> GetImageStream()
        {
            var response = await _dummyEndpoint.GetHttpResponse();
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
