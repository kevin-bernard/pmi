using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using pmi.Core.Utilities;
using pmi.iOS.Services;
using pmi.iOS.Views;
using UIKit;

namespace pmi.iOS.Utilities
{
    public class LangTableSource : UITableViewSource
    {
        List<Lang> TableItems;

        OptionView View;

        string CellIdentifier = "TableCell";

        private double tableHeight = 0;


        public LangTableSource(List<Lang> items, OptionView view)
        {
            TableItems = items;
            View = view;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Count;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            LangTableRow cell = tableView.DequeueReusableCell(CellIdentifier) as LangTableRow;
            Lang item = TableItems[indexPath.Row];

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            { cell = new LangTableRow(new NSString(CellIdentifier)); }

            cell.TintColor = Style.OptionView.ContentColor;

            cell.UpdateCell(item.Text, UIImage.FromBundle(item.ImageName));
            
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            cell.AccessoryView = new UIImageView(UIImage.FromBundle("forward"));

            cell.BackgroundColor = UIColor.Clear;
            tableHeight += cell.Bounds.Height;

            tableView.Frame = new CGRect(tableView.Frame.X, tableView.Frame.Y, View.Layout.Bounds.Width, tableHeight);

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            View.UpdateLang(TableItems[indexPath.Row]);

            tableView.UserInteractionEnabled = false;
        }
    }
}