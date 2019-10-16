using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FindGymMate
{
    [Activity(Label = "UserListActivity")]
    public class UserListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.UserList);

                Button button1 = FindViewById<Button>(Resource.Id.button1);
                button1.Click += openChat;

                Button button2 = FindViewById<Button>(Resource.Id.button2);
                button2.Click += openChat2;

                Button button3 = FindViewById<Button>(Resource.Id.button3);
                button3.Click += openChat3;
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Error:" + e.InnerException);
            }
        }

        private void openChat(object sender, EventArgs args)
        {
            var intent = new Intent(this, typeof(ChatActivity));
            this.StartActivity(intent);
        }

        private void openChat2(object sender, EventArgs args)
        {
            var intent = new Intent(this, typeof(ChatActivity2));
            this.StartActivity(intent);
        }

        private void openChat3(object sender, EventArgs args)
        {
            var intent = new Intent(this, typeof(ChatActivity3));
            this.StartActivity(intent);
        }

    }
}