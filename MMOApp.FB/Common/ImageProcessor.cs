using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using KSS.Patterns.Extensions;

namespace KSS.Patterns.ImageProcessing
{
    public static class ImageProcessor
    {
        public static Image ToImage(this Bitmap bitmap)
        {
            return bitmap != null ? bitmap.Clone() as Image : null;
        }

        public static Bitmap ToBitmap(this Image image)
        {
            return image != null ? new Bitmap(image) : null;
        }

        public static bool Compare(Bitmap firstImage, Bitmap secondImage)
        {
            MemoryStream ms = new MemoryStream();
            firstImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String firstBitmap = Convert.ToBase64String(ms.ToArray());
            ms.Position = 0;

            secondImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String secondBitmap = Convert.ToBase64String(ms.ToArray());

            if (firstBitmap.Equals(secondBitmap))
            {
                return true;
            }

            return false;
        }

        public static byte[] ToByte(this Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));

            //MemoryStream ms = new MemoryStream();
            //image.Save(ms, ImageFormat.Jpeg);
            //return ms.ToArray();
        }

        public static byte[] GetRawBytes(this Bitmap image, PixelFormat format, int bytesPerPixel)
        {
            byte[] data = new byte[image.Width * image.Height * bytesPerPixel];

            var bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, format);
            Marshal.Copy(bitmapData.Scan0, data, 0, data.Length);
            image.UnlockBits(bitmapData);

            return data;
        }

        public static void SetRawBytes(this Bitmap image, byte[] data)
        {
            var bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, image.PixelFormat);

            Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);

            image.UnlockBits(bitmapData);
        }

        public static Bitmap FromRawBytes(byte[] data, int width, int height, PixelFormat format, int bytesPerPixel)
        {
            Bitmap bitmap = new Bitmap(width, height, format);
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, format);
            Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        public static Image ToImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static Image Crop(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
                                            bmpImage.PixelFormat);
            return bmpCrop;
        }

        /// <summary>
        /// Resize image to new size, prefer small ratio
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            double nPercent = 0;
            double nPercentW = 0;
            double nPercentH = 0;

            nPercentW = ((double)size.Width / (double)sourceWidth);
            nPercentH = ((double)size.Height / (double)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static void SaveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
                new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public static Image Clear(Image img, Rectangle area, Color color)
        {
            Bitmap bmp = new Bitmap(img);
            for (int x = area.Left; x < area.Left + area.Width; x++)
            {
                for (int y = area.Top; y < area.Top + area.Height; y++)
                {
                    bmp.SetPixel(x, y, color);
                }
            }

            return bmp;
        }

        private static void SetIndexedPixel(int x, int y, BitmapData bmd, bool pixel)
        {
            int index = y * bmd.Stride + (x >> 3);
            byte p = Marshal.ReadByte(bmd.Scan0, index);
            byte mask = (byte)(0x80 >> (x & 0x7));
            if (pixel)
                p |= mask;
            else
                p &= (byte)(mask ^ 0xff);
            Marshal.WriteByte(bmd.Scan0, index, p);
        }

        public static Bitmap MakeBlackWhite(Bitmap originalBitmap)
        {
            //Ensure that it's a 32 bit per pixel file
            if (originalBitmap.PixelFormat != PixelFormat.Format32bppPArgb)
            {
                Bitmap temp = new Bitmap(originalBitmap.Width, originalBitmap.Height, PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(temp);
                g.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height), 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel);
                originalBitmap.Dispose();
                g.Dispose();
                originalBitmap = temp;
            }

            //lock the bits of the original bitmap
            BitmapData bmdo = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height), ImageLockMode.ReadOnly, originalBitmap.PixelFormat);

            //and the new 1bpp bitmap
            Bitmap bitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, PixelFormat.Format1bppIndexed);
            BitmapData bmdn = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);

            //scan through the pixels Y by X
            int x, y;
            for (y = 0; y < originalBitmap.Height; y++)
            {
                for (x = 0; x < originalBitmap.Width; x++)
                {
                    //generate the address of the colour pixel
                    int index = y * bmdo.Stride + (x * 4);
                    //check its brightness
                    if (Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2),
                                    Marshal.ReadByte(bmdo.Scan0, index + 1),
                                    Marshal.ReadByte(bmdo.Scan0, index)).GetBrightness() > 0.5f)
                    {
                        SetIndexedPixel(x, y, bmdn, true); //set it if its bright.
                    }
                }
            }

            //tidy up
            bitmap.UnlockBits(bmdn);
            originalBitmap.UnlockBits(bmdo);

            return bitmap;
        }

        public static Bitmap MakeBlackWhite2(Bitmap original)
        {
            Bitmap output = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {

                    Color c = original.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < 200)
                        output.SetPixel(i, j, Color.Black);

                    else
                        output.SetPixel(i, j, Color.White);

                }
            }

            return output;
        }

        public static double CompareSimilar(Image image1, Image image2)
        {
            if (image1 == null)
                throw new ArgumentNullException("image1");

            if (image2 == null)
                throw new ArgumentNullException("image2");

            Bitmap bitmap1 = ImageUtility.ResizeBitmap(image1.ToBitmap(), 100, 100);
            Bitmap bitmap2 = ImageUtility.ResizeBitmap(image2.ToBitmap(), 100, 100);

            RgbProjections projections1 = new RgbProjections(ImageUtility.GetRgbProjections(bitmap1));
            RgbProjections projections2 = new RgbProjections(ImageUtility.GetRgbProjections(bitmap2));

            return projections1.CalculateSimilarity(projections2) * 100;
        }

        public static Bitmap ReplaceColor(Bitmap bitmap, Color orgColor, Color repColor)
        {
            Bitmap bmp = new Bitmap(bitmap);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    if (color == orgColor)
                        bmp.SetPixel(x, y, repColor);
                }
            }

            return bmp;
        }

        public static Bitmap ClearColorRange(Bitmap bitmap, int min, int max, Color rplColor)
        {
            Bitmap bmp = new Bitmap(bitmap);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    int average = ((color.R + color.B + color.G) / 3);
                    if (min <= average && average <= max)
                        bmp.SetPixel(x, y, rplColor);
                }
            }

            return bmp;
        }

        public static Bitmap SubtractColorInstead(Bitmap bitmap, Color keepColor, Color replacedColor)
        {
            Bitmap bmp = new Bitmap(bitmap);

            List<Color> tColors = new List<Color>();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    if (!tColors.Contains(color))
                    {
                        tColors.Add(color);
                    }

                    if (color != keepColor)
                    {
                        bmp.SetPixel(x, y, replacedColor);
                    }
                    else
                    {

                    }
                }
            }

            return bmp;
        }

        /// <summary>
        /// Draw an image on another
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="another"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Bitmap DrawImage(Bitmap bitmap, Bitmap another, Rectangle location)
        {
            Bitmap clone = another.Clone() as Bitmap;

            using (var g = Graphics.FromImage(clone))
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.DrawImage(bitmap, location);
            }

            return clone;
        }

        #region Remove Noise

        public static void RemoveNoise(Bitmap bitmap)
        {
            RemoveNoise(bitmap, Color.White);
        }

        public static Bitmap RemoveNoise(Bitmap bitmap, Color backgroundColor)
        {
            bitmap = new Bitmap(bitmap);

            List<Point> colorPoints = GetColorPoints(bitmap, backgroundColor);

            do
            {
                var hasNoiseRemoved = DoRemoveNoise(bitmap, colorPoints);
                if (!hasNoiseRemoved)
                    break;

                hasNoiseRemoved = RemoveJuniorPoints(bitmap, colorPoints);
                if (!hasNoiseRemoved)
                    break;
            } while (true);

            return bitmap;
        }

        private static List<Point> GetColorPoints(Bitmap bitmap, Color backgroundColor)
        {
            List<Point> colorPoints = new List<Point>();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    if (bitmap.GetPixel(x, y).ToArgb() != backgroundColor.ToArgb())
                        colorPoints.Add(new Point(x, y));
                }
            }

            return colorPoints;
        }

        private static bool DoRemoveNoise(Bitmap bitmap, List<Point> blackPoints, int whiteCount = 3, int amount = 0)
        {
            int removedElments = 0;
            bool hasElementRemoved = false;
            bool changed;
            do
            {
                changed = false;
                List<Point> removedPoints = new List<Point>();
                foreach (var blackPoint in blackPoints)
                {
                    if (IsNoise(bitmap, blackPoint.X, blackPoint.Y, 1, whiteCount))
                    {
                        bitmap.SetPixel(blackPoint.X, blackPoint.Y, Color.White);
                        removedPoints.Add(blackPoint);
                        changed = true;
                        hasElementRemoved = true;
                        removedElments++;
                        if (amount > 0 && removedElments >= amount)
                            return true;
                    }
                }

                blackPoints.RemoveAll(removedPoints.Contains);
            } while (changed);

            return hasElementRemoved;
        }

        private static bool RemoveJuniorPoints(Bitmap image, List<Point> blackPoints)
        {
            bool changed = false;
            List<Point> removedPoints = new List<Point>();
            var points = blackPoints.Where(p => IsNoise(image, p.X, p.Y, 1, 2)).ToList();
            foreach (var point in points)
            {
                var neighbourPoints = GetNeighbourBlackPoint(blackPoints, point);
                if (neighbourPoints.All(p => IsNoise(image, p.X, p.Y, 1, 2)))
                {
                    image.SetPixel(point.X, point.Y, Color.White);
                    removedPoints.Add(point);
                    changed = true;
                }
            }

            blackPoints.RemoveAll(removedPoints.Contains);
            return changed;
        }

        private static bool IsNoise(Bitmap image, int x, int y, int size = 1, int whiteCount = 3)
        {
            int white = 0;

            if (x == 0 || image.GetPixel(x - 1, y).ToArgb() == Color.White.ToArgb())
                white++;

            if (x == image.Width - size || image.GetPixel(x + size, y).ToArgb() == Color.White.ToArgb())
                white++;

            if (y == 0 || image.GetPixel(x, y - 1).ToArgb() == Color.White.ToArgb())
                white++;

            if (y == image.Height - size || image.GetPixel(x, y + size).ToArgb() == Color.White.ToArgb())
                white++;

            return white >= whiteCount;
        }

        private static IEnumerable<Point> GetNeighbourBlackPoint(IEnumerable<Point> blackPoints, Point point)
        {
            return blackPoints.Where(p => p != point && p.X.InRange(point.X - 1, point.X + 1) && p.Y.InRange(point.Y - 1, point.Y + 1)).ToList();
        }
        
        #endregion
    }
}