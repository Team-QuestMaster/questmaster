using UnityEngine;

public class QuestPresenter : MonoBehaviour
{
    private QuestModel _questModel;
    private UI_QuestView _questView;

    private void Awake()
    {
        _questView = GetComponent<UI_QuestView>();
        _questModel = GetComponent<QuestModel>();
    }

    private void Start()
    {
        if (!ReferenceEquals(_questModel, null))
        {
            _questModel.OnQuestDataChanged += _questView.RefreshUI;
        }
    }
}
