using System;
using System.Collections.Generic;
using CoreGraphics;
using InstaXamarinMobile;
using InstaXamarinMobile.iOS;
using InstaXamarinMobile.iOS.Custom;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using InstaXamarinMobile.Custom;
using Foundation;
using InstaXamarinMobile.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace InstaXamarinMobile.iOS.Custom
{
    public class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        bool touchOnCustonPinView;
        List<PinoFoto> PinosFotos;
        CustomMap CMAP;
        MKMapView nativeMap;

        //LOAD
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap = Control as MKMapView;

                nativeMap.GetViewForAnnotation = null;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }

            if (e.NewElement != null)
            {
                CMAP = (CustomMap)e.NewElement;
                nativeMap = Control as MKMapView;

                PinosFotos = CMAP.PinosFotos;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            PinosFotos = CMAP.PinosFotos;

            if (PinosFotos == null)
                return;

            foreach (var PF in PinosFotos)
            {
                nativeMap.AddAnnotation(
                    new MapAnnotation(
                        new CoreLocation.CLLocationCoordinate2D(PF.POST.Latitude, PF.POST.Longitude),
                        PF));
            }
        }

        //View do PINO
        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            //Exclui se o ponto for a posição do usuário
            if (annotation is MKUserLocation)
                return null;

            var PinoFoto = ((MapAnnotation)annotation).PinoFoto;


            if (PinoFoto == null)
                throw new Exception("Pino não encontrado");

            MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(PinoFoto.POST.Id.ToString());

            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, PinoFoto.POST.Id.ToString());
                ((CustomMKAnnotationView)annotationView).POST = PinoFoto.POST;

                annotationView.Image = UIImage.FromFile("PinoFoto.png");
                annotationView.CenterOffset = new CGPoint(0, -28.5);
                annotationView.CalloutOffset = new CGPoint(0, 0);
            }
            annotationView.CanShowCallout = false;

            return annotationView;
        }

        //CLICA NO PINO e ABRE A FOTO
        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (customPinView != null)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }

            customPinView = new UIView();
            customPinView.BackgroundColor = UIColor.White;
            customPinView.Frame = new CGRect(0, 0, 130, 160);

            //Nome do usuário
            var TXTNome = new UILabel(new CGRect(5, 5, 120, 20));
            TXTNome.Text = ((MapAnnotation)e.View.Annotation).PinoFoto.POST.UsuarioDados.Nome;
            customPinView.AddSubview(TXTNome);

            //IMG
            var image = new UIImageView(new CGRect(5, 30, 120, 120));
            customPinView.AddSubview(image);

            CustomMKAnnotationView AV = ((CustomMKAnnotationView)e.View);
            if (AV.ImagemCache != null)
                image.Image = UIImage.LoadFromData(AV.ImagemCache);
            else
                ImageFromUrl(image, ((MapAnnotation)e.View.Annotation).PinoFoto.POST.FotoURL, AV);

            customPinView.Center = new CGPoint(25, -(e.View.Frame.Height + 25));

            e.View.AddSubview(customPinView);

            //CLICA NA FOTO
            customPinView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                touchOnCustonPinView = true;
                ((MapAnnotation)e.View.Annotation).PinoFoto.ClicaPino();
            }));
        }

        //Get IMAGEM DE URL ASYNC
        async Task ImageFromUrl(UIImageView IMAGE, string imageUrl, CustomMKAnnotationView AV)
        {
            var httpClient = new HttpClient();
            Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);
            var contents = await contentsTask;
            IMAGE.Image = UIImage.LoadFromData(NSData.FromArray(contents));
            AV.ImagemCache = NSData.FromArray(contents);
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected && !touchOnCustonPinView)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
            else if (touchOnCustonPinView && !e.View.Selected)
            {
                nativeMap.SelectAnnotation(e.View.Annotation, false);
            }

            touchOnCustonPinView = false;
        }
    }


}
