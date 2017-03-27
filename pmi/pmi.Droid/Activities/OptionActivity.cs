using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;

using pmi.Core.Views;

using pmi.Core.Services;
using pmi.Core.Utilities;

using pmi.Droid.Utilities;

using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Widget;
using Android.Views;
using Android.Content.PM;
using Android.Graphics;

namespace pmi.Droid.Activities
{
    [Activity(
        Theme = "@style/AppTheme",
        Icon = "@drawable/icon",
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleInstance,
        Name = "pmi.droid.activities.OptionActivity"
    )]
    public class OptionActivity : BaseActivity<OptionViewModel>
    {
        private LangAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_options);
            
            ListView lst = FindViewById<ListView>(Resource.Id.lstview_options);
            var langs = ViewModel.Langs;
            adapter = new LangAdapter(this, langs, GetView);

            lst.Adapter = adapter;
            lst.ItemClick += OnListItemClick;
          
            SetTitle(Translator.GetText("choose_lang"));
        }
        
        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Lang lang = adapter[e.Position];

            ViewModel.SelectedLang = lang.Value;

            ViewModel.ShowHomeCommand.Execute();
        }

        public View GetView(int position, View convertView, ViewGroup parent) {

            View view = convertView;

            if (view == null)
                view = LayoutInflater.Inflate(Resource.Layout.options_list_row, parent, false);

            Lang item = adapter[position];

            var img = view.FindViewById<ImageView>(Resource.Id.Image);
            img.SetScaleType(ImageView.ScaleType.CenterInside);
            img.SetImageResource(Resources.GetIdentifier(String.Format("drawable/{0}", item.ImageName), null, PackageName));
            
            var txt = view.FindViewById<TextView>(Resource.Id.Title);

            txt.Text = item.ToString();

            SetFont(txt, "PTSans-Regular.ttf");

            try
            {
                txt.SetPadding(img.Drawable.IntrinsicWidth + img.PaddingLeft + 40, img.Drawable.IntrinsicHeight / 2, 0, 0);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            

            return view;
        }
    }
}