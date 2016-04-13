using FrazzApps.Xamarin.GoogleAnalyticsConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogCCA.Telas
{
    public class DetalhePost : ContentPage
    {
        Post POST;
        GoogleAnalytics GA;

        public DetalhePost(Post P)
        {
            POST = P;

            NavigationPage.SetHasNavigationBar(this, false);

            //< trackingID >
            //< appName >
            //< appVersion >
            //< appId >
            //< appInstallerId >
            GA = new GoogleAnalytics(
                            "UA-17713535-12",
                            "AppCCA",
                            "",
                            "",
                            "");

            GA.TrackPage("APP", Device.OnPlatform("IOS", "ANDROID", "WPHONE") + POST.title);

            BackgroundColor = Color.White;

            String CSS = @"

<!DOCTYPE html>
    <head>
        <meta charset='UTF-8' />

        <style type='text/css'>
            body{
                font-family: 'Verdana';
                font-size: 14px;
                margin: 0px;
            }
        </style>

    </head>
    <body>
            ";

            Button BTN = new Button();
            BTN.Text = "X";
            BTN.TextColor = Color.FromHex("#E9BD10");
            BTN.BackgroundColor = Color.Transparent;
            BTN.FontSize = 30;

            BTN.Clicked += (s, e) =>
            {
                App.NAV.PopAsync();
            };

            BTN.HorizontalOptions = LayoutOptions.End;


            Button BTN_FAV = new Button();

            DB.Inicializa();
            if (!DB.PostEhFavorito(POST))
                BTN_FAV.Text = "Adicionar aos Favoritos";
            else
                BTN_FAV.Text = "Remover dos Favoritos";
            DB.Finaliza();

            BTN_FAV.TextColor = Color.FromHex("#E9BD10");
            BTN_FAV.FontSize = 14;
            BTN_FAV.WidthRequest = 300;

            BTN_FAV.BackgroundColor = Color.FromHex("#E9BD10");
            BTN_FAV.TextColor = Color.FromHex("#000000");

            BTN_FAV.Clicked += (s, e) =>
            {
                DB.Inicializa();
                if (DB.PostEhFavorito(POST))
                {
                    DB.RemovePostFavorito(P);
                    BTN_FAV.Text = "Adicionar aos Favoritos";

                    GA.TrackEvent("APP", "Favorito", "REMOVIDO - " +  Device.OnPlatform("IOS", "ANDROID", "WPHONE") + POST.title);
                }
                else
                {
                    DB.SalvaPostFavorito(P);
                    BTN_FAV.Text = "Remover dos Favoritos";

                    GA.TrackEvent("APP", "Favorito", "ADICIONADO - " + Device.OnPlatform("IOS", "ANDROID", "WPHONE") + POST.title);
                }
                DB.Finaliza();
            };
            BTN_FAV.HorizontalOptions = LayoutOptions.CenterAndExpand;

            Content = new StackLayout {
                Children = {
                    new StackLayout
                    {
                        Children= {
                            new StackLayout
                            {
                                Children= {
                                    new StackLayout
                                    {
                                        Children= {
                                            new Label
                                            {
                                                Text = P.title,
                                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                                FontAttributes = FontAttributes.Bold,
                                                TextColor = Color.FromHex("#666666"),
                                            },
                                        },
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                    },
                                    new StackLayout
                                    {
                                        Children= {
                                            new Label
                                            {
                                                Text = P.Data,
                                                HorizontalOptions = LayoutOptions.Start,
                                                TextColor = Color.FromHex("#14CD95"),
                                                FontSize = 12
                                            },
                                            new Label
                                            {
                                                Text = P.author.name,
                                                HorizontalOptions = LayoutOptions.End,
                                                TextColor = Color.FromHex("#D56F00"),
                                                FontSize = 12
                                            },
                                        },
                                        Orientation = StackOrientation.Horizontal,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                    }
                                },
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                            },
                            BTN
                        },
                        Orientation = StackOrientation.Horizontal
                    },
                    new WebView
                    {
                        Source = new HtmlWebViewSource
                        {
                            Html = CSS + P.content + "</body></html>",
                        },
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    BTN_FAV
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
            };
        }
    }
}
