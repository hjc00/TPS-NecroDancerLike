using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorParticles : Particles
{
    public int damageAmount = 3;

    private ParticleSystem ps;

    public override void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Destroy(this.gameObject, ps.main.duration);

    }


    public override void OnParticleCollision(GameObject other)
    {
        if (other != null)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damageAmount);
        }
    }
}
