using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ActionBar_TABS
{
    [Activity(Label = "Android TABS", MainLauncher = true, Icon = "@drawable/icon")]
    public class TABS : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Tabs);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            //TAB1
            var FRAGMENT1 = new Fragment1();
            var tab = this.ActionBar.NewTab();
            tab.SetText("TAB 1");
            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, FRAGMENT1);
            };
            tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(FRAGMENT1);
            };
            this.ActionBar.AddTab(tab);

            //TAB2
            var FRAGMENT2 = new Fragment2();
            tab = this.ActionBar.NewTab();
            tab.SetText("TAB 2");
            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, FRAGMENT2);
            };
            tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(FRAGMENT2);
            };
            this.ActionBar.AddTab(tab);

        }
    }
}

