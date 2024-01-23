using System.Collections.Generic;
using UnityEngine;

// Dialogue Scriptable Object
[CreateAssetMenu(fileName = "DefaultDialogueData", menuName = "Dialogue/Default", order = 0)]
public class DialogueSO : ScriptableObject
{
    [Header("Actor")]
    public string characterName;

    [Header("Dialogue")]
    public List<string> dialogueScript;
}
