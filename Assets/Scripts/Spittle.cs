using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spittle : Item
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

        collision.gameObject.GetComponent<PlayerController>().Death();

        DeathObject(GetComponent<Item>());
    }
}
