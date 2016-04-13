using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogCCA;
using BlogCCA.Telas;
using BlogCCA.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using Microsoft.Phone.Shell;

[assembly: ExportRenderer(typeof(Menu), typeof(RenderMenu))]

namespace BlogCCA.WinPhone
{
    public class RenderMenu : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);

            //var mainPage = ((PhoneApplicationFrame)App.Current.RootVisual).Content as MainPage;
            
            //mainPage.ApplicationBar = new ApplicationBar();

            //foreach (var menuItem in menuPageViewModel.MenuItems)
            //{
            //    ((IList<IApplicationBarMenuItem>)mainPage.ApplicationBar.MenuItems).Add(menuItem);
            //}

            //return detailPage;


            //var app = (MainPage)((PhoneApplicationFrame)System.Windows.Application.Current.RootVisual).Content;
            //var applicationBar = app.ApplicationBar;
            //applicationBar.Buttons.Clear();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //var app = (MainPage)((PhoneApplicationFrame)System.Windows.Application.Current.RootVisual).Content;
            //var applicationBar = app.ApplicationBar;
            //if (applicationBar.Buttons != null && applicationBar.Buttons.Count > 3)
            //{
            //    applicationBar.Buttons.RemoveAt(0);
            //}

            base.OnElementPropertyChanged(sender, e);
        }

      
    }
}

