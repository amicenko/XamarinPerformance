using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Maintenance.Models
{
    public class Image
    {
        private MemoryStream _stream;

        public Image()
        {
        }

        public Image(byte[] data)
        {
            _stream = new MemoryStream(data);
        }

        public Image(Stream data)
        {
            _stream = new MemoryStream();
            data.CopyTo(_stream);
            _stream.Position = 0;
        }

        public string Name { get; set; }

        public byte[] Data
        {
            get
            {
                return _stream.ToArray();
            }

            set
            {
                _stream?.Dispose();
                _stream = new MemoryStream(value);
            }
        }

        [IgnoreDataMember]
        public Func<CancellationToken, Task<Stream>> StreamGetter => (ct) => Task.FromResult((Stream)_stream);

        [IgnoreDataMember]
        public string Length => Data?.Length.ToString();
    }
}
