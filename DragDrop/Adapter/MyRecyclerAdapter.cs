using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.RecyclerView.Widget;
using DragDrop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragDrop.Adapter
{
    public class MyRecyclerAdapter : RecyclerView.Adapter, ItemTouchHelperAdapter
    {
       private Context _context;
       public List<string> _srtingList;
       private OnStartDragListener _listener;

        public MyRecyclerAdapter(Context context,
        List<string> srtingList,
        OnStartDragListener listener
        )
        {
            _context = context;
            _srtingList = srtingList;
            _listener = listener;

        }

        public override int ItemCount => _srtingList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // code later
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.text_number.Text = _srtingList[position];
            myViewHolder.text_title.Text = (position + 1).ToString();

            myViewHolder.cardView.SetOnLongClickListener(new MyLongClickEvent(myViewHolder, _listener));

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new MyViewHolder(LayoutInflater.From(_context).Inflate(Resource.Layout.card_item,parent,false));

        }

        public class  MyViewHolder: RecyclerView.ViewHolder
        {

            public TextView text_title, text_number;
            public CardView cardView;
            public MyViewHolder(View view):base(view)
            {
                text_title = view.FindViewById<TextView>(Resource.Id.text_title);
                text_number = view.FindViewById<TextView>(Resource.Id.text_number);
                cardView = view.FindViewById<CardView>(Resource.Id.cardView);

            }

        }

        public void OnItemDismiss(int position)
        {

            _srtingList.RemoveAt(position);
            NotifyItemRemoved(position);

        }

        public bool OnItemMove(int fromPosition, int toPosition)
        {

            ListUtils.Swap(_srtingList,fromPosition,toPosition);
            NotifyItemMoved(fromPosition, toPosition);
            Console.WriteLine(_srtingList[fromPosition] + "theList from ");
            Console.WriteLine(_srtingList[toPosition] + "theList to ");

            return true;

        }

        private class MyLongClickEvent : Java.Lang.Object,View.IOnLongClickListener
        {
            private MyViewHolder _myViewHolder;
            private OnStartDragListener _onStartDragListener;

            public MyLongClickEvent(MyViewHolder myViewHolder, OnStartDragListener onStartDragListener)
            {
                _myViewHolder = myViewHolder;
                _onStartDragListener = onStartDragListener;
            }

            public bool OnLongClick(View view) 
            {

                _onStartDragListener.OnStartDrag(_myViewHolder);
                return true;
            }

        }


    }
}