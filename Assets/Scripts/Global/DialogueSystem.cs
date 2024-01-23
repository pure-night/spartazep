using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject canTalkPanel;
    [SerializeField] private TMP_Text canTalkText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject nextDialogueBtn;

    // dictionary 값 받기. Serializable Dictionary만드려다가 실패함.
    [SerializeField] private List<string> keys;
    [SerializeField] private List<DialogueSO> values;

    // dialogue가 저장될 dictionary key : character name, value : dialogue Scriptable object
    private Dictionary<string, DialogueSO> _dialogueDictionary;

    private string _currentNpcName;
    private List<string> _dialogueLines;
    private int _currentDialogue;
    
    private void Awake()
    {
        _dialogueDictionary = new Dictionary<string, DialogueSO>();
        _dialogueLines = new List<string>();
        _currentDialogue = 0;
        
        // key 갯수와 value 갯수가 다를 경우 최솟값으로.
        var count = Mathf.Min(keys.Count, values.Count);

        for (var i = 0; i < count; i++)
        {
            _dialogueDictionary.Add(keys[i], values[i]);
        }
    }

    private void Start()
    {
        canTalkPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    // 대화 시작하기 창
    public void CanTalkToIt(string characterName)
    {
        // key부터 체크
        if(!_dialogueDictionary.ContainsKey(characterName))
            return;
        
        canTalkText.text = characterName;
        _currentNpcName = characterName;
        canTalkPanel.SetActive(true);
    }

    public void DoDialogue()
    {
        // TryGetValue로 예외처리
        if (!_dialogueDictionary.TryGetValue(_currentNpcName, out var dialogueSO))
            return;
        
        canTalkPanel.SetActive(false);
        
        // _dialogueLines에 dialogue string 저장
        _dialogueLines = dialogueSO.dialogueScript;
        _currentDialogue = 0;
        
        // 저장된 dialogue의 string 수가 0일 경우 예외처리
        if(_dialogueLines.Count == 0)
            return;
        
        dialogueText.text = _dialogueLines[_currentDialogue];
        nextDialogueBtn.SetActive(true);
        dialoguePanel.SetActive(true);
    }

    // 다음 dialogue line 출력용 - button
    public void ShowNextDialogue()
    {
        // 길이 체크
        if (_dialogueLines.Count - 1 <= _currentDialogue)
            return;

        _currentDialogue++;
        dialogueText.text = _dialogueLines[_currentDialogue];
        
        // 마지막 dialogue line의 경우 버튼 비활성화
        if(_dialogueLines.Count - 1 <= _currentDialogue)
            nextDialogueBtn.SetActive(false);
    }

    // dialogue close용
    public void CloseDialogue()
    {
        canTalkPanel.SetActive(false);
        dialoguePanel.SetActive(false);
        _currentDialogue = 0;
    }
}
