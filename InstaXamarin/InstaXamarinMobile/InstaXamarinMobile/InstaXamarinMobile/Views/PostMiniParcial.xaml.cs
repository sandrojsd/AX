using System;
using System.Collections.Generic;
using InstaXamarinMobile.Models;
using Xamarin.Forms;

namespace InstaXamarinMobile
{
    public partial class PostMiniParcial : StackLayout
    {
        public PostMiniParcial()
        {
            InitializeComponent();
        }

        public delegate void ClicaPost(Post post);
        public event ClicaPost OnClicaPost;

        Post _POST;

        public PostMiniParcial(Post post)
        {
            _POST = post;

            InitializeComponent();

            BindingContext = _POST;
        }

        public void SelecionaPost(object sender, EventArgs e)
        {
            if (OnClicaPost != null)
                OnClicaPost(_POST);
        }
    }
}
