using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : Item
{
    [SerializeField] private Sprite[] allSprites;

    protected override void Start()
    {
        base.Start();

        ChangeSprite();
    }    

    // Выбор спрайта из вариантов
    private void ChangeSprite()
    {
        int rnd = Random.Range(0, allSprites.Length);

        GetComponent<SpriteRenderer>().sprite = allSprites[rnd];
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "EndSpace")
            DeathObject();
    }

    protected override void InteractionWithPlayer(GameObject collision)
    {
        base.InteractionWithPlayer(collision);

        collision.gameObject.GetComponent<PlayerController>().Death();

        DeathObject();
    }

}
