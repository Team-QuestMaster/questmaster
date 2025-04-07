using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MainProcess : MonoBehaviour
{
    private List<(Adventurer, Quest)> _todayRequest = new List<(Adventurer, Quest)>();

    private const int _requestCountMaxPerDay = 5;
    private int _requestCount = 0; 
    public event Action OnRequestCountIncreased;
    public event Action OnRequestMade;

    private void Start()
    {
        OnRequestCountIncreased += 
            (() => DateManager.Instance.ChangeDate(_requestCount, _requestCountMaxPerDay));
        OnRequestCountIncreased += 
            (() => DateManager.Instance.ChangeTimePeriod(_requestCount, _requestCountMaxPerDay));
        DateManager.Instance.OnDateChanged +=
            (() => GetRequests());
        DateManager.Instance.OnDateChanged +=
            (() => ApplyQuestResult());
        UIManager.Instance.StampUI.UIApproveEvent +=
            (() => ApproveRequest());
        
        GetRequests();
    }
    private void GetRequests()
    {
        InitVisitedAdventureCount();
        PickTodayRequests();
    }
    private void InitVisitedAdventureCount()
    {
        _requestCount = 0;
    }
    private void ApplyQuestResult()
    {
        UIManager.Instance.ReportUI.TextClear();
        List<QuestResult> questResults = DateManager.Instance.GetTodayQuestResults();

        // UI data
        int beforeGold = GuildStatManager.Instance.Gold;
        int beforeFame = GuildStatManager.Instance.Fame;
        foreach (QuestResult questResult in questResults)
        {
            if (questResult.IsSuccess)
            {
                GuildStatManager.Instance.Fame += questResult.Quest.QuestData.FameReward;
                GuildStatManager.Instance.Gold += questResult.Quest.QuestData.GoldReward;
                UIManager.Instance.ReportUI.QuestResultTextAdd($"ï¿½ï¿½ï¿½ï¿½: {questResult.Quest.QuestData.QuestName} {questResult.Probability}");
                UIManager.Instance.ReportUI.SpecialCommentText($"ï¿½ï¿½ï¿½ï¿½: {questResult.Quest.QuestData.QuestName}: {questResult.Quest.QuestData.QuestHint}");
            }
            else
            {
                GuildStatManager.Instance.Fame -= questResult.Quest.QuestData.FamePenalty;
                GuildStatManager.Instance.Gold -= questResult.Quest.QuestData.GoldPenalty;
                UIManager.Instance.ReportUI.QuestResultTextAdd($"ï¿½ï¿½ï¿½ï¿½: {questResult.Quest.QuestData.QuestName} {questResult.Probability}");
                UIManager.Instance.ReportUI.SpecialCommentText($"ï¿½ï¿½ï¿½ï¿½: {questResult.Quest.QuestData.QuestName}: {questResult.Quest.QuestData.QuestHint}");
            }
        }
        UIManager.Instance.ReportUI.GoldText(beforeGold, GuildStatManager.Instance.Gold);
        UIManager.Instance.ReportUI.FameText(beforeFame, GuildStatManager.Instance.Fame);
    }
    private void PickTodayRequests()
    {
        int requestCount = 0;
        while (requestCount < _requestCountMaxPerDay)
        {
            (Adventurer, Quest) request = PickManager.Instance.Pick();
            if (!ReferenceEquals(request.Item1, null) && !ReferenceEquals(request.Item2, null))
            {
                _todayRequest.Add(request);
            }
            requestCount++;
        }
        foreach ((Adventurer, Quest) request in _todayRequest)
        {
            UIManager.Instance.CharacterUI.Characters.Add(request.Item1.gameObject);

            UIManager.Instance.QuestUI.Quests.Add(request.Item2);
        }
        StageShowManager.Instance.ShowResult.Initialize(_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2);
    }
    public void ApproveRequest()
    {
        //¼ö¶ô ½Ã Äù½ºÆ® À§¿¡ ÀÖ´Â ¾ÆÀÌÅÛ »ç¿ë
        foreach (Item item in ItemManager.Instance.HavingItemList)
        {
            if (item.ItemState == ItemStateType.ReadyToUse)
            {
                item.ItemState = ItemStateType.UnBuy;
                item.Use(_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2);
            }
        }

        float probability = CalculateManager.Instance.CalculateProbability
            (_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2);
        bool isQuestSuccess = CalculateManager.Instance.JudgeQuestResult
            (_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2, probability);
        DateManager.Instance.AddQuestResultToList
            (_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2, isQuestSuccess, probability);
        
        int questEndDay = DateManager.Instance.CurrentDate + _todayRequest[_requestCount].Item2.QuestData.Days;
        string questCalenderInfoText = $"{_todayRequest[_requestCount].Item2.QuestData.QuestName} <color=green>{isQuestSuccess}</color>";
        UIManager.Instance.CalenderManager.AddCalenderText(questEndDay, questCalenderInfoText);

        EndRequest();
    }
    public void EndRequest()
    {
        StageShowManager.Instance.ShowResult.Initialize(_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2);
        _requestCount++;
        OnRequestCountIncreased?.Invoke();
        //_todayRequest[_requestCount].Item1.gameObject.SetActive(false);
        //_todayRequest[_requestCount].Item2.gameObject.SetActive(false);
    }
}
