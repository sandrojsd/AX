using FormsPlugin.Iconize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaXamarinMobile.Views
{
    public partial class Home : IconTabbedPage
    {
        public Home()
        {
            InitializeComponent();

            Title = "InstaXamarin";

            //MessagingCenter.Subscribe<Object, int>(this, "NotificacaoRecebida", async (sender, IDPost) => {
            //    Post P = await new FeedViewModel().GetPost(IDPost);
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        Navigation.PushModalAsync(new PostDetalhado(P));
            //    });
            //});

            MessagingCenter.Subscribe<Object, int>(this, "NotificacaoRecebida", (sender, IDPost) => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CurrentPage = FeedTab; // Seleciona a primeira aba
                    FeedTab.AbrePost(IDPost); // Abre o post
                });
            });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PushNotification.Plugin.CrossPushNotification.Current.Register();
        }
    }
}
