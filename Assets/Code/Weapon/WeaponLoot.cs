using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GiveWeaponEvent))]
[RequireComponent(typeof(GiveArthefactEvent))]
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
    [SerializeField] private Weapon WeaponOnPedestal;
    #region Arthefact
    [Space(10)]
    [Header("Arthefact")]
    #endregion
    #region Tooltip
    [Tooltip("Populate with Arthefact")]
    #endregion
    [SerializeField] private Arthefact arthefactOnPedestal;



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
    private GiveArthefactEvent _arthefactEvent;
    private GiveWeapon _giveWeapon;

    public Arthefact ArthefactOnPedestal { get => arthefactOnPedestal; set => arthefactOnPedestal = value; }

    public WeaponLoot(Weapon weapon, Arthefact arthefact)
    {
        Init(weapon, arthefact);

    }
    private void Awake()
    {
        _weaponEvent = GetComponent<GiveWeaponEvent>();
        _giveWeapon = GetComponent<GiveWeapon>();
        _weaponEvent.OnGiveWeapon += PlaySoundEffect;
        _arthefactEvent = GetComponent<GiveArthefactEvent>();
        _arthefactEvent.OnGiveArthefact += PlaySoundEffect;
        _arthefactEvent.OnGiveArthefact += GiveArthefact;
    }

  

    private void Start()
    {
        Init(WeaponOnPedestal, arthefactOnPedestal);
    }

   

    private void Update()
    {
        bool isButtonPressed = Input.GetKeyDown(KeyCode.E);
        if (WeaponOnPedestal != null )
        {
            if (_isReadyToGive)
            {
               
                if (isButtonPressed)
                {
                    
                        _weaponEvent.CallGiveWeaponEvent(WeaponOnPedestal);
                    
                  
                    DisableThisPedestal();
                }

            }
        }
        if (arthefactOnPedestal !=null)
        {
            if (_isReadyToGive)
            {

              
                if (isButtonPressed)
                {
                   
                        _arthefactEvent.CallGiveArthefactEvent(arthefactOnPedestal);
                    
                    DisableThisPedestal();
                }

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


    public void Init(Weapon weapon, Arthefact arthefact)
    {
        _startPosition = loot.transform.position;
        _WeaponSpriteRenderer = loot.GetComponent<SpriteRenderer>();
        if (weapon!=null)
        {
            WeaponOnPedestal = weapon;
            _WeaponSpriteRenderer.sprite = WeaponOnPedestal.WeaponDetails.Icon;
        }
        if (arthefact != null)
        {
            arthefactOnPedestal = arthefact;
            _WeaponSpriteRenderer.sprite = arthefactOnPedestal.Icon;
        }




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
        WeaponOnPedestal = null;
        arthefactOnPedestal = null;
        _WeaponSpriteRenderer.sprite = null;
        _weaponEvent.OnGiveWeapon -= PlaySoundEffect;
        Destroy(this);

    }

    private void GiveArthefact(GiveArthefactEvent giveArthefactEvent, GiveArthefactEventArgs giveArthefactEventArgs)
    {
        GameManager.Instance.GetPlayer().CurrentArthefact.ChangeArthefact(giveArthefactEventArgs.Arthefact);
    }


    private void PlaySoundEffect(GiveWeaponEvent giveWeapon, GiveWeaponEventArgs args)
    {
        if (takeTheLootSoundEffect!=null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(takeTheLootSoundEffect);

        }
    }

    private void PlaySoundEffect(GiveArthefactEvent giveArthefactEvent, GiveArthefactEventArgs giveArthefactEventArgs)
    {
        if (takeTheLootSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(takeTheLootSoundEffect);

        }
    }





}
