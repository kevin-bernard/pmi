using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.App;
using Android;
using pmi.Core.Utilities;

namespace pmi.Droid.Utilities
{
    public class LangAdapter : BaseAdapter<Lang>
    {
        public delegate View GetOriginalView(int position, View convertView, ViewGroup parent);

        public event GetOriginalView TriggerSwitchUrl;

        private List<Lang> langs;
        private Activity context;

        public LangAdapter(Activity ctx, List<Lang> langs, GetOriginalView e) {
            this.context = ctx;
            this.langs = langs;
            TriggerSwitchUrl += e;
        }
         
        public override Lang this[int position]
        {
            get
            {
                return langs[position];
            }
        }

        public override int Count
        {
            get
            {
                return langs.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return TriggerSwitchUrl(position, convertView, parent);
        }
    }
}
