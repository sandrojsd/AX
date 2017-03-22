using InstaXamarinMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaXamarinMobile.Custom
{
    public class PinoFoto
    {
        public Post POST { get; set; }

        public delegate void ClicaPost(Post post);
        public event ClicaPost OnClicaPost;

        public void ClicaPino()
        {
            if (OnClicaPost != null)
                OnClicaPost(POST);
        }
    }
}
