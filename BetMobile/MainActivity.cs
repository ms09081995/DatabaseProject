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
        private Button _buttonaddBet;

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
        private TextView _textViewMecz;
        private TextView _textViewMinuta;
        private TextView _textViewStatus;

        private Spinner _spinnerTyp;
        private EditText _editTextKwota;
            

        private ListView _listViewMatches;
        private List<string> _matches;
        private ListView _listViewBets;
        private int _currentMatchID;
        private List<string> _bets;
        public WSToDatabase ws;

        protected override void OnCreate(Bundle bundle)
        {
            ws = new WSToDatabase();
            ws.Url = "http://192.168.43.107:3000/WSToDatabase.asmx";
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
                   ConstructMatchesView();
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
                case Resource.Id.buttonAddBet:
                    money = FindViewById<EditText>(Resource.Id.editTextMoney);
                    var typ = FindViewById<Spinner>(Resource.Id.spinner1).SelectedItemPosition;
                    ws.AddBetForUser(_userName, Convert.ToInt32(money.Text), _currentMatchID, typ);
                    ConstructBetsView();
                    break;
                #endregion

                case Resource.Id.buttonBack:
                    ConstructStartPage();
                    break;


            }
        }
        void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ListView lv = (ListView)sender;

            if (lv.Id== Resource.Id.listViewBets)
            {
                var item = this._listViewBets.GetItemAtPosition(e.Position);
                ConstructBetInfoView(e.Position);
            }
            else
            {
                var item = this._listViewMatches.GetItemAtPosition(e.Position);
                var startpos = item.ToString().IndexOf(":");
                var endpos = item.ToString().IndexOf("M");
                _currentMatchID = Convert.ToInt32(item.ToString().Substring(3, endpos - 4));

                ConstructAddBetView(_currentMatchID);

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
            _listViewMatches.ItemClick += listView_ItemClick;

            _matches = new List<string>();
            var list = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Match>>(ws.GetMatches());
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var team1 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(list[i].id_gosp))).nazwa;
                var team2 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(list[i].id_gosc))).nazwa;

                _matches.Add("ID:" + list[i].id_mecz+ "\nMecz: "+team1+"-"+team2 + "\nMinuta: " + list[i].minuta + "\nKurs: "+list[i].kurs);
            }
            ArrayAdapter<string> adapater = new ArrayAdapter<string>(this, Resource.Layout.ListItem, _matches);
            _listViewMatches.Adapter = adapater;
        }
        private void ConstructBetsView()
        {
            SetContentView(Resource.Layout.Bets);
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
            _listViewBets = FindViewById<ListView>(Resource.Id.listViewBets);
            _listViewBets.ItemClick += listView_ItemClick;
            var list  = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Bet>>(ws.GetUserBets(_userName.ToString()));
            _bets = new List<string>();
            for(int i=list.Count-1;i>=0;i--)
            {
                _bets.Add("Numer zakładu: "+list[i].id_zaklad.ToString() + "\nStatus: " + list[i].status +"\nDo wygrania: " + list[i].wygrana);
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

        private void ConstructBetInfoView(int position)
        {
            var currentbet = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Bet>>(ws.GetUserBets(_userName.ToString()));
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(currentbet[currentbet.Count - 1 - position].id_mecz)));
            var team1 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosp))).nazwa;
            var team2 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosc))).nazwa;
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
            _textViewMinuta = FindViewById<TextView>(Resource.Id.textViewMinuta);
            _textViewStatus = FindViewById<TextView>(Resource.Id.textViewStatus);
            _textViewNumber.Text = "Zakład numer: "+ currentbet[currentbet.Count-1-position].id_zaklad;
            _textViewGosp.Text = "Drużyna Gospodaży: "+ team1;
            _textViewGosc.Text = "Drużyna gości: "+ team2;
            _textViewResult.Text = "Wynik: " + match.bramki_gosp + " : " + match.bramki_gosc;
            _textViewStawka.Text = "Stawka: "+ currentbet[currentbet.Count - 1 - position].kwota;
            _textViewKurs.Text = "Kurs: "+ match.kurs ;
            if(match.minuta>=90)
            {
                _textViewMinuta.Text = "Minuta: Zakończony";
            }
            else
            {
                _textViewMinuta.Text = "Minuta: "+match.minuta;
            }
            _textViewStatus.Text ="Status: " + currentbet[currentbet.Count - 1 - position].status.ToString();


            var typ = currentbet[currentbet.Count - 1 - position].typ;
            switch(typ)
            {
                case 0:
                    _textViewTyp.Text = "Typ: Remis";
                    break;
                case 1:
                    _textViewTyp.Text = "Typ: Wygrana gospodarzy";
                    break;
                case 2:
                    _textViewTyp.Text = "Typ: Wygrana gości";
                    break;
            }
            
            _textViewWygrana.Text ="Do wygrania: "+currentbet[currentbet.Count - 1 - position].wygrana;
        }

        private void ConstructMatchInfoView(int position)
        {
            var currentbet = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Bet>>(ws.GetUserBets(_userName.ToString()));
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(currentbet[0].id_mecz)));
            var team1 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosp))).nazwa;
            var team2 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosc))).nazwa;
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
            _textViewNumber.Text = "Zakład numer: " + currentbet[currentbet.Count - 1 - position].id_zaklad;
            _textViewGosp.Text = "Drużyna Gospodaży: " + team1;
            _textViewGosc.Text = "Drużyna gości: " + team2;
            _textViewResult.Text = "Wynik: " + match.bramki_gosp + " : " + match.bramki_gosc;
            _textViewStawka.Text = "Stawka: " + currentbet[currentbet.Count - 1 - position].kwota;
            _textViewKurs.Text = "Kurs: " + match.kurs;
            _textViewTyp.Text = "Typ: " + currentbet[currentbet.Count - 1 - position].typ; ;
            _textViewWygrana.Text = " Do wygrania: " + currentbet[currentbet.Count - 1 - position].wygrana;
        }

        private void ConstructAddBetView(int position)
        {
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(position)));
            var team1 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosp))).nazwa;
            var team2 = JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosc))).nazwa;
            SetContentView(Resource.Layout.AddBetForUser);
            _buttonBack = FindViewById<Button>(Resource.Id.buttonBack);
            _buttonBack.Click += ButtonClick;
            _buttonaddBet = FindViewById<Button>(Resource.Id.buttonAddBet);
            _buttonaddBet.Click += ButtonClick;
            _textViewMecz = FindViewById<TextView>(Resource.Id.textViewMecz);
            _textViewMinuta = FindViewById<TextView>(Resource.Id.textViewMinuta);
            _textViewResult = FindViewById<TextView>(Resource.Id.textViewResult);
            _textViewKurs = FindViewById<TextView>(Resource.Id.textViewKurs);
            _spinnerTyp = FindViewById<Spinner>(Resource.Id.spinner1);

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.typ, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _spinnerTyp.Adapter = adapter;

            _textViewMecz.Text = "Mecz: "+ team1 + "-"+ team2;
            _textViewMinuta.Text = "Minuta: " + match.minuta;
            _textViewResult.Text = "Wynik: " + match.bramki_gosp + " : " + match.bramki_gosc;
            _textViewKurs.Text = "Kurs: " + match.kurs;
            
            

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

        public override void OnBackPressed()
        {
            ConstructStartPage();
        }
    }
}
