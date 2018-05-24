using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenParticles : Particles
{


    public  override void Start()
    {
        Destroy(this.gameObject, 5.1f);
    }

}
