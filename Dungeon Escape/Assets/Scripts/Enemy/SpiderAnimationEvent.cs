using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{

    public AcidEffect acidEffect;

    public void Fire()
    {
        Instantiate(acidEffect, this.transform.position, Quaternion.identity);
    }
}
