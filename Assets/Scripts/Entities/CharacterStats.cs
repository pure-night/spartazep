using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public string characterName;
    [Range(0f, 20f)] public float movingSpeed;

    public StatSO statSO;
}
