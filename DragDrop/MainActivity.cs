using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using DragDrop.Adapter;
using DragDrop.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DragDrop
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ObservableCollection<string> PlayersSetsCollection;

        RecyclerView recyclerView;
        RecyclerView recyclerView1;

        ItemTouchHelper itemTouchHelper;
        public MyRecyclerAdapter trial;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Init();
            GenerateItem();
            //Btn.Click += Btn_Click; 



        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var tt = trial._srtingList[0];
            var ll = trial._srtingList[trial._srtingList.Count - 1];
            Console.WriteLine(tt + "first");
            Console.WriteLine(ll + "last");

        }

        private void GenerateItem()
        {
            var data = new string[] {"allister","Badong","Radislao","Palermo","Borazon","Fajardo"}.ToList();

            var data1 = new string[] { "1", "2", "3", "4", "5", "6" }.ToList();


            var adapter = new  MyRecyclerAdapter(this,data,new MyImplementDrag(this));
            trial = new MyRecyclerAdapter(this, data, new MyImplementDrag(this));

            var adapter1 = new MyrecyclerAdapter2(this, data1);




            recyclerView.SetAdapter(adapter);
            var callback = new MyItemTouchHelperCallBack(adapter);
            itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(recyclerView);

            recyclerView1.SetAdapter(adapter1);
         
        }


        //private void SetRecyclerAdapter()
        //{

        //    var adapterClassGroup = PlayersSetsCollection.GetRecyclerAdapter(BindClassGroupViewHolder, Resource.Layout.layout1);
        //    RecyclerAttendList.SetAdapter(adapterClassGroup);
        //    linearProgressAttend.Visibility = ViewStates.Invisible;
        //}



        //private void BindClassGroupViewHolder(CachingViewHolder holder, string model, int position)
        //{
        //    var textName = holder.FindCachedViewById<TextView>(Resource.Id.textView1);
        //    textName.Text = model;

        //}


        private void getter() {
            
          var tt =  trial._srtingList[0];
          Console.WriteLine(tt);
        }


        private void Init()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            recyclerView1 = FindViewById<RecyclerView>(Resource.Id.recycler_view1);

            recyclerView1.HasFixedSize = true;

            recyclerView.HasFixedSize = true;
            var layoutmanager = new GridLayoutManager(this,1);
            recyclerView.SetLayoutManager(layoutmanager);

            var layoutmanager1 = new GridLayoutManager(this, 1);
            recyclerView1.SetLayoutManager(layoutmanager1);



        }




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private class MyImplementDrag : OnStartDragListener
        {
            private MainActivity _mainactivity;
            public MyImplementDrag(MainActivity activity)
            {
                _mainactivity = activity;
            }

            public void OnStartDrag(RecyclerView.ViewHolder viewHolder)
            {
                _mainactivity.itemTouchHelper.StartDrag(viewHolder);
            }
        }


        //public Button Btn  => FindViewById<Button>(Resource.Id.button1);


    }
}