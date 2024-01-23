using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    
    public CharacterStats CurrentStats { get; private set; }

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        StatSO statSO = null;
        if (baseStats.statSO != null)
        {
            statSO = Instantiate(baseStats.statSO);
        }
        
        CurrentStats = new CharacterStats
        {
            statSO = statSO,
            characterName = baseStats.characterName,
            movingSpeed = baseStats.movingSpeed
        };
    }
}
