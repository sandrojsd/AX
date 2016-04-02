using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Mapas
{
    public class MAPA1 : ContentPage
    {
        public MAPA1()
        {
            var MAPA = new Map();
            MAPA.VerticalOptions = LayoutOptions.FillAndExpand;

            var Layout = new StackLayout();
            Layout.Spacing = 0;
            Layout.Children.Add(MAPA);

            Content = Layout;
        }

    }
}
