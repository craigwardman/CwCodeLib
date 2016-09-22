using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace CwCodeLib.ImageHelpers
{
    /// <summary>
    /// Class containing a shared function to generate a thumbnail of an image
    /// </summary>
    /// <remarks></remarks>
    public class Thumbnail
    {

        /// <summary>
        /// Generates a thumbnail of the given image using targetsize as the largest dimension
        /// </summary>
        /// <param name="OriginalImage">The original Image object</param>
        /// <param name="TargetSize">Size of the biggest target dimension</param>
        /// <returns>The thumbnail image</returns>
        /// <remarks></remarks>
        public static Image GetThumbnail(Image OriginalImage, int TargetSize)
        {
            int imgWidth = OriginalImage.Width;
            int imgHeight = OriginalImage.Height;

            double ratio = 0;

            //adjust the target size as necessary
            if (imgWidth > TargetSize || imgHeight > TargetSize)
            {
                if (imgWidth > imgHeight)
                {
                    ratio = TargetSize / imgWidth;
                    imgWidth = TargetSize;
                    imgHeight = Convert.ToInt32(imgHeight * ratio);
                }
                else if (imgHeight > imgWidth)
                {
                    ratio = TargetSize / imgHeight;
                    imgHeight = TargetSize;
                    imgWidth = Convert.ToInt32(imgWidth * ratio);
                }
                else
                {
                    ratio = 1;
                    imgHeight = TargetSize;
                    imgWidth = TargetSize;
                }
            }

            //get whole numbers
            imgWidth = Convert.ToInt32(imgWidth);
            imgHeight = Convert.ToInt32(imgHeight);

            if (imgWidth == 0)
                imgWidth = 1;
            if (imgHeight == 0)
                imgHeight = 1;

            return ResizeImage(OriginalImage, imgWidth, imgHeight);
        }

        /// <summary>
        /// Generates a thumbnail of an image to fit a bounding box, specified by max height and max width, whilst maintaining aspect ratio
        /// </summary>
        /// <param name="OriginalImage">he original Image object</param>
        /// <param name="maxWidth">Maximum width the image can be</param>
        /// <param name="maxHeight">Maximum height the image can be</param>
        /// <returns>A thumbnail image fitting the max sizes</returns>
        /// <remarks></remarks>
        public static Image GetThumbnail(Image OriginalImage, int maxWidth, int maxHeight)
        {
            if (maxHeight <= 0)
                maxHeight = OriginalImage.Height;
            if (maxWidth <= 0)
                maxWidth = OriginalImage.Width;

            double ww = 0;
            double wh = 0;
            double ph = 0;
            double pw = 0;
            double a1 = 0;
            double targetWidth = 0;
            double targetHeight = 0;
            ///''''
            //     '
            ///''''
            if (OriginalImage.Width > OriginalImage.Height)
            {
                ww = maxWidth;
                wh = maxHeight;
                ph = OriginalImage.Height;
                pw = OriginalImage.Width;
                a1 = pw / ww;
                targetWidth = pw / a1;
                targetHeight = ph / a1;
                //just in case pic is still taller than available space
                if (targetHeight > wh)
                {
                    targetHeight = wh;
                    targetWidth = targetHeight / OriginalImage.Height * OriginalImage.Width;
                }

                // '
                // '
                // '
            }
            else if (OriginalImage.Height > OriginalImage.Width)
            {
                //picture is taller than wide
                wh = maxHeight;
                ww = maxWidth;
                pw = OriginalImage.Width;
                ph = OriginalImage.Height;
                a1 = ph / wh;
                targetWidth = pw / a1;
                targetHeight = ph / a1;
                //just in case pic is still wider than available space
                if (targetWidth > ww)
                {
                    targetWidth = ww;
                    targetHeight = targetWidth / OriginalImage.Width * OriginalImage.Height;
                }

                //'
                //'
            }
            else
            {
                //incase of perfect square
                //fill the whole space
                // ERROR: Not supported in C#: OnErrorStatement

                targetHeight = maxHeight;
                targetWidth = maxWidth;
                //make square again
                //if available space is wider than tall
                if (targetWidth > targetHeight)
                {
                    targetWidth = targetHeight;
                    //if available space is taller than wide
                }
                else if (targetHeight > targetWidth)
                {
                    targetHeight = targetWidth;
                }
                //the available space was already square, skip to here
            }

            return ResizeImage(OriginalImage, Convert.ToInt32(targetWidth), Convert.ToInt32(targetHeight));
        }

        private static Image ResizeImage(Image originalImage, int width, int height)
        {
            //for speed, treat everything as a 32BPP ARGB JPEG for outputting

            Image returnBmp = null;
            Rectangle thumbSizing = new Rectangle(0, 0, width, height);

            //use a generate exception handler, just in case

            try
            {
                //create a bitmap in memory and get a handle to its Graphics object
                returnBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (Graphics g = Graphics.FromImage(returnBmp))
                {
                    //now copy the srcImage to the destination bitmap at required size
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    g.FillRectangle(System.Drawing.Brushes.White, 0, 0, thumbSizing.Width, thumbSizing.Height);
                    if (originalImage.Height >= height | originalImage.Width >= width)
                    {
                        g.DrawImage(originalImage, thumbSizing);
                    }
                    else
                    {
                        //original is smaller than target, put it in the middle
                        int x = 0;
                        int y = 0;
                        x = Convert.ToInt32((width / 2) - originalImage.Width / 2);
                        y = Convert.ToInt32((height / 2) - originalImage.Height / 2);
                        g.DrawImage(originalImage, x, y, originalImage.Width, originalImage.Height);
                    }
                }

            }
            catch (Exception ex)
            {
                //free up resources
                if (returnBmp != null) { returnBmp.Dispose(); returnBmp = null; }

                //just create a JPEG with the words 'unavailable'
                returnBmp = new Bitmap(200, 15, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (Graphics g = Graphics.FromImage(returnBmp))
                {
                    g.DrawString("Unavailable", new Font("Verdana", 8), Brushes.White, 0, 0);
                }
            }

            return returnBmp;
        }
    }
}