using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody2D))]
public class S_EnemyFarm : MonoBehaviour
{
    //
    [SerializeField] private S_OtherPlace S_OtherPlace;
    [SerializeField] private S_Player S_Player;
    //

    // Объект взгляда 
    CircleCollider2D SeeCol;

    [SerializeField] private GameObject Player;
    [HideInInspector] public Vector3 Target;

    [Header("Sprites")]   // Изображения 
    [SerializeField] private Sprite Way_Left;
    [SerializeField] private Sprite Way_Right;
    [SerializeField] private Sprite Way_Up;
    [SerializeField] private Sprite Way_Down;

    [SerializeField] private Sprite Run_Left;
    [SerializeField] private Sprite Run_Right;
    [SerializeField] private Sprite Run_Up;
    [SerializeField] private Sprite Run_Down;

    [SerializeField] private Sprite Filthy_im;

    [Header("Сharacteristics")]  // характеристики 
    [SerializeField] private float RunSpeed = 1.6f;

    [SerializeField] private bool DontMove;
    [SerializeField] private bool Filthy;  // грязный, когда взорвалась бомба 

    [HideInInspector] public int x = 99;
    [HideInInspector] public int y = 99;

    [SerializeField] private bool ForEasyWalk;

    [SerializeField] private bool SeePig;
    [SerializeField] private bool StopSee;
    [SerializeField] private int Target_x = 99;
    [SerializeField] private int Target_y = 99;

    private void Start()
    {
        SeeCol = gameObject.GetComponent<CircleCollider2D>();
    }

    // Движение
    #region Направление движения
    public void MoveUp()
    {
        if (y != 0 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y - 1, x].position;
            y--;

            if (!SeePig)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Up;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Run_Up;
        }
    }

    public void MoveDown()
    {
        if (y != S_OtherPlace.PosibleMovePosition.GetLength(0) - 1 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y + 1, x].position;
            y++;

            if (!SeePig)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Down;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Run_Down;

        }
    }

    public void MoveLeft()
    {
        if (x != 0 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y, x - 1].position;
            x--;

            if (!SeePig)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Left;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Run_Left;

        }
    }

    public void MoveRight()
    {
        if (x != S_OtherPlace.PosibleMovePosition.GetLength(1) - 1 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y, x + 1].position;
            x++;

            if (!SeePig)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Right;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Run_Right;
        }
    }
    #endregion

    // Движение 
    private void FixedUpdate()
    {
        if (!Filthy)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, RunSpeed * Time.deltaTime);

            // Бежит за целью 
            if (SeePig)
            {

                if (Target_y > y)
                    MoveDown();
                else if (Target_y < y)
                    MoveUp();
                else if (Target_x < x)
                    MoveLeft();
                else if (Target_x > x)
                    MoveRight();


                if (x == Target_x && y == Target_y)
                {
                    SeePig = false;
                    StartCoroutine(See());
                }

            }
            else
            {
                if (!ForEasyWalk)
                {
                    ForEasyWalk = true;
                    StartCoroutine(Walk());
                }
            }
        }
    }


    // Таймер на обновление взгляда
    IEnumerator See()
    {
        SeeCol.enabled = false;
        yield return new WaitForSeconds(1f);
        StopSee = false;
        SeeCol.enabled = true;
        ForEasyWalk = false;
    }

    // Таймер на обычную ходьбу
    IEnumerator Walk()
    {
        yield return new WaitForSeconds(1.1f);

        if (!SeePig)
            RandomMove();
    }

    void RandomMove()
    {
        int a = Random.Range(0, 4);

        switch (a)
        {
            case 0:
                MoveDown();
                break;
            case 1:
                MoveLeft();
                break;
            case 2:
                MoveRight();
                break;
            case 3:
                MoveUp();
                break;
        }

        StartCoroutine(Walk());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pig" && !StopSee)
        {
            Target_x = S_Player.x;
            Target_y = S_Player.y;

            StopSee = true;
            SeePig = true;
        }
    }

    public void Dead()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Filthy_im;
        Filthy = true;
        GameObject.Destroy(this);
    }

}
