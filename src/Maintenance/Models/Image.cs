using System.IO;
using System.Runtime.Serialization;

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
                var data = _stream.ToArray();
                _stream.Position = 0;
                return data;
            }

            set
            {
                _stream?.Dispose();
                _stream = new MemoryStream(value);
            }
        }

        [IgnoreDataMember]
        public Stream Stream => _stream;
    }
}
