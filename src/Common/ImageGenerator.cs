using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common
{
    public static class ImageGenerator
    {
        private static readonly Random _random = new Random();
        private static readonly Stream _segoeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(ImageGenerator), "SEGUIHIS.TTF");
        private static readonly FontCollection _fonts = new FontCollection();

        private static volatile int _imageNumber = 0;

        static ImageGenerator()
        {
            _fonts.Add(_segoeStream);
            _segoeStream.Dispose();
        }

        public static async Task<Stream> GenerateImage(int width = 2500, int height = 1800)
        {
            byte[] noise = new byte[width * height * 4];
            _random.NextBytes(noise);
            using (Image image = Image.LoadPixelData<Rgb24>(noise, width, height))
            {
                image.Metadata.HorizontalResolution = 96;
                image.Metadata.VerticalResolution = 96;
                var font = new Font(_fonts.Families.First(), 48f);
                var text = (++_imageNumber).ToString();
                var box = TextMeasurer.MeasureBounds(text, new TextOptions(font));
                image.Mutate(ctx =>
                {
                    ctx.Draw(Brushes.Solid(Color.White), 1f, new RectangleF(0, 0, width, box.Height));
                    ctx.DrawText(text, font, Brushes.Solid(Color.DarkGoldenrod), new PointF(0, 60));
                });

                var jpegStream = new MemoryStream();
                await image.SaveAsJpegAsync(jpegStream);
                jpegStream.Position = 0;
                return jpegStream;
            }
        }
    }
}
