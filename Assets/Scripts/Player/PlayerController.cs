using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;


public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";
    public GameObject endScreen;
    public bool invencible = true;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("VFX")]
    public ParticleSystem vfxDeath;

    [Header("Limits")]
    public float limits = 4;
    public Vector2 limitVector = new Vector2(-4, 4);

    [SerializeField] private BounceHelper _bounceHelper;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentspeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7f;


    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    public void Bounce()
    {
        if(_bounceHelper != null)
            _bounceHelper.Bounce();
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;//o target recebe o y e o z dele mesmo deixando o x livre.
        _pos.z = transform.position.z;//entao o metedo lerp ao inves de pegar o player e encaixar em todos os vectores do target,
                                      //ele vai apenas encaixar o x, deixando a posição y livre e fazendo com q o z faça o transalate.
        if (_pos.x < limitVector.x) _pos.x = limitVector.x;
        else if (_pos.x > limitVector.y) _pos.x = limitVector.y;
        
        
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentspeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
            if (!invencible)
            {
                MoveBack(collision.transform);
                EndGame(AnimatorManager.AnimationType.DEAD);
                if (vfxDeath != null) vfxDeath.Play();
            }
        }
    }

    private void MoveBack(Transform t)
    {
        /*var pos = transform.position;
        pos.z = transform.position.z - 1f;
        transform.position = pos;*/
        t.DOMoveZ(1f, .3f).SetRelative();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            EndGame();
        }
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.play(animationType);
        
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.play(AnimatorManager.AnimationType.RUN, _currentspeed / _baseSpeedToAnimation);
    }




    #region POWERUPS

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentspeed = f;
    }

    public void ResetSpeed()
    {
        _currentspeed = speed;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);//.OnComplete(ResetHeight);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        /*var p = transform.position;//para receber a position
        p.y = _startPosition.y;//o p.y recebe a position inicial de y
        transform.position = p;//agora a position reseta.*/

        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }

    #endregion
}
