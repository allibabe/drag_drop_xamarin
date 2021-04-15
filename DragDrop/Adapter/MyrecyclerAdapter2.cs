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
    public class MyrecyclerAdapter2 : RecyclerView.Adapter
    {
        private Context _context;
        public List<string> _srtingList1;
      
        public MyrecyclerAdapter2(Context context,
        List<string> srtingList
      
        )
        {
            _context = context;
            _srtingList1  = srtingList;
  
        }

        public override int ItemCount => _srtingList1.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // code later
            MyViewHolder myViewHolder = holder as MyViewHolder;
            
            myViewHolder.text_title.Text = _srtingList1[position];

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new MyViewHolder(LayoutInflater.From(_context).Inflate(Resource.Layout.layout1, parent, false));

        }

        public class MyViewHolder : RecyclerView.ViewHolder
        {

            public TextView text_title;
            public MyViewHolder(View view) : base(view)
            {
                text_title = view.FindViewById<TextView>(Resource.Id.textView1);
         
            }

        }

        public void OnItemDismiss(int position)
        {

            _srtingList1.RemoveAt(position);
            NotifyItemRemoved(position);

        }


    }
}