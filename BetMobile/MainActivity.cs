using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Ads;
using BetMobile.WebReference;
namespace BetMobile
{
    [Activity(Label = "BetMobile", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private string _userName;

        AdView _bannerad;
        private Button _buttonToLogin;
        private Button _buttonToRegister;
        private Button _buttonLogin;
        private TextView _textViewLoginError;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            GetControlsFromLayout();
            AddClickToButton();
            var ad = FindViewById<AdView>(Resource.Id.adMobBanner);
            var adRequest = new AdRequest.Builder().Build();
           
            ad.LoadAd(adRequest);
       }


        private void GetControlsFromLayout()
        {
            _buttonToLogin = FindViewById<Button>(Resource.Id.buttonToLogin); 
            _buttonToRegister = FindViewById<Button>(Resource.Id.buttonToRegister); 
           
        }
       // ads:adUnitId="ca-app-pub-3940256099942544/6300978111"

        private void AddClickToButton()
        {
            _buttonToLogin.Click += ButtonClick;
            _buttonToRegister.Click += ButtonClick;
        }



        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Id)
            {
                case Resource.Id.buttonToLogin:
                    {
                        SetContentView(Resource.Layout.Login);
                        _buttonLogin = FindViewById<Button>(Resource.Id.buttonLogIn);
                        _buttonLogin.Click += ButtonClick;
                        _textViewLoginError = FindViewById<TextView>(Resource.Id.textViewLoginError);
                        _textViewLoginError.Visibility = ViewStates.Invisible;
                        break;
                    }

                case Resource.Id.buttonToRegister:
                    {
                        SetContentView(Resource.Layout.Main);
                        break;
                    }
                case Resource.Id.buttonLogIn:
                    {
                        EditText login= FindViewById<EditText>(Resource.Id.editTextLogin);
                        EditText password = FindViewById<EditText>(Resource.Id.editTextPassword);
                        WSToDatabase ws = new WSToDatabase();
                        ws.Url = "http://95.108.69.237:3000/WSToDatabase.asmx";
                        string loged =ws.Login(login.Text.ToString(),password.Text.ToString());
                        if(loged=="true")
                        {
                            _userName = login.Text;
                            SetContentView(Resource.Layout.Main);
                        }
                        else
                        {
                            _textViewLoginError.Visibility = ViewStates.Visible;
                        }
                        
                        break;
                    }


            }
        }

    }
}
