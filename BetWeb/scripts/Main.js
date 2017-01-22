function GetBetShow()
{
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