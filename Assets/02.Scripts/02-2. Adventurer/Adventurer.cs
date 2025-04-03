using UnityEngine;

public class Adventurer : MonoBehaviour
{
    [SerializeField]
    private AdventurerSO _adventurerData;
    public AdventurerSO AdventurerData { get => _adventurerData; set => _adventurerData = value; }

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
