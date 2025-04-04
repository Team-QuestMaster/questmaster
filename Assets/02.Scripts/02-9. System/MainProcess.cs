using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProcess : MonoBehaviour
{
    private (Adventurer, Quest) _currentRequest;


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

    }
    private void GetRequest()
    {
        InitVisitedAdventureCount();
        StartCoroutine(GetRequestCoroutine());
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
    private IEnumerator GetRequestCoroutine()
    {
        // 하루 최대 방문 수가 될 때 까지
        while (_requestCount < _requestCountMaxPerDay)
        {
            yield return new WaitUntil
                (() => ReferenceEquals(_currentRequest.Item1, null)
                || ReferenceEquals(_currentRequest.Item2, null));
            // 현재 모험가 또는 퀘스트가 null일 때 까지 대기

            // (모험가, 퀘스트)를 뽑아온 후, 활성화해준다.
            _currentRequest = PickManager.Instance.Pick();
            if (!ReferenceEquals(_currentRequest.Item1, null) && !ReferenceEquals(_currentRequest.Item2, null))
            {
                _currentRequest.Item1.gameObject.SetActive(true);
                _currentRequest.Item2.gameObject.SetActive(true);
            }
            _requestCount++;
            OnRequestCountIncreased?.Invoke();
        }
        yield return null;
    }
    public void ApproveRequest()
    {
        float probability = CalculateManager.Instance.CalculateProbability(_currentRequest.Item1, _currentRequest.Item2);
        bool isQuestSuccess = CalculateManager.Instance.JudgeQuestResult(_currentRequest.Item1, _currentRequest.Item2, probability);
        DateManager.Instance.AddQuestResultToList(_currentRequest.Item1, _currentRequest.Item2, isQuestSuccess, probability);

        // UI 싱글톤 스크립트에서 확률 보여주기 위해 메서드 호출 필요, 아래와 같은 형식으로
        // 확률 팝업 UI 스크립트.메서드명(probability);
        EndRequest();
    }
    public void EndRequest()
    {
        _currentRequest.Item1.gameObject.SetActive(false);
        _currentRequest.Item2.gameObject.SetActive(false);
        _currentRequest = (null, null);
    }
}
