using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private QuestSO _questData;
    public QuestSO QuestData { get => _questData; set => _questData = value; }

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
