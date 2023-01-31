using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
    public Animator anima;
    public string triggerAttack = "Attack";

    public Health healthBase;

    private void OnValidate()
    {
        try { healthBase = gameObject.GetComponent<Health>(); }
        catch { Debug.LogWarning("Script Health n�o encontrado"); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        anima.SetTrigger(triggerAttack);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}
