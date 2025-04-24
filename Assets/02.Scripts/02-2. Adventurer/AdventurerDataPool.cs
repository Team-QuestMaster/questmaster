using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AdventurerDataPool : MonoBehaviour
{
    public List<string> AdventurerNameList= new List<string>();
    public List<string> AdventurerClassList = new List<string>();
    public List<string> AdventurerTitleList = new List<string>();
    public int OrignSTR;
    public int OrignMAG;
    public int OrignINS;
    public int OrignDEX;
    public List<DialogSet> DialogList = new List<DialogSet>();
    public List<Sprite> AdventurerSpriteLDList = new List<Sprite>();
}
