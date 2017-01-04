using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Web.Core
{
    public class ImageHelper
    {
        public static Image GetImageFromBytes(byte[] bytes)
        {

            Image image;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
        public static byte[] ConvertImageToByteArray(System.Drawing.Image image,
                                    string format="jpeg")
        {

            var imageFormat = ImageFormat.Jpeg;
            if (format == "gif") imageFormat = ImageFormat.Gif;
            else if (format == "png") imageFormat = ImageFormat.Png
 ;
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, imageFormat);
                bytes = ms.ToArray();
            }

            return bytes;
        }
        public static byte[] GetByteFromImageFile(FileInfo fileInfo, string format="")
        {
            var image = Image.FromFile(fileInfo.FullName);

            return ConvertImageToByteArray(image, format);
            
        }
        public static Image GetThumbnail(Image sourceImage, int width, int height)
        {

            return sourceImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }
      

        public static Image GetFixedSizeThumbnail(Image image, int width, int height)
        {
            Size thumbnailSize = new Size(width, height);
            float scalingRatio = CalculateScalingRatio(image.Size, thumbnailSize);

            int scaledWidth = (int)Math.Round((float)image.Size.Width * scalingRatio);
            int scaledHeight = (int)Math.Round((float)image.Size.Height * scalingRatio);
            int scaledLeft = (thumbnailSize.Width - scaledWidth) / 2;
            int scaledTop = (thumbnailSize.Height - scaledHeight) / 2;

            // For portrait mode, adjust the vertical top of the crop area so that we get more of the top area
            if (scaledWidth < scaledHeight && scaledHeight > thumbnailSize.Height)
            {
                scaledTop = (thumbnailSize.Height - scaledHeight) / 4;
            }

            var cropArea = new Rectangle(scaledLeft, scaledTop, scaledWidth, scaledHeight);

            Image thumbnail = new Bitmap(thumbnailSize.Width, thumbnailSize.Height);
            using (Graphics thumbnailGraphics = Graphics.FromImage(thumbnail))
            {
                thumbnailGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbnailGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                thumbnailGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbnailGraphics.DrawImage(image, cropArea);
            }
            return thumbnail;
        }

        static float CalculateScalingRatio(Size originalSize, Size targetSize)
        {
            float originalAspectRatio = (float)originalSize.Width / (float)originalSize.Height;
            float targetAspectRatio = (float)targetSize.Width / (float)targetSize.Height;

            float scalingRatio = 0;

            if (targetAspectRatio >= originalAspectRatio)
            {
                scalingRatio = (float)targetSize.Width / (float)originalSize.Width;
            }
            else
            {
                scalingRatio = (float)targetSize.Height / (float)originalSize.Height;
            }

            return scalingRatio;
        }
    }
}
