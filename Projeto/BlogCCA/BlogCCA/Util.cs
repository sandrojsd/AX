using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogCCA
{
    public class Util
    {
        public static Grid ConteudoComLoading(StackLayout Conteudo, ref Telas.Loading LOAD)
        {
            LOAD = new Telas.Loading();

            return new Grid()
            {
                Children = {
                    Conteudo,
                    LOAD.Content
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };
        }
    }
}
