using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragDrop.Helper
{
   public interface  ItemTouchHelperAdapter
    {
        bool OnItemMove(int fromPosition,int toPosition);
        void OnItemDismiss(int position);

    }
}