using UnityEngine;

public class MinorAdventurerStatHandler : MonoBehaviour
{
    public void SetRandomStat(Adventurer adventurer, int currentDate)
    {
        AdventurerData data = adventurer.AdventurerData;
        AdventurerSO adventurerSO = adventurer.AdventurerSO;

        float randSTR = Random.Range(0f, 4f);
        float randMAG = Random.Range(0f, 4f - randSTR);
        float randINS = Random.Range(0f, 4f - randSTR-randMAG);
        float randDEX = 4f - randSTR - randMAG - randINS;
        data.CurrentSTR = (int)(adventurerSO.OriginalSTR * (1 + currentDate / 2) * randSTR);
        data.CurrentMAG = (int)(adventurerSO.OriginalMAG * (1 + currentDate / 2) * randMAG);
        data.CurrentINS = (int)(adventurerSO.OriginalINS * (1 + currentDate / 2) * randINS);
        data.CurrentDEX = (int)(adventurerSO.OriginalDEX * (1 + currentDate / 2) * randDEX);

    }
}
