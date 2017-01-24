function GetBetShow(obj)
{
    $('#BetMatchId')[0].value = obj.id;
    $('.BetContener').show();
}
function GetBetHide()
{
    $('.BetContener').hide();
}

function GetMoneyShow() {
    $('.MoneySetDivs').hide()
    $('.MoneyGetDivs').show()
    $('.MoneyContener').show();
}
function SetMoneyShow() {
    $('.MoneyGetDivs').hide()
    $('.MoneySetDivs').show()
    $('.MoneyContener').show();
}
function GetMoneyHide() {
    $('.MoneyContener').hide();
}
function GetMatchesForLeague(obj)
{
    var leagueId = $('#LeaguesSelect')[0].value;
    var time = $('#TimeSelect')[0].value;
    var href = $('#LeaguesSelectBtn').attr('href').substring(0, $('#LeaguesSelectBtn').attr('href').indexOf('?') + 1);
    $('#LeaguesSelectBtn').attr('href', href + 'SelectedLegueId=' + leagueId + "&SelectedTime=" + time);
   // $('#LeaguesSelectBtn').attr('href', $('#LeaguesSelectBtn').attr('href').substring( 0,$('#LeaguesSelectBtn').attr('href').indexOf('SelectedLegueId')+16) + obj.value);
    $('#LeaguesSelectBtn')[0].click();
}
function SendMoney(obj) {

    var money = $('#MoneyInput')[0].value;
    var href = $('#MoneySendBtn').attr('href').substring(0, $('#MoneySendBtn').attr('href').indexOf('?') + 1);
    $('#MoneySendBtn').attr('href', href + 'money=' + money);
    // $('#LeaguesSelectBtn').attr('href', $('#LeaguesSelectBtn').attr('href').substring( 0,$('#LeaguesSelectBtn').attr('href').indexOf('SelectedLegueId')+16) + obj.value);
    $('#MoneySendBtn')[0].click();
}
function SetMoney(obj) {
    
    var money = $('#MoneyInput')[0].value;
    var href = $('#MoneySetBtn').attr('href').substring(0, $('#MoneySetBtn').attr('href').indexOf('?') + 1);
    $('#MoneySetBtn').attr('href', href + 'money=' + money);
    // $('#LeaguesSelectBtn').attr('href', $('#LeaguesSelectBtn').attr('href').substring( 0,$('#LeaguesSelectBtn').attr('href').indexOf('SelectedLegueId')+16) + obj.value);
    $('#MoneySetBtn')[0].click();
}

function AddBet(obj) {

    var money = $('#MoneyToBet')[0].value;
    var matchid = $('#BetMatchId')[0].value;
    var typ = $('#SelectedTypId')[0].value;
    var href = $('#AddBetBtn').attr('href').substring(0, $('#AddBetBtn').attr('href').indexOf('?') + 1);
    $('#AddBetBtn').attr('href', href + 'money=' + money + "&matchid=" + matchid + "&typ=" + typ);
    // $('#LeaguesSelectBtn').attr('href', $('#LeaguesSelectBtn').attr('href').substring( 0,$('#LeaguesSelectBtn').attr('href').indexOf('SelectedLegueId')+16) + obj.value);
    $('#AddBetBtn')[0].click();
}