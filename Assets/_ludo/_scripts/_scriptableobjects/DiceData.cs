using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Data", menuName = "_scriptableobjects/_dicedata", order = 1)]
public class DiceData : ScriptableObject
{

    public List<_DiceSpriteContainer> m_dicedata;


}
[System.Serializable]
public class _DiceSpriteContainer
{
    public _MyColor m_id;
    public List<Sprite> m_dicesprites;
}
