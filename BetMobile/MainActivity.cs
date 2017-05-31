using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Ads;
using BetMobile.WebReference;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BetMobile
{
    [Activity(Label = "BetMobile", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private string _userName = "";
        private int _money = 0;

        AdView _bannerad;
        private Button _buttonToLogin;
        private Button _buttonToRegister;
        private Button _buttonToMaches;
        private Button _buttonToBets;
        private Button _buttonToInfo;
        private Button _buttonLogout;
        private Button _buttonToClose;
        private Button _buttonToAddMoney;
        private Button _buttonToGetMoney;


        private Button _buttonLogin;
        private Button _buttonRegister;
        private Button _buttonBack;
        private Button _buttonAddMoney;
        private Button _buttonGetMoney;

        private TextView _textViewLoginError;
        private TextView _textViewAccount;
        private TextView _textViewMoney;

        private TextView _textViewNumber;
        private TextView _textViewGosp;
        private TextView _textViewGosc;
        private TextView _textViewResult;
        private TextView _textViewKurs;
        private TextView _textViewStawka;
        private TextView _textViewTyp;
        private TextView _textViewWygrana;

        private ListView _listViewMatches;
        private List<string> _matches;
        private ListView _listViewBets;
        private List<string> _bets;
        public WSToDatabase ws;

        protected override void OnCreate(Bundle bundle)
        {
            ws = new WSToDatabase();
            ws.Url = "http://95.108.69.237:3000/WSToDatabase.asmx";
            base.OnCreate(bundle);
            ConstructStartPage();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            EditText login;
            EditText password;
            EditText money;

            switch (btn.Id)
            {
                #region Bottomto
                case Resource.Id.buttonToLogin:
                    {
                        ConstructLoginPage();
                        break;
                    }

                case Resource.Id.buttonToRegister:
                    {
                        ConstructRegistePage();
                        break;
                    }
                case Resource.Id.buttonToMatches:
                   // ConstructMatchesView();
                    ConstructBetInfoView();
                    break;
                case Resource.Id.buttonToBets:
                    ConstructBetsView();
                   
                    break;
                case Resource.Id.buttonToInfo:
                    ConstructAccountInfoPage();
                    break;
                case Resource.Id.buttonLogout:
                    _userName = "";
                    ConstructStartPage();
                    break;
                case Resource.Id.buttonClose:
                    this.Finish();
                    break;
                case Resource.Id.buttonToAddMoney:
                    ConstructAddMoneyPage();
                    break;
                case Resource.Id.buttonToGetMoney:
                    ConstructGetMoneyPage();
                    break;
                #endregion

                #region BottonAccount
                case Resource.Id.buttonLogIn:
                    {
                        login = FindViewById<EditText>(Resource.Id.editTextLogin);
                        password = FindViewById<EditText>(Resource.Id.editTextPassword);

                        string loged = ws.Login(login.Text.ToString(), password.Text.ToString());
                        if (loged == "true")
                        {
                            _userName = login.Text;
                            _money = Convert.ToInt32(ws.GetUserMoney(_userName));
                            btn.RequestFocus();
                            ConstructStartPage();
                        }
                        else
                        {
                            _textViewLoginError.Visibility = ViewStates.Visible;
                        }

                        break;
                    }
                case Resource.Id.buttonRegister:
                    {
                        login = FindViewById<EditText>(Resource.Id.editTextLogin);
                        password = FindViewById<EditText>(Resource.Id.editTextPassword);
                        EditText password2 = FindViewById<EditText>(Resource.Id.editTextPassword2);
                        if (password.Text.ToString() == password2.Text.ToString())
                        {
                            bool register = ws.AddNewUser(login.Text.ToString(), password.Text.ToString(), 18);
                            if (register == true)
                            {
                                btn.RequestFocus();
                                ConstructStartPage();
                            }
                            else
                            {
                                _textViewLoginError.Visibility = ViewStates.Visible;
                            }
                        }
                        else
                        {
                            _textViewLoginError.Visibility = ViewStates.Visible;
                        }

                        break;
                    }

                case Resource.Id.buttonAddMoney:
                    money = FindViewById<EditText>(Resource.Id.editTextMoney);
                    ws.SetUserMoney(_userName, Convert.ToInt32(money.Text.ToString()));
                    _money = Convert.ToInt32(ws.GetUserMoney(_userName));
                    btn.RequestFocus();
                    ConstructAccountInfoPage();
                    break;

                case Resource.Id.buttonGetMoney:
                    money = FindViewById<EditText>(Resource.Id.editTextMoney);
                    ws.SendUserMoney(_userName,Convert.ToInt32(money.Text.ToString()));
                    _money = Convert.ToInt32(ws.GetUserMoney(_userName));
                    btn.RequestFocus();
                    ConstructAccountInfoPage();
                    break;

                #endregion

                case Resource.Id.buttonBack:
                    ConstructStartPage();
                    break;


            }
        }
        private void ListViewClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ListView lw = (ListView)sender;


            switch (lw.Id)
            {
               
                case Resource.Id.ListViewMatches:
                    {
                        ConstructLoginPage();
                        break;
                    }


            }
        }
        private void ConstructStartPage()
        {
            SetContentView(Resource.Layout.Main);

            _buttonToLogin = FindViewById<Button>(Resource.Id.buttonToLogin);
            _buttonToRegister = FindViewById<Button>(Resource.Id.buttonToRegister);
            _buttonToMaches = FindViewById<Button>(Resource.Id.buttonToMatches);
            _buttonToBets = FindViewById<Button>(Resource.Id.buttonToBets);
            _buttonToInfo = FindViewById<Button>(Resource.Id.buttonToInfo);
            _buttonLogout = FindViewById<Button>(Resource.Id.buttonLogout);
            _buttonToClose = FindViewById<Button>(Resource.Id.buttonClose);

            _buttonToLogin.Click += ButtonClick;
            _buttonToRegister.Click += ButtonClick;
            _buttonToMaches.Click += ButtonClick;
            _buttonToBets.Click += ButtonClick;
            _buttonToInfo.Click += ButtonClick;
            _buttonLogout.Click += ButtonClick;
            _buttonToClose.Click += ButtonClick;

            if (_userName == "")
            {
                _buttonToLogin.Enabled = true;
                _buttonToRegister.Enabled = true;
                _buttonToMaches.Enabled = false;
                _buttonToBets.Enabled = false;
                _buttonToInfo.Enabled = false;
                _buttonLogout.Enabled = false;
                _buttonToClose.Enabled = true;
            }
            else
            {
                _buttonToLogin.Enabled = false;
                _buttonToRegister.Enabled = false;
                _buttonToMaches.Enabled = true;
                _buttonToBets.Enabled = true;
                _buttonToInfo.Enabled = true;
                _buttonLogout.Enabled = true;
                _buttonToClose.Enabled = true;
            }
            var ad = FindViewById<AdView>(Resource.Id.adMobBanner);
            var adRequest = new AdRequest.Builder().Build();

            ad.LoadAd(adRequest);
        }
        private void ConstructLoginPage()
        {
            SetContentView(Resource.Layout.Login);
            _buttonLogin = FindViewById<Button>(Resource.Id.buttonLogIn);
            _buttonLogin.Click += ButtonClick;
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
            _textViewLoginError = FindViewById<TextView>(Resource.Id.textViewLoginError);
            _textViewLoginError.Visibility = ViewStates.Invisible;

        }
        private void ConstructRegistePage()
        {

            SetContentView(Resource.Layout.Register);
            _buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);
            _buttonRegister.Click += ButtonClick;
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
            _textViewLoginError = FindViewById<TextView>(Resource.Id.textViewLoginError);
            _textViewLoginError.Visibility = ViewStates.Invisible;


        }

        private void ConstructMatchesView()
        {
            SetContentView(Resource.Layout.Matches);
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
            _listViewMatches = FindViewById<ListView>(Resource.Id.ListViewMatches);
            //_listViewMatches.Click += ListViewClick;
            _matches = new List<string>();
            _matches.Add("1");
            _matches.Add("2");
            _matches.Add("3");
            _matches.Add("4");
            _matches.Add("5");
            ArrayAdapter<string> adapater = new ArrayAdapter<string>(this, Resource.Layout.ListItem, _matches);
            _listViewMatches.Adapter = adapater;
        }
        private void ConstructBetsView()
        {
            SetContentView(Resource.Layout.Bets);
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
            _listViewBets = FindViewById<ListView>(Resource.Id.listViewBets);
            //_listViewMatches.Click += ListViewClick;
            var list  = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Bet>>(ws.GetUserBets(_userName.ToString()));
            _bets = new List<string>();
            for(int i=0;i<list.Count;i++)
            {
                _bets.Add(list[i].id_zaklad.ToString());
            }            
            
            ArrayAdapter<string> adapater = new ArrayAdapter<string>(this, Resource.Layout.ListItem, _bets);
            _listViewBets.Adapter = adapater;

        }
        private void ConstructAccountInfoPage()
        {
            SetContentView(Resource.Layout.Info);
            _buttonToAddMoney = FindViewById<Button>(Resource.Id.buttonToAddMoney);
            _buttonToAddMoney.Click += ButtonClick;
            _buttonToGetMoney = FindViewById<Button>(Resource.Id.buttonToGetMoney);
            _buttonToGetMoney.Click += ButtonClick;
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;

            _textViewAccount = FindViewById<TextView>(Resource.Id.textViewLogin);
            _textViewAccount.Text = "Nazwa użytkownika: " + _userName;
            _textViewMoney = FindViewById<TextView>(Resource.Id.textViewMoney);
            _textViewMoney.Text = "Stan konta: " + _money + " PLN";


        }

        private void ConstructBetInfoView()
        {
            var currentbet = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Bet>>(ws.GetUserBets(_userName.ToString()));
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(currentbet[0].id_mecz)));
            var team1 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosp))).nazwa;
            var team2 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosp))).nazwa;
            SetContentView(Resource.Layout.BetInfo);
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
            _textViewNumber = FindViewById<TextView>(Resource.Id.textViewNumber);
            _textViewGosp = FindViewById<TextView>(Resource.Id.textViewGosp);
            _textViewGosc = FindViewById<TextView>(Resource.Id.textViewGosc);
            _textViewResult = FindViewById<TextView>(Resource.Id.textViewResult);
            _textViewStawka = FindViewById<TextView>(Resource.Id.textViewStawka);
            _textViewKurs = FindViewById<TextView>(Resource.Id.textViewKurs);
            _textViewTyp = FindViewById<TextView>(Resource.Id.textViewTyp);
            _textViewWygrana = FindViewById<TextView>(Resource.Id.textViewWygrana);
            _textViewNumber.Text = "Zakład numer: "+ currentbet[0].id_zaklad;
            _textViewGosp.Text = "Drużyna Gospodaży: "+ team1;
            _textViewGosc.Text = "Drużyna gości: "+ team2;
            _textViewResult.Text = "Wynik: " + match.bramki_gosp + " : " + match.bramki_gosc;
            _textViewStawka.Text = "Stawka: "+ currentbet[0].kwota;
            _textViewKurs.Text = "Kurs: "+ match.kurs ;
            _textViewTyp.Text = "Typ: "+ currentbet[0].typ; ;
            _textViewWygrana.Text ="Wygrana: "+currentbet[0].wygrana;




        }

        private void ConstructAddMoneyPage()
        {
            SetContentView(Resource.Layout.AddMoney);
            _buttonAddMoney = FindViewById<Button>(Resource.Id.buttonAddMoney);
            _buttonAddMoney.Click += ButtonClick;
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
        }
        private void ConstructGetMoneyPage()
        {
            SetContentView(Resource.Layout.GetMoney);
            _buttonGetMoney = FindViewById<Button>(Resource.Id.buttonGetMoney);
            _buttonGetMoney.Click += ButtonClick;
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
        }
    }
}
