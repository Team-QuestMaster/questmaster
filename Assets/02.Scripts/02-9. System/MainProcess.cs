using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProcess : MonoBehaviour
{
    private List<(Adventurer, Quest)> _todayRequest;

    private const int _requestCountMaxPerDay = 5; // 하루에 방문 가능한 최대 모험가의 수
    private int _requestCount = 0; // 현재 방문한 모험가의 수
    public event Action OnRequestCountIncreased;
    public event Action OnRequestMade;

    private void Start()
    {
        // TODO : UI에서 Approve 또는 Reject 상호작용을 했을  때,
        // UI 싱글톤에 존재하는 Action에 아래 메서드들을 동록한다.
        // UI매니저명.Instnace.OnApprove += ApproveRequest;
        // UI매니저명.Instnace.OnReject += RejectRequest 또는 EndRequest;
        // UI매니저명.Instance.On모험가퇴장Complete += EndRequest;
        // UI매니저명.Instance.OnViewQuestResultFinished += ApplyQuestResult;
        // UI매니저명.Instance.OnViewQuestResultFinished += GetRequest;
        OnRequestCountIncreased += 
            (() => DateManager.Instance.ChangeDate(_requestCount, _requestCountMaxPerDay));
        OnRequestCountIncreased += 
            (() => DateManager.Instance.ChangeTimePeriod(_requestCount, _requestCountMaxPerDay));
        DateManager.Instance.OnDateChanged +=
            (() => GetRequests());
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
        List<QuestResult> questResults = DateManager.Instance.GetTodayQuestResults();

        foreach (QuestResult questResult in questResults)
        {
            if (questResult.IsSuccess)
            {
                GuildStatManager.Instance.Fame += questResult.Quest.QuestData.FameReward;
                GuildStatManager.Instance.Gold += questResult.Quest.QuestData.GoldReward;
            }
            else
            {
                GuildStatManager.Instance.Fame -= questResult.Quest.QuestData.FamePenalty;
                GuildStatManager.Instance.Gold -= questResult.Quest.QuestData.GoldPenalty;
            }
        }
    }
    private void PickTodayRequests()
    {
        // 하루 최대 방문 수가 될 때 까지
        int requestCount = 0;
        while (requestCount < _requestCountMaxPerDay)
        {
            // (모험가, 퀘스트)를 뽑아온다
            (Adventurer, Quest) request = PickManager.Instance.Pick();
            if (!ReferenceEquals(request.Item1, null) && !ReferenceEquals(request.Item2, null))
            {
                _todayRequest.Add(request);
            }
            requestCount++;
        }
        foreach ((Adventurer, Quest) request in _todayRequest)
        {
            // 모험가는 Monobehaviour를 상속하고, Adventurer 스크립트가 부착된 GO가 곧 캐릭터의 GO이므로
            UIManager.Instance.CharacterUI.Characters.Add(request.Item1.gameObject);

            // 퀘스트 UI 쪽 로직이 완성되면, 위와 같이 추가해주면 될듯하다.
        }
    }
    public void ApproveRequest()
    {
        float probability = CalculateManager.Instance.CalculateProbability
            (_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2);
        bool isQuestSuccess = CalculateManager.Instance.JudgeQuestResult
            (_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2, probability);
        DateManager.Instance.AddQuestResultToList
            (_todayRequest[_requestCount].Item1, _todayRequest[_requestCount].Item2, isQuestSuccess, probability);

        // UI 싱글톤 스크립트에서 확률 보여주기 위해 메서드 호출 필요, 아래와 같은 형식으로
        // 확률 팝업 UI 스크립트.메서드명(probability);
    }
    public void EndRequest()
    {
        // 어처피 ChangeCharacter에서 해주고 있음(퀘스트 제외)
        //_todayRequest[_requestCount].Item1.gameObject.SetActive(false);
        //_todayRequest[_requestCount].Item2.gameObject.SetActive(false);
    }
}
