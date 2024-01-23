using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private TMP_Text timeText;
    public Transform player;

    public DialogueSystem dialogueSystem { get; private set; }

    private void Awake()
    {
        Instance = this;
        dialogueSystem = GetComponentInChildren<DialogueSystem>();
    }

    private void Start()
    {
        StartCoroutine(UpdateRealTimeClock());
    }

    // 플레이어 조작 불가능, 가능 변환용
    public void ChangePlayerState(bool state)
    {
        playerInput.enabled = state;
    }

    // 코루틴으로 구현한 시계
    private IEnumerator UpdateRealTimeClock()
    {
        while (true)
        {
            var today = DateTime.Now.ToString("HH:mm:ss");
            timeText.text = today;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
