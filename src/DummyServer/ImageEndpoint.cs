using Common;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DummyServer
{
    public class ImageEndpoint
    {
        public static readonly ImageEndpoint Instance = new ImageEndpoint();

        private ImageEndpoint()
        {
        }

        public async Task<HttpResponseMessage> GetHttpResponse()
        {
            SlowToReadStream slowStream;
            using (var image = await ImageGenerator.GenerateImage())
            {
                slowStream = new SlowToReadStream(((MemoryStream)image).ToArray());
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StreamContent(slowStream)
            };
        }
    }
}
