using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    //weapons stuffs
    [SerializeField] protected float timeBetweenAttacks;
    
    [SerializeField] protected float chargeUpTime;
    [SerializeField, Range(0, 1)] protected float minChargePercent;
    [SerializeField] private bool isFullyAuto;
    private Coroutine _currentFireTimer;
    private bool _isOnCooldown;
    private WaitForSeconds _coolDownWait;
    private WaitUntil _coolDownEnforce;
    private float _currentChargeTime;
    public static int currentBullets = 15;
    private void Start()
    {
        _coolDownWait = new WaitForSeconds(timeBetweenAttacks);
        _coolDownEnforce = new WaitUntil(() => !_isOnCooldown);
    }
    
    public void StartShooting()
    {
        _currentFireTimer = StartCoroutine(ReFireTimer());
    }
    public void StopShooting()
    {
        StopCoroutine(_currentFireTimer);
        float percent = _currentChargeTime / chargeUpTime;
        if(percent != 0) TryAttack(percent);
        
    }
    private IEnumerator CooldownTimer()
    {
        _isOnCooldown = true;
        yield return _coolDownWait;
        _isOnCooldown = false;
    }
    private IEnumerator ReFireTimer()
    {
        yield return _coolDownEnforce;
        while (_currentChargeTime < chargeUpTime)
        {
            _currentChargeTime += Time.deltaTime;
            yield return null;
        }
        
        TryAttack(1);
        yield return null;
    }
    private void TryAttack(float percent, bool isRefire = true)
    {
        _currentChargeTime = 0;
        if (!CanAttack(percent)) return;
        Attack(percent);
        StartCoroutine(CooldownTimer());
        
        if (isFullyAuto && percent >= 1) _currentFireTimer = StartCoroutine(ReFireTimer());
        
    }
    protected virtual bool CanAttack(float percent)
    {
        return !_isOnCooldown && percent >= minChargePercent;
    }
    
    
    protected abstract void Attack(float percent);

}