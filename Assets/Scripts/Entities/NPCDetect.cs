using System.Collections;
using UnityEngine;

public class NPCDetect : MonoBehaviour
{
    // 감지 거리
    [SerializeField] public float detectionDistance = 5f;
    private LayerMask _npcLayerMask;
    private LayerMask _enemyLayerMask;
    private LayerMask _collisionLayerMask;
    private Transform _player;

    private float _shortestDistance;

    // 가장 가까운 객체
    private Collider2D _closestTarget;
    private Collider2D _latestTarget;

    private void Awake()
    {
        _closestTarget = null;
        _shortestDistance = float.MaxValue;
        _npcLayerMask = LayerMask.GetMask("NPC");
        _enemyLayerMask = LayerMask.GetMask("Enemy");
        _collisionLayerMask = _npcLayerMask | _enemyLayerMask;
    }

    public void Start()
    {
        _player = GameManager.Instance.player;
        StartCoroutine(CheckCharacter());
    }

    private IEnumerator CheckCharacter()
    {
        while (true)
        {
            // 부하 줄이기
            yield return new WaitForSeconds(0.2f);

            // detectionDistance안에 있는 물체 찾기.
            var collisions = Physics2D.OverlapCircleAll(_player.transform.position, detectionDistance, _collisionLayerMask);


            // 발견 없을시 처리;
            if (collisions.Length == 0)
            {
                // 이전에 대화한 npc가 있을 경우 창 닫기
                if (_latestTarget && _npcLayerMask.value == (_npcLayerMask.value | (1 << _latestTarget.gameObject.layer)))
                {
                    GameManager.Instance.dialogueSystem.CloseDialogue();
                    _latestTarget = null;
                }

                continue;
            }

            // 최단거리 물체 찾기
            for (var i = 0; i < collisions.Length; i++)
            {
                var targetDistance = Vector2.Distance(_player.position, collisions[i].transform.position);
                if (targetDistance < _shortestDistance)
                {
                    _shortestDistance = targetDistance;
                    _closestTarget = collisions[i];
                }
            }

            if (_latestTarget != null)
            {
                // 최근에 대화한 npc와 closestTarget이 같으면 continue
                if (ReferenceEquals(_latestTarget, _closestTarget))
                {
                    _shortestDistance = float.MaxValue;
                    continue;
                }

                // 최근에 대화한 npc와 closestTarget이 다르면 dialogue close
                if (_npcLayerMask.value == (_npcLayerMask.value | (1 << _latestTarget.gameObject.layer)))
                {
                    GameManager.Instance.dialogueSystem.CloseDialogue();
                }
            }

            _latestTarget = _closestTarget;
            _shortestDistance = float.MaxValue;

            // npc 처리
            if (_npcLayerMask.value == (_npcLayerMask.value | (1 << _closestTarget.gameObject.layer)))
            {
                var characterName = _closestTarget.GetComponent<CharacterStatsHandler>().CurrentStats.characterName;
                GameManager.Instance.dialogueSystem.CanTalkToIt(characterName);
            }
        }
    }
}
