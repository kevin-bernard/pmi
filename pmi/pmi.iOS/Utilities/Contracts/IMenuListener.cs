using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace pmi.iOS.Utilities.Contracts
{
    public interface IMenuListener
    {
        void OnMenuItemClick(string title, string url, int index);
    }
}