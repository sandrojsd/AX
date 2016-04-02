using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ActionBar_TABS
{
    public class Fragment1 : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment, container, false);
            var sampleTextView = view.FindViewById<TextView>(Resource.Id.TXT);
            sampleTextView.Text = "Fragmento 1";

            return view;
        }
    }
}