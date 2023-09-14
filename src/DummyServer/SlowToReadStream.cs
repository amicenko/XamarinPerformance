using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DummyServer
{
    internal class SlowToReadStream : MemoryStream
    {
        private static volatile int _numConcurrent = 0;

        public override bool CanSeek => false;
        public override bool CanWrite => false;

        public SlowToReadStream(byte[] content) : base(content)
        {
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref _numConcurrent);

            // Simulate 2 megabits/second (250 bytes/millisec)
            var totalBytes = 0;
            while (totalBytes < count)
            {
                await Task.Delay(1 * _numConcurrent);
                var numBytesToRead = Math.Min(250, count - totalBytes);
                var bytesRead = await base.ReadAsync(buffer, totalBytes + offset, numBytesToRead, cancellationToken);
                if (bytesRead == 0)
                {
                    break;
                }

                totalBytes += bytesRead;
            }

            Interlocked.Decrement(ref _numConcurrent);
            return totalBytes;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Simulate 2 megabits/second (250 bytes/millisec)
            var totalBytes = 0;
            while (totalBytes < count)
            {
                Thread.Sleep(1);
                var numBytesToRead = Math.Min(250, count - totalBytes);
                var bytesRead = base.Read(buffer, totalBytes + offset, numBytesToRead);
                if (bytesRead == 0)
                {
                    break;
                }

                totalBytes += bytesRead;
            }

            return totalBytes;
        }
    }
}
