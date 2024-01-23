using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStatData", menuName = "Controller/Stat/Default", order = 0)]
public class StatSO : ScriptableObject
{
    [Header("Character Info")]
    public string characterName;
    public float movingSpeed;
}
