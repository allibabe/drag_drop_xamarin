using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragDrop.Helper
{
    public class MyItemTouchHelperCallBack : ItemTouchHelper.Callback
    {

        public static float Alpha_full = 1.0f;
        private ItemTouchHelperAdapter _adapter;

        public MyItemTouchHelperCallBack(ItemTouchHelperAdapter adapter) {
            _adapter = adapter;
        }


        public override bool IsLongPressDragEnabled => true;
        public override bool IsItemViewSwipeEnabled => true;



        public override int GetMovementFlags(RecyclerView  recyclerView, RecyclerView.ViewHolder  viewHolder)
        {
            if (recyclerView.GetLayoutManager() is GridLayoutManager)
            {
                var dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down | ItemTouchHelper.Left | ItemTouchHelper.Right;
                var swipeFlags = 0;
                return MakeMovementFlags(dragFlags, swipeFlags);
            }
            else {
                var dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
                var swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
                return MakeMovementFlags(dragFlags, swipeFlags);


            }



        }

        public override bool OnMove(RecyclerView  recyclerView, RecyclerView.ViewHolder  viewHolder, RecyclerView.ViewHolder  targetView)
        {
            if (viewHolder.ItemViewType != targetView.ItemViewType)
                return false;

            _adapter.OnItemMove(viewHolder.AdapterPosition, targetView.AdapterPosition);
            return true;

        }


        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            if (actionState == ItemTouchHelper.ActionStateSwipe) {
                var alpha = Alpha_full - Math.Abs(dX) / (float)viewHolder.ItemView.Width;
                viewHolder.ItemView.Alpha = alpha;
                viewHolder.ItemView.TranslationX = dX;
            }else
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
        }

        public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            base.ClearView(recyclerView, viewHolder);
            viewHolder.ItemView.Alpha = Alpha_full;
            if (viewHolder is ItemTouchHelperViewHolder) {
                var ItemHolder = viewHolder as ItemTouchHelperViewHolder;
                ItemHolder.OnItemCleared();
            }

        }

        public override void OnSelectedChanged(RecyclerView.ViewHolder viewHolder, int actionState)
        {
            if (actionState != ItemTouchHelper.ActionStateIdle) {

                if (viewHolder is ItemTouchHelperViewHolder)
                {
                    var ItemHolder = viewHolder as ItemTouchHelperViewHolder;
                    ItemHolder.OnItemSelected();
                }

            }

            base.OnSelectedChanged(viewHolder, actionState);
        }


        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int p1)
        {
            _adapter.OnItemDismiss(viewHolder.AdapterPosition);
        }

    }
}