using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AdventurerDataPool : MonoBehaviour
{
    public List<string> AdventurerNameList= new List<string>();
    public List<string> AdventurerClassList = new List<string>();
    public List<string> AdventurerTitleList = new List<string>();
    public int OriginalSTR;
    public int OriginalMAG;
    public int OriginalINS;
    public int OriginalDEX;
    public List<DialogSet> DialogList = new List<DialogSet>();
    public List<Sprite> AdventurerSpriteLDList = new List<Sprite>();
}
