using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private QuestSO _questSO;
    public QuestSO QuestSO { get => _questSO; }
    private QuestData _questData;
    public QuestData QuestData { get => _questData; set => _questData = value; }

    private void Awake()
    {
        InitQuestData();
        gameObject.SetActive(false);
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
            _questSO.Days,
            _questSO.QuestHint
        );
    }
    public void ChangeQuestData(QuestSO questSO)
    {
        _questSO = questSO;
        InitQuestData();
    }
    private void SetQuestDataOnDate()
    {
        // TODO : 퀘스트 데이터 현재 날짜에 맞춰서 조정해주기
        // 날짜가 클 수록 보상과 패널티가 커지도록 - 알고리즘 개선 필요
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
        // 퀘스트 성공을 위한 전투력 기준으로 티어 자르는게 맞다고 생각하긴 하는데..
        // 더 좋은 방안은 없을까?
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
        // 퀘스트 성공을 위한 전투력에 의존해야 하는가?
        // 아니면 퀘스트의 내용에 의존해야 하는가? 
        // ex) 약초채집 퀘스트인데 날짜가 오래지났다고 죽어야 하는가?
        // ex) 용 처치 퀘스트 실패 시 죽는게 맞지 않는가?
    }
    private void SetQuestDays(ref int currentDate)
    {
        // Question
        // 동일 내용 퀘스트라도, 소요 시간을 바꿀 명분은 있다.
        // 초반 약초 채집 퀘스트는 뭐 흔한 약초를 가져오라는 걸 수 있고
        // 후반 약초 채집 퀘스트는 희귀한 약초를 가져오라는 걸 수 있다.
    }
}
