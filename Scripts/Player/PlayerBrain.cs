using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    private static PlayerBrain _instance;
    public static PlayerBrain Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerBrain>();

            return _instance;
        }
    }

    private PlayerMovement _movement;
    private PlayerChars _chars;
    private BackGroundMusic _backGroundMusic;

    private List<Skill> _skills = new List<Skill>();

    private Radar _radar;
    private RocketArm _arm;

    [SerializeField]
    private QTEManager _qte;
    [SerializeField]
    private Collectable _collectableItem = null;

    private bool _canGetDamage;

    private bool _touchingEnemy;
    private EnemyBrain _enemyTouching;

    private bool _activatedDodgeAttack;

    // Start is called before the first frame update
    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _chars = GetComponent<PlayerChars>();

        _activatedDodgeAttack = false;
        _canGetDamage = true;

        _skills.AddRange(GetComponents<Skill>());
        
        _radar = GetComponent<Radar>();
        _arm = GetComponent<RocketArm>();
    }

    // Update is called once per frame
    private void Update()
    {
        _movement.TakeInputAndMove(OnBecameInvincible, OnBecameVulnerable);

        QTEInteraction();

        if (Input.GetMouseButtonDown(1))
        {
            _arm.Activate();
        }

        else if (Input.GetMouseButtonDown(2))
        {
            _radar.Activate();
        }


    }

    private void QTEInteraction()
    {
        if (_touchingEnemy && Input.GetMouseButtonDown(0) && !_activatedDodgeAttack && !_canGetDamage)
        {
            Time.timeScale = 0.01f;
            _activatedDodgeAttack = true;
            _qte.ActivateQTE();
            // Call QTE window
            StartCoroutine(ResetDodgeAttack(4.0f));
        }

        else if (Input.GetMouseButtonDown(0) && _activatedDodgeAttack)
        {
            bool hitTarget = _qte.Click();

            if (_enemyTouching)
            {
                if (hitTarget)
                {
                    _enemyTouching.Die();
                }

                else
                {
                    _chars.HP -= 10;
                }

                _qte.DeactivateQTE();

                StopAllCoroutines();
                StartCoroutine(ResetDodgeAttack(0.0f));
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        if (_canGetDamage)
            _chars.HP -= damage;
    }

    private void OnBecameInvincible()
    {
        _canGetDamage = false;
    }

    private void OnBecameVulnerable()
    {
        _canGetDamage = true;
        _qte.DeactivateQTE();
        _activatedDodgeAttack = false;

        _enemyTouching = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _touchingEnemy = collision.CompareTag("Enemy");
        if (_touchingEnemy)
        {
            _enemyTouching = collision.GetComponent<EnemyBrain>();
            return;
        }

        if (collision.GetComponent<Collectable>() && Input.GetKeyDown(KeyCode.F))
        {
            var hpRate = collision.GetComponent<Collectable>().AttachedItem;
            _chars.AddEffect(hpRate.HpRange, hpRate.Duration);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _touchingEnemy = false;
    }

    private IEnumerator ResetDodgeAttack(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
