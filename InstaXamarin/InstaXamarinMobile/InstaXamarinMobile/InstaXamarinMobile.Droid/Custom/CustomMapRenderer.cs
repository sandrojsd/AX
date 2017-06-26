using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using InstaXamarinMobile.Custom;
using InstaXamarinMobile.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Android.Graphics;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace InstaXamarinMobile.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
    {
        CustomMap CMAP;
        GoogleMap map;
        List<PinoFoto> PinosFotos;
        bool isDrawn;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                map.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                CMAP = (CustomMap)e.NewElement;
                ((MapView)Control).GetMapAsync(this);
            }
        }

        PinoFoto GETPinoFoto(Marker annotation)
        {
            foreach (var pin in PinosFotos)
                if (pin.POST.Id.ToString() == annotation.Title)
                    return pin;
            return null;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;

			//map.Clear();


            map.InfoWindowClick += OnInfoWindowClick;
            map.SetInfoWindowAdapter(this);





            InvokeOnMapReadyBaseClassHack(googleMap);
        }

		private void InvokeOnMapReadyBaseClassHack(GoogleMap googleMap)
		{
			System.Reflection.MethodInfo onMapReadyMethodInfo = null;

			Type baseType = typeof(MapRenderer);
			foreach (var currentMethod in baseType.GetMethods(System.Reflection.BindingFlags.NonPublic |
																System.Reflection.BindingFlags.Instance |
																System.Reflection.BindingFlags.DeclaredOnly))
			{

				if (currentMethod.IsFinal && currentMethod.IsPrivate)
				{
					if (string.Equals(currentMethod.Name, "OnMapReady", StringComparison.Ordinal))
					{
						onMapReadyMethodInfo = currentMethod;

						break;
					}

					if (currentMethod.Name.EndsWith(".OnMapReady", StringComparison.Ordinal))
					{
						onMapReadyMethodInfo = currentMethod;

						break;
					}
				}
			}

			if (onMapReadyMethodInfo != null)
			{
				onMapReadyMethodInfo.Invoke(this, new[] { googleMap });
			}
		}

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            PinosFotos = CMAP.PinosFotos;

            if (PinosFotos == null)
                return;

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn && map != null)
            {
                map.Clear();

                //Coloca todos os pinos
                foreach (var pin in PinosFotos)
                {
                    var marker = new MarkerOptions();
                    marker.SetTitle(pin.POST.Id.ToString());
                    marker.SetPosition(new LatLng(pin.POST.Latitude, pin.POST.Longitude));
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.PinoFoto));
                    map.AddMarker(marker);
                }

                isDrawn = true;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            //Para rodar no emulador
            try
            {
                base.OnLayout(changed, l, t, r, b);
                if (changed)
                    isDrawn = false;
            }
            catch
            { }
        }


        //Mostra o Preview da Foto + Dados
        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {

                var PIN = GETPinoFoto(marker);
                if (PIN == null)
                    throw new Exception("Pino Não encontrado");

                Android.Views.View view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

                //Nome Usuário
                TextView InfoUsuarioNome = view.FindViewById<TextView>(Resource.Id.InfoWindowUsuarioNome);
                if (InfoUsuarioNome != null)
                    InfoUsuarioNome.SetText(PIN.POST.UsuarioDados.Nome, TextView.BufferType.Normal);

                //FOTO
                ImageView InfoFoto = view.FindViewById<ImageView>(Resource.Id.InfoWindowFoto);
                if (InfoFoto != null)
                    GetImageBitmapFromUrl(InfoFoto, PIN.POST.FotoURLMiniatura);

                return view;
            }
            return null;
        }

        //GET IMAGEM
        async void GetImageBitmapFromUrl(ImageView img, string url)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    var bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    img.SetImageBitmap(bitmap);
                }
            }
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        //CLICK na FOTO
        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var PIN = GETPinoFoto(e.Marker);
            if (PIN == null)
                throw new Exception("Pino Não encontrado");

            if (PIN.POST != null)
                PIN.ClicaPino();
        }
    }
}

