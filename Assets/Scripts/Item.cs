using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SpawnController spawnCon;

    // Скорость движения вверх
    public float speed;

    // Можно ли двигаться сейчас
    protected bool canMove = false;

    // Направление движения
    protected Vector2 direction;

    protected AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();

        direction.y = 5;

        canMove = true;
    }

    protected void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            InteractionWithPlayer(collision.gameObject);
        }
    }

    // Взаимодействие с Player
    protected virtual void InteractionWithPlayer(GameObject collision)
    {

    }

    public void CanMove(bool move = false)
    {
        canMove = move;
    }

    protected virtual void DeathObject() 
    {
        spawnCon.DeleteItem(GetComponent<Item>());
    }

}
