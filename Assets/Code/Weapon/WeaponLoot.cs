using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GiveWeaponEvent))]
[RequireComponent(typeof(GiveWeapon))]
[DisallowMultipleComponent]
public class WeaponLoot : MonoBehaviour
{
    #region Weapon
    [Space(10)]
    [Header("Weapon")]
    #endregion
    #region Tooltip
    [Tooltip("Populate with weapon")]
    #endregion
    [SerializeField] private Weapon itemOnPedestal;

    #region LevitationDetails
    [Space(10)]
    [Header("Weapon")]
    #endregion
    [SerializeField] private GameObject loot;
    [SerializeField]private SoundEffectSO takeTheLootSoundEffect;
    [SerializeField] private float levitationSpeed;
    [SerializeField] private float levitationDistance;

    private SpriteRenderer _WeaponSpriteRenderer;
    private Vector2 _startPosition;
    private Coroutine _levitateCouratine;
    private bool _isReadyToGive;
    private GiveWeaponEvent _weaponEvent;
    private GiveWeapon _giveWeapon;
   

    public WeaponLoot(Weapon weapon)
    {
        Init(weapon);

    }
    private void Awake()
    {
        _weaponEvent = GetComponent<GiveWeaponEvent>();
        _giveWeapon = GetComponent<GiveWeapon>();
        _weaponEvent.OnGiveWeapon += PlaySoundEffect;
    }

    private void Start()
    {
        Init(itemOnPedestal);
    }

   

    private void Update()
    {
        if (itemOnPedestal == null) return;
        if (_isReadyToGive)
        {
            
            bool isButtonPressed = Input.GetKeyDown(KeyCode.E);
            if (isButtonPressed)
            {
                _weaponEvent.CallGiveWeaponEvent(itemOnPedestal);
                DisableThisPedestal();
            }
               



        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            OnZoneEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            OnZoneExit();
    }


    private void Init(Weapon weapon)
    {
        _startPosition = loot.transform.position;
        itemOnPedestal = weapon;
        _WeaponSpriteRenderer = loot.GetComponent<SpriteRenderer>();
        _WeaponSpriteRenderer.sprite =itemOnPedestal.WeaponDetails.Icon;
        _levitateCouratine = StartCoroutine(nameof(Levitate));
    }

    IEnumerator Levitate()
    {
        float upBorder = _startPosition.y + levitationDistance;
        float downBorder = _startPosition.y - levitationDistance;
        bool moveDown = true;
        while (true)
        {
            if (moveDown)
            {
                loot.transform.position = new Vector3(loot.transform.position.x, loot.transform.position.y - Time.deltaTime * levitationSpeed);
                if (loot.transform.position.y <= downBorder)
                    moveDown = false;
            }
            else
            {
                loot.transform.position = new Vector3(loot.transform.position.x, loot.transform.position.y + Time.deltaTime * levitationSpeed);
                if (loot.transform.position.y >= upBorder)
                    moveDown = true;
            }
            yield return null;
        }

    }

    private void OnZoneEnter()
    {
        _isReadyToGive = true;
    }
    private void OnZoneExit()
    {
        _isReadyToGive = false;
    }


    private void DisableThisPedestal()
    {
        itemOnPedestal = null;
        _WeaponSpriteRenderer.sprite = null;
        _weaponEvent.OnGiveWeapon -= PlaySoundEffect;
        Destroy(this);

    }


    private void PlaySoundEffect(GiveWeaponEvent giveWeapon, GiveWeaponEventArgs args)
    {
        if (takeTheLootSoundEffect!=null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(takeTheLootSoundEffect);

        }
    }



}
