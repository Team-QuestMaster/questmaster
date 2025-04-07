using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProcess : MonoBehaviour
{
    private List<(Adventurer, Quest)> _todayRequest;

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
        // �Ϸ� �ִ� �湮 ���� �� �� ����
        int requestCount = 0;
        while (requestCount < _requestCountMaxPerDay)
        {
            // (���谡, ����Ʈ)�� �̾ƿ´�
            (Adventurer, Quest) request = PickManager.Instance.Pick();
            if (!ReferenceEquals(request.Item1, null) && !ReferenceEquals(request.Item2, null))
            {
                _todayRequest.Add(request);
            }
            requestCount++;
        }
        foreach ((Adventurer, Quest) request in _todayRequest)
        {
            // ���谡�� Monobehaviour�� ����ϰ�, Adventurer ��ũ��Ʈ�� ������ GO�� �� ĳ������ GO�̹Ƿ�
            UIManager.Instance.CharacterUI.Characters.Add(request.Item1.gameObject);

            // ����Ʈ UI �� ������ �ϼ��Ǹ�, ���� ���� �߰����ָ� �ɵ��ϴ�.
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

        // UI �̱��� ��ũ��Ʈ���� Ȯ�� �����ֱ� ���� �޼��� ȣ�� �ʿ�, �Ʒ��� ���� ��������
        // Ȯ�� �˾� UI ��ũ��Ʈ.�޼����(probability);
    }
    public void EndRequest()
    {
        // ��ó�� ChangeCharacter���� ���ְ� ����(����Ʈ ����)
        //_todayRequest[_requestCount].Item1.gameObject.SetActive(false);
        //_todayRequest[_requestCount].Item2.gameObject.SetActive(false);
    }
}
