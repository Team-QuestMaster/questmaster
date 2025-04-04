using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private QuestSO _questSO;
    private QuestData _questData;
    public QuestData QuestData { get => _questData; set => _questData = value; }

    private void Awake()
    {
        InitQuestData();
    }
    private void OnEnable()
    {
        SetQuestDataOnDate();
    }
    private void InitQuestData()
    {
        _questData = new QuestData(
            _questSO.QuestName,
            _questSO.QuestDescription,
            _questSO.QuestTier,
            _questSO.STRWeight,
            _questSO.MAGWeight,
            _questSO.INSWeight,
            _questSO.DEXWeight,
            _questSO.PowerForClear,
            _questSO.FameReward,
            _questSO.GoldReward,
            _questSO.FamePenalty,
            _questSO.GoldPenalty,
            _questSO.StateAfterFail,
            _questSO.Days
        );
    }
    private void SetQuestDataOnDate()
    {
        // TODO : ����Ʈ ������ ���� ��¥�� ���缭 �������ֱ�
        // ��¥�� Ŭ ���� ����� �г�Ƽ�� Ŀ������ - �˰��� ���� �ʿ�
        int currentDate = DateManager.Instance.CurrentDate;

        SetQuestPowerForClearOnDate(ref currentDate);
        SetQuestTierOnDate(ref currentDate);
        SetQuestRewardOnDate(ref currentDate);
        SetQuestPenaltyOnDate(ref currentDate);
        SetQuestStateAfterFailOnDate(ref currentDate);
        SetQuestDays(ref currentDate);
    }
    
    private void SetQuestPowerForClearOnDate(ref int currentDate)
    {
        _questData.PowerForClear += currentDate * _questSO.PowerForClear;
    }
    private void SetQuestTierOnDate(ref int currentDate)
    {
        // Question
        // ����Ʈ ������ ���� ������ �������� Ƽ�� �ڸ��°� �´ٰ� �����ϱ� �ϴµ�..
        // �� ���� ����� ������?
        float powerForClear = _questData.PowerForClear;
        if (powerForClear < 1000)
        {
            _questData.QuestTier = QuestTierType.Green;
        }
        else if (powerForClear < 5000)
        {
            _questData.QuestTier = QuestTierType.Blue;
        }
        else if (powerForClear < 10000)
        {
            _questData.QuestTier = QuestTierType.Yellow;
        }
        else if (powerForClear < 20000)
        {
            _questData.QuestTier = QuestTierType.Orange;
        }
        else
        {
            _questData.QuestTier = QuestTierType.Red;
        }
    }
    private void SetQuestRewardOnDate(ref int currentDate)
    {
        _questData.FameReward += currentDate * _questSO.FameReward;
        _questData.GoldReward += currentDate * _questSO.GoldReward;
    }
    private void SetQuestPenaltyOnDate(ref int currentDate)
    {
        _questData.FamePenalty += currentDate * _questSO.FamePenalty;
        _questData.GoldPenalty += currentDate * _questSO.GoldPenalty;
    }
    private void SetQuestStateAfterFailOnDate(ref int currentDate)
    {
        // Question
        // ����Ʈ ������ ���� �����¿� �����ؾ� �ϴ°�?
        // �ƴϸ� ����Ʈ�� ���뿡 �����ؾ� �ϴ°�? 
        // ex) ����ä�� ����Ʈ�ε� ��¥�� ���������ٰ� �׾�� �ϴ°�?
        // ex) �� óġ ����Ʈ ���� �� �״°� ���� �ʴ°�?
    }
    private void SetQuestDays(ref int currentDate)
    {
        // Question
        // ���� ���� ����Ʈ��, �ҿ� �ð��� �ٲ� ����� �ִ�.
        // �ʹ� ���� ä�� ����Ʈ�� �� ���� ���ʸ� ��������� �� �� �ְ�
        // �Ĺ� ���� ä�� ����Ʈ�� ����� ���ʸ� ��������� �� �� �ִ�.
    }
}
