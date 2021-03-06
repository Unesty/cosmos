﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : Item
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "EndSpace")
            DeathObject(GetComponent<Item>());      
    }

    protected override void InteractionWithPlayer(GameObject collision)
    {
        base.InteractionWithPlayer(collision);
        
        //collision.GetComponent<PlayerController>().GetBalloon();

        StartCoroutine(CoDeathObject());
    }

    private IEnumerator CoDeathObject()
    {
        audioSource.Play();

        yield return new WaitForSeconds(1);

        DeathObject(GetComponent<Item>());
    } 
}
