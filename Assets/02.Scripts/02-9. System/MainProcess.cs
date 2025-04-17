using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MainProcess : MonoBehaviour
{
    private List<(Adventurer, QuestData)> _todayRequest = new List<(Adventurer, QuestData)>();

    private const int _requestCountMaxPerDay = 5;
    private int _requestCount = 0;
    public event Action OnRequestCountIncreased;
    public event Action OnRequestMade;

    private void Start()
    {
        OnRequestCountIncreased +=
            (() => DateManager.Instance.ChangeDate(_requestCount, _requestCountMaxPerDay));
        DateManager.Instance.OnDateChanged += (() => GetRequests());
        DateManager.Instance.OnDateChanged += (() => ApplyQuestResult());
        UIManager.Instance.StampUI.UIApproveEvent += (() => ApproveRequest());
        UIManager.Instance.StampUI.UIRejectEvent += (() => RejectRequest());
        GetRequests();
        StageShowManager.Instance.AppearEventSet();
        // 처음 게임 시작
        UIManager.Instance.OneCycleStartAndEnd.StartCycle();
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
    private void PickTodayRequests()
    {
        UIManager.Instance.CharacterUI.Characters.Clear();
        UIManager.Instance.QuestUI.QuestDatas.Clear();
        _todayRequest.Clear();

        int requestCount = 0;
        while (requestCount < _requestCountMaxPerDay)
        {
            (Adventurer, QuestData) request = PickManager.Instance.Pick();
            if (!ReferenceEquals(request.Item1, null) && !ReferenceEquals(request.Item2, null))
            {
                // request.Item1.AdventurerData.AdventurerState = AdventurerStateType.TodayCome;
                // request.Item2.IsQuesting = true;
                _todayRequest.Add(request);
            }
            requestCount++;
        }
        foreach ((Adventurer, QuestData) request in _todayRequest)
        {
            UIManager.Instance.CharacterUI.Characters.Add(request.Item1.gameObject);

            UIManager.Instance.QuestUI.QuestDatas.Add(request.Item2);
        }
        StageShowManager.Instance.ShowResult.Initialize
            (_todayRequest[_requestCount].Item1, UIManager.Instance.QuestUI.CurrentQuest);
    }
    public void ApproveRequest()
    {
        StageShowManager.Instance.ShowResult.Initialize
            (_todayRequest[_requestCount].Item1, UIManager.Instance.QuestUI.CurrentQuest);
        Adventurer currentAdventurer = _todayRequest[_requestCount].Item1;
        Quest currentQuest = UIManager.Instance.QuestUI.CurrentQuest;
        ItemManager.Instance.StatItemUse(currentAdventurer, currentQuest);
        MakeQuestResult(currentAdventurer, currentQuest);

        // currentAdventurer.AdventurerData.AdventurerState = AdventurerStateType.Questing;
        EndRequest();
    }
    private bool MakeQuestResult(Adventurer adventurer, Quest quest)
    {
        float probability = CalculateManager.Instance.CalculateProbability(adventurer, quest);
        float itemProbability = ItemManager.Instance.QuestItemUse(adventurer, quest);
        probability = Mathf.Clamp(probability + itemProbability, 0f, 100f);
        bool isQuestSuccess = CalculateManager.Instance.JudgeQuestResult(adventurer, quest, probability);
        DateManager.Instance.AddQuestResultToList(adventurer, quest, isQuestSuccess, probability);
        StageShowManager.Instance.ShowResult.ResultText(true, probability,quest.QuestData.GoldReward,quest.QuestData.GoldPenalty);
        UpdateCalender(quest, probability);
        return isQuestSuccess;
    }
    private void UpdateCalender(Quest quest, float isQuestSuccess)
    {
        int questEndDay = DateManager.Instance.CurrentDate + quest.QuestData.Days;
        string questCalenderInfoText = $"{quest.QuestData.QuestName} <color=#99A136>{isQuestSuccess:N1}</color>";
        UIManager.Instance.CalenderManager.AddCalenderText(questEndDay, questCalenderInfoText);
    }
    public void RejectRequest()
    {
        StageShowManager.Instance.ShowResult.Initialize
            (_todayRequest[_requestCount].Item1, UIManager.Instance.QuestUI.CurrentQuest);
        Adventurer currentAdventurer = _todayRequest[_requestCount].Item1;
        Quest currentQuest = UIManager.Instance.QuestUI.CurrentQuest;
        float probability = CalculateManager.Instance.CalculateProbability(currentAdventurer, currentQuest);
        StageShowManager.Instance.ShowResult.ResultText(false, probability, currentQuest.QuestData.GoldReward, currentQuest.QuestData.GoldPenalty);
        _todayRequest[_requestCount].Item1.AdventurerData.AdventurerState = AdventurerStateType.Idle;
        _todayRequest[_requestCount].Item2.IsQuesting = false;
        EndRequest();
    }
    public void EndRequest()
    {
        _requestCount++;
        OnRequestCountIncreased?.Invoke();
        //_todayRequest[_requestCount].Item1.gameObject.SetActive(false);
        //_todayRequest[_requestCount].Item2.gameObject.SetActive(false);
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
                GuildStatManager.Instance.Fame += questResult.QuestData.FameReward;
                GuildStatManager.Instance.Gold += questResult.QuestData.GoldReward;
                GuildStatManager.Instance.NumOfCompletedQuests++;
                UIManager.Instance.ReportUI.QuestResultTextAdd($"<color=#99A136>성공 :</color> {questResult.QuestData.QuestName} | {questResult.Probability.ToString("N1")}%");
               // UIManager.Instance.ReportUI.SpecialCommentText($"성공: {questResult.QuestData.QuestName}: {questResult.QuestData.QuestHint}");
            }
            else
            {
                GuildStatManager.Instance.Fame -= questResult.QuestData.FamePenalty;
                GuildStatManager.Instance.Gold -= questResult.QuestData.GoldPenalty;
                UIManager.Instance.ReportUI.QuestResultTextAdd($"<color=#C4402E>실패 :</color> {questResult.QuestData.QuestName} | {questResult.Probability.ToString("N1")}%");
                UIManager.Instance.ReportUI.SpecialCommentText($"{questResult.QuestData.QuestName} : {questResult.QuestData.QuestHint}");
            }
        }
        UIManager.Instance.ReportUI.GoldText(beforeGold, GuildStatManager.Instance.Gold);
        UIManager.Instance.ReportUI.FameText(beforeFame, GuildStatManager.Instance.Fame);
    }
}
