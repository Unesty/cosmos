using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Item
{
    public PointController pointCon;    

    // Кол-во очков, получаемое за эту звезду
    [SerializeField] private int myPoints;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "EndSpace")
            DeathObject();
    }

    protected override void InteractionWithPlayer(GameObject collision)
    {
        base.InteractionWithPlayer(collision);
        
        pointCon.AddPoint(myPoints);

        StartCoroutine(CoDeathObject());
    }

    private IEnumerator CoDeathObject()
    {
        audioSource.Play();

        yield return new WaitForSeconds(1);

        DeathObject();
    }

}
