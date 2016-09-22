
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace CwCodeLib.ImageHelpers
{
    public class Jpeg
    {
        public static void SaveJpeg(Image image, System.IO.Stream stream, int qualityPercent)
        {
            using (EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, qualityPercent))
            {
                using (EncoderParameters eps = new EncoderParameters(1))
                {
                    eps.Param[0] = qualityParam;
                    ImageCodecInfo ici = GetEncoderInfo("image/jpeg");

                    image.Save(stream, ici, eps);
                }

            }

        }

        public static void SaveJpeg(Image image, string filename, int qualityPercent)
        {
            using (EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, qualityPercent))
            {
                using (EncoderParameters eps = new EncoderParameters(1))
                {
                    eps.Param[0] = qualityParam;
                    ImageCodecInfo ici = GetEncoderInfo("image/jpeg");

                    image.Save(filename, ici, eps);
                }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j = 0;
            ImageCodecInfo[] encoders = null;
            encoders = ImageCodecInfo.GetImageEncoders();

            for (j = 0; j <= encoders.Length; j++)
            {
                if (encoders[j].MimeType == mimeType)
                {
                    return encoders[j];
                }
            }
            return null;
        }
    }
}