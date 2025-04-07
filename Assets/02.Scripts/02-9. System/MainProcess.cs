using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MainProcess : MonoBehaviour
{
    private List<(Adventurer, Quest)> _todayRequest;

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
        GetRequests();
        StageShowManager.Instance.ShowCharacter.Appear();
        StageShowManager.Instance.ShowQuest.Appear();
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
        // ï¿½ï¿½ï¿?UI ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
        UIManager.Instance.ReportUI.GoldText(beforeGold, GuildStatManager.Instance.Gold);
        UIManager.Instance.ReportUI.FameText(beforeFame, GuildStatManager.Instance.Fame);
    }
    private void PickTodayRequests()
    {
        // ï¿½Ï·ï¿½ ï¿½Ö´ï¿½ ï¿½æ¹® ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        int requestCount = 0;
        while (requestCount < _requestCountMaxPerDay)
        {
            // (ï¿½ï¿½ï¿½è°¡, ï¿½ï¿½ï¿½ï¿½Æ®)ï¿½ï¿½ ï¿½Ì¾Æ¿Â´ï¿½
            (Adventurer, Quest) request = PickManager.Instance.Pick();
            if (!ReferenceEquals(request.Item1, null) && !ReferenceEquals(request.Item2, null))
            {
                _todayRequest.Add(request);
            }
            requestCount++;
        }
        foreach ((Adventurer, Quest) request in _todayRequest)
        {
            // ï¿½ï¿½ï¿½è°¡ï¿½ï¿½ Monobehaviourï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½Ï°ï¿? Adventurer ï¿½ï¿½Å©ï¿½ï¿½Æ®ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ GOï¿½ï¿½ ï¿½ï¿½ Ä³ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ GOï¿½Ì¹Ç·ï¿½
            UIManager.Instance.CharacterUI.Characters.Add(request.Item1.gameObject);

            // ï¿½ï¿½ï¿½ï¿½Æ® UI ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ï¼ï¿½ï¿½Ç¸ï¿½, ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ß°ï¿½ï¿½ï¿½ï¿½Ö¸ï¿½ ï¿½Éµï¿½ï¿½Ï´ï¿½.
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
        
        // UI ï¿½Ì±ï¿½ï¿½ï¿½ ï¿½ï¿½Å©ï¿½ï¿½Æ®ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ö±ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Þ¼ï¿½ï¿½ï¿½ È£ï¿½ï¿½ ï¿½Ê¿ï¿½, ï¿½Æ·ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
        // È®ï¿½ï¿½ ï¿½Ë¾ï¿½ UI ï¿½ï¿½Å©ï¿½ï¿½Æ®.ï¿½Þ¼ï¿½ï¿½ï¿½ï¿?probability);
        // Ä¶ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ç¥ï¿½ï¿½
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
        // ï¿½ï¿½Ã³ï¿½ï¿½ ChangeCharacterï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ö°ï¿½ ï¿½ï¿½ï¿½ï¿½(ï¿½ï¿½ï¿½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½)
        //_todayRequest[_requestCount].Item1.gameObject.SetActive(false);
        //_todayRequest[_requestCount].Item2.gameObject.SetActive(false);
    }
}
