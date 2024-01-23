using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    // 캐릭터 선택용 sprites
    [SerializeField] private List<Sprite> characterSprites;
    // 각 캐릭터의 animator controller
    [SerializeField] private List<AnimatorController> animatorControllers;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private GameObject characterMainSprite;
    
    private CharacterStats _currentStats;

    private void Start()
    {
        _currentStats = GetComponent<CharacterStatsHandler>().CurrentStats;
    }

    public void SetCharacterName(string characterName)
    {
        _currentStats.characterName = characterName;
        characterNameText.text = characterName;
    }

    // character 변경시 sprite와 animator만 변경해줌
    //TODO 다른 방식 찾아보기
    public void SetCharacterSprite(int select)
    {
        characterMainSprite.GetComponent<SpriteRenderer>().sprite = characterSprites[select];
        characterMainSprite.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[select];
        if (select == 0)
        {
            characterMainSprite.GetComponent<Transform>().localPosition = new Vector3(0f, 0.5f, 0f);
            characterMainSprite.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else if (select == 1)
        {
            characterMainSprite.GetComponent<Transform>().localPosition = new Vector3(0f, 0.25f, 0f);
            characterMainSprite.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
