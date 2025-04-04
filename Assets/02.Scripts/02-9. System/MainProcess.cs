using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProcess : MonoBehaviour
{
    private (Adventurer, Quest) _currentRequest;


    private const int _requestCountMaxPerDay = 5; // �Ϸ翡 �湮 ������ �ִ� ���谡�� ��
    private int _requestCount = 0; // ���� �湮�� ���谡�� ��
    public event Action OnRequestCountIncreased;
    public event Action OnRequestMade;

    private void Start()
    {
        // TODO : UI���� Approve �Ǵ� Reject ��ȣ�ۿ��� ����  ��,
        // UI �̱��濡 �����ϴ� Action�� �Ʒ� �޼������ �����Ѵ�.
        // UI�Ŵ�����.Instnace.OnApprove += ApproveRequest;
        // UI�Ŵ�����.Instnace.OnReject += RejectRequest �Ǵ� EndRequest;
        // UI�Ŵ�����.Instance.On���谡����Complete += EndRequest;
        // UI�Ŵ�����.Instance.OnViewQuestResultFinished += ApplyQuestResult;
        // UI�Ŵ�����.Instance.OnViewQuestResultFinished += GetRequest;
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
        // �Ϸ� �ִ� �湮 ���� �� �� ����
        while (_requestCount < _requestCountMaxPerDay)
        {
            yield return new WaitUntil
                (() => ReferenceEquals(_currentRequest.Item1, null)
                || ReferenceEquals(_currentRequest.Item2, null));
            // ���� ���谡 �Ǵ� ����Ʈ�� null�� �� ���� ���

            // (���谡, ����Ʈ)�� �̾ƿ� ��, Ȱ��ȭ���ش�.
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

        // UI �̱��� ��ũ��Ʈ���� Ȯ�� �����ֱ� ���� �޼��� ȣ�� �ʿ�, �Ʒ��� ���� ��������
        // Ȯ�� �˾� UI ��ũ��Ʈ.�޼����(probability);
        EndRequest();
    }
    public void EndRequest()
    {
        _currentRequest.Item1.gameObject.SetActive(false);
        _currentRequest.Item2.gameObject.SetActive(false);
        _currentRequest = (null, null);
    }
}
