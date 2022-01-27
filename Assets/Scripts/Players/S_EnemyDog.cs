using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody2D))]
public class S_EnemyDog : MonoBehaviour
{
    //
    [SerializeField] private S_OtherPlace S_OtherPlace;
    [SerializeField] private S_Player S_Player;
    //

    [SerializeField] private GameObject Player;
    [HideInInspector] public Vector3 Target;

    [HideInInspector] public int x = 99;
    [HideInInspector] public int y = 99;

    [Header("Sprites")]   // Изображения 
    [SerializeField] private Sprite Way_Left;
    [SerializeField] private Sprite Way_Right;
    [SerializeField] private Sprite Way_Up;
    [SerializeField] private Sprite Way_Down;

    [SerializeField] private Sprite Filthy_Left;
    [SerializeField] private Sprite Filthy_Right;
    [SerializeField] private Sprite Filthy_Up;
    [SerializeField] private Sprite Filthy_Down;

    [Header("Сharacteristics")]  // характеристики 
    [SerializeField] private float WalkSpeed = 2.1f;
    [SerializeField] private bool DontMove;
    [HideInInspector] public string NeedMove = "";
    [SerializeField] private bool Filthy;  // грязный, когда взорвалась  бомба 
    [SerializeField] private bool Dead;

    public void DogMov()
    {
        if (!DontMove)
        {
            if (NeedMove == "x")
            {
                if (transform.position.x > Player.transform.position.x)
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
            }
            else if (NeedMove == "y")
            {
                if (S_Player.y > y)
                {
                    MoveDown();
                }
                else
                {
                    MoveUp();
                }
            }
        }
    }

    // Движение
    public void MoveUp()
    {
        if (y != 0 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y - 1, x].position;
            y--;

            if (!Filthy)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Up;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Filthy_Up;

        }
    }

    public void MoveDown()
    {
        if (y != S_OtherPlace.PosibleMovePosition.GetLength(0) - 1 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y + 1, x].position;
            y++;

            if (!Filthy)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Down;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Filthy_Down;
        }
    }

    public void MoveLeft()
    {
        if (x != 0 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y, x - 1].position;
            x--;

            if (!Filthy)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Left;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Filthy_Left;
        }
    }

    public void MoveRight()
    {
        if (x != S_OtherPlace.PosibleMovePosition.GetLength(1) - 1 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y, x + 1].position;
            x++;

            if (!Filthy)
                gameObject.GetComponent<SpriteRenderer>().sprite = Way_Right;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Filthy_Right;
        }
    }


    // Движение 
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target, WalkSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Filthy = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = Filthy_Left;
        }

        if (collision.gameObject.tag == "Pit")
        {
            GameObject.FindGameObjectWithTag("Pit").GetComponent<Animator>().enabled = true;
            GameObject.Destroy(gameObject);
        }


    }
}
