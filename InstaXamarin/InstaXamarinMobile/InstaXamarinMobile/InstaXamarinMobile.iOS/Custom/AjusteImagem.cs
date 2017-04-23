using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using InstaXamarinMobile.Custom;
using Xamarin.Forms;
using System.Drawing;
using CoreGraphics;

[assembly: Xamarin.Forms.Dependency(typeof(IAjusteImagem))]

namespace InstaXamarinMobile.iOS.Custom
{
    public class AjusteImagem : IAjusteImagem
    {
        public byte[] Cortar(byte[] imagem, int Largura, int Altura)
        {
            byte[] ImagemRetorno = null;
            byte[] ImagemRedimencioada = null;

            int DiferencaLargura = 0;
            int DiferencaAltura = 0;

            //Redimenciona
            using (var originalData = NSData.FromArray(imagem))
            {
                using (var originalImage = UIImage.LoadFromData(originalData))
                {
                    int LarguraOriginalImagem = ((int)originalImage.CGImage.Width);
                    int AlturaOriginalImagem = ((int)originalImage.CGImage.Height);

                    var larguraNova = Largura;
                    var alturaNova = Altura;

                    if (LarguraOriginalImagem > AlturaOriginalImagem)
                    {
                        larguraNova = ((int)((double)Largura * ((double)LarguraOriginalImagem / (double)AlturaOriginalImagem)));
                        DiferencaLargura = larguraNova - Largura;
                    }
                    else if (AlturaOriginalImagem > LarguraOriginalImagem)
                    {
                        alturaNova = ((int)((double)Altura * ((double)AlturaOriginalImagem / (double)LarguraOriginalImagem)));
                        DiferencaAltura = alturaNova - Altura;
                    }

                    ImagemRedimencioada = originalImage.Scale(new SizeF((float)larguraNova, (float)alturaNova)).AsJPEG().ToArray();

                }
            }


            //Corta

            //Descobra o X e Y para o corte // Precisa ser exatamento o centro.
            int X = 0;
            int Y = 0;

            if (DiferencaLargura > 0)
            {
                X = DiferencaLargura / 2;
            }

            if (DiferencaAltura > 0)
            {
                Y = DiferencaAltura / 2;
            }


            using (var data = NSData.FromArray(ImagemRedimencioada))
            {
                using (var photoToCropCGImage = UIImage.LoadFromData(data).CGImage)
                {
                    //crop image
                    using (var photoCroppedCGImage = photoToCropCGImage.WithImageInRect(new CGRect((nfloat)X, (nfloat)Y, (nfloat)Largura, (nfloat)Altura)))
                    {
                        using (var photoCroppedUIImage = UIImage.FromImage(photoCroppedCGImage))
                        {
                            //create a 24bit RGB image to the output size
                            using (var cGBitmapContext = new CGBitmapContext(IntPtr.Zero, (int)Largura, (int)Altura, 8, (int)(4 * Largura), CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
                            {
                                var photoOutputRectangleF = new RectangleF(0f, 0f, (float)Largura, (float)Altura);

                                // draw the cropped photo resized 
                                cGBitmapContext.DrawImage(photoOutputRectangleF, photoCroppedUIImage.CGImage);

                                //get cropped resized photo
                                var photoOutputUIImage = UIKit.UIImage.FromImage(cGBitmapContext.ToImage());

                                //convert cropped resized photo to bytes and then stream
                                using (var photoOutputNsData = photoOutputUIImage.AsJPEG())
                                {
                                    ImagemRetorno = new Byte[photoOutputNsData.Length];
                                    System.Runtime.InteropServices.Marshal.Copy(photoOutputNsData.Bytes, ImagemRetorno, 0, Convert.ToInt32(photoOutputNsData.Length));
                                }
                            }
                        }
                    }
                }
            }

            return ImagemRetorno;
        }
    }
}