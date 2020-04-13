using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MainController mainCon;

    float speed = 5;
    private Vector3 firstTouch;
    private Vector3 secondTouch;

    int nowLaneNumber = 1,
        lanesCount = 2;

    public float FirstLanePos,
                 LaneDistance,
                 SideSpeed;

    private bool didChangeLastFrame = false;

    private bool canMove = true;

    private bool iHaveShield;
    [SerializeField] private SpriteRenderer shieldSprite;

    private Animator anim;
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canMove)
        {
            // При нажатии первая точка свайпа
            if (Input.GetMouseButtonDown(0))
            {
                firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            // При отжатии вторая точка свайпа
            if (Input.GetMouseButtonUp(0))
            {
                secondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                didChangeLastFrame = true;
            }

            if (didChangeLastFrame)
            {
                // Свайп влево
                if (firstTouch.x > secondTouch.x)
                {
                    if (nowLaneNumber > 0)
                    {
                        nowLaneNumber--;

                        ChangeAnimation("left");
                    }
                }
                else // Свайп вправо
                {
                    if (nowLaneNumber < 2)
                    {
                        nowLaneNumber++;

                        ChangeAnimation("right");
                    }
                }

                didChangeLastFrame = false;
            }

            Vector3 newPos = transform.position;
            newPos.x = Mathf.Lerp(newPos.x, FirstLanePos + (nowLaneNumber * LaneDistance), Time.deltaTime * SideSpeed);
            transform.position = newPos;
        }
    }

    private void ChangeAnimation(string animationNow)
    {
        anim.SetTrigger(animationNow);
    }

    public void GetShield()
    {
        iHaveShield = true;

        shieldSprite.enabled = true;
    }

    public void Death()
    {
        if (iHaveShield)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();

            iHaveShield = false;

            shieldSprite.enabled = false;

            return;
        }
        else
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();

            canMove = false;

            mainCon.GameDeActivate();
        }
    }
}
