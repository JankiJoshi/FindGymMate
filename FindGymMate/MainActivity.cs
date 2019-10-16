
/*
 * MainActivity opens up Map with markers of gym locations
 */
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace FindGymMate
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
        private GoogleMap GMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.activity_main);
                Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                SetSupportActionBar(toolbar);

                FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
                fab.Click += FabOnClick;
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Error:" + e.InnerException);
            }
            try
            {
                SetUpMap();
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Error in setUpMap:" + e.InnerException);
            }
        }

        /// <summary>
        /// Sets up Map
        /// </summary>
        private void SetUpMap()
        {
            System.Console.WriteLine("setUpMap");

            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
            }
        }

        /// <summary>
        /// creates marker from the provided list
        /// </summary>
        /// <param name="googleMap"></param>
        public void OnMapReady(GoogleMap googleMap)
        {
            this.GMap = googleMap;

            //get list of locations and add it to a list of Strings(listOfLocations)
            List<string> listOfLocations= new List<string>() { "Conestoga College Doon Campus, Kitchener, ON",
                "100 Old Carriage Drive, Kitchener, ON" };

            foreach(String location in listOfLocations)
            {
                var geo = new Geocoder(this);
                var addresses = geo.GetFromLocationName(location, 1);
                double lat = 0, lon = 0;
                foreach (Address address in addresses)
                {
                    lat = address.Latitude;
                    lon = address.Longitude;
                }
                LatLng latlng = new LatLng(lat, lon);
                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
                GMap.MoveCamera(camera);
                MarkerOptions options = new MarkerOptions();
                options.SetPosition(latlng);
                options.SetSnippet(location);
                GMap.AddMarker(options);
                GMap.MarkerClick += GMap_MarkerClick;
            }
        }

        /// <summary>
        /// Opens up a new activity with location Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GMap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            //System.Console.WriteLine("Marker_Clicked" + e.Marker.Snippet.ToString());
            //var intent = new Intent(this, typeof(LocationDetailsActivity));
            //intent.PutExtra("location_data", e.Marker.Snippet.ToString());
            //this.StartActivity(intent);
            //To Do
            //send location details to the new activity
            var intent = new Intent(this, typeof(UserListActivity));
            this.StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_about)
            {
                var intent = new Intent(this, typeof(AboutActivity));
                this.StartActivity(intent);
                return true;
            }
            else if (id == Resource.Id.action_add_profile)
            {
                var intent = new Intent(this, typeof(AddProfileActivity));
                this.StartActivity(intent);
                return true;
            }
            else if (id == Resource.Id.action_user_list)
            {
                var intent = new Intent(this, typeof(UserListActivity));
                this.StartActivity(intent);
                return true;
            }
            else if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

