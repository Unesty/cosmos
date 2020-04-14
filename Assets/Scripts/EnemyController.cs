using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpawnController spawnCon;
    [SerializeField] private PlayerController playerCon;

    // Позиция спауна шаров из Enemy
    [SerializeField] private Transform posSpawnInEnemy;

    // Можно ли сейчас двигаться
    private bool canMove;

    // Позиция линии к которой нужно двигаться, на которой находится Player
    private Vector3 nextPos;

    // Скорость передвижения Enemy
    [SerializeField] private float speedEnemy = 3;

    private void Update()
    {
        if (canMove)
        {
            // Движение Enemy к нужной линии
            transform.position = Vector2.MoveTowards(transform.position, nextPos, speedEnemy * Time.deltaTime);

            // При достижении нужной линии сообщение SpawnCon об этом
            if (transform.position.x == nextPos.x)
            {
                canMove = false;

                StartCoroutine(spawnCon.SpawnSpittle(posSpawnInEnemy.position));
            }
                
        }
    }

    // Задача новой позиции для Enemy, активация движение 
    public void MoveEnemyToPlayer(float posX)
    {
        nextPos = transform.position;
        nextPos.x = posX;

        canMove = true;
    }




}
