using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using pmi.iOS.Utilities.Contracts;
using UIKit;

namespace pmi.iOS.Utilities
{
    public class MenuClickEvent
    {
        private static List<IMenuListener> listeners = new List<IMenuListener>();

        public static void AddListener(IMenuListener listener)
        {
            listeners.Add(listener);
        }

        public static void Notify(string title, string url, int index)
        {
            foreach (IMenuListener listener in listeners)
            {
                listener.OnMenuItemClick(title, url, index);
            }
        }

    }
}