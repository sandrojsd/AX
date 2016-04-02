// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ListasIOS
{
	[Register ("Celula")]
	partial class Celula
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgItem { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblItem { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (imgItem != null) {
				imgItem.Dispose ();
				imgItem = null;
			}
			if (lblItem != null) {
				lblItem.Dispose ();
				lblItem = null;
			}
		}
	}
}
