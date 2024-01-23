using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private GameObject rightBar;
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private List<GameObject> characterSprite;
    [SerializeField] private CharacterAppearance playerAppearance;
    private CharacterStats _characterStats;
    private int _characterSelect = 0;
    
    private void Start()
    {
        _characterStats = GameManager.Instance.player.GetComponent<CharacterStatsHandler>().CurrentStats;
        ShowLoginUI();
    }

    // 첫 로그인 화면
    private void ShowLoginUI()
    {
        characterPanel.SetActive(false);
        background.SetActive(true);
        GameManager.Instance.ChangePlayerState(false);
    }

    // 이름 변경창 show
    public void CharacterNameChanging()
    {
        namePanel.SetActive(true);
        GameManager.Instance.ChangePlayerState(false);
    }
    
    // 이름 변경 완료
    public void CharacterNameChanged()
    {
        var characterName = namePanel.GetComponentInChildren<TMP_InputField>().text;
        if(characterName.Length is < 2 or > 10)
            return;
        
        playerAppearance.SetCharacterName(characterName);
        UpdateNameUI(characterName);

        namePanel.GetComponentInChildren<TMP_InputField>().text = "";
        namePanel.SetActive(false);
        background.SetActive(false);
        GameManager.Instance.ChangePlayerState(true);
    }

    // 캐릭터 변경창 show
    public void CharacterChanging()
    {
        characterPanel.SetActive(true);
        GameManager.Instance.ChangePlayerState(false);
    }
    
    // 캐릭터 변경 완료
    public void CharacterChanged(int select)
    {
        if (_characterSelect == select)
        {
            characterPanel.SetActive(false);
            return;
        }

        characterSprite[_characterSelect].SetActive(false);
        _characterSelect = select;
        characterSprite[_characterSelect].SetActive(true);
        playerAppearance.SetCharacterSprite(_characterSelect);
        characterPanel.SetActive(false);
        if(!namePanel.activeSelf)
            GameManager.Instance.ChangePlayerState(true);
    }

    // 플레이어 목록
    public void ShowPlayerList()
    {
        rightBar.SetActive(!rightBar.activeSelf);
    }

    //TODO 멀티플레이어 만들 시, Action으로 Observer패턴을 만들어 구현하는 게 나아보임.
    // 일단 간단한 구현부터.
    private void UpdateNameUI(string characterName)
    {
        playerText.text = characterName;
        _characterStats.characterName = characterName;
    }
}
