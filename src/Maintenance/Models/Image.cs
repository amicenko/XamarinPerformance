using System.IO;

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

        public int Width { get; set; }

        public int Height { get; set; }

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
    }
}
