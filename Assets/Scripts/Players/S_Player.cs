using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Collider), typeof(Rigidbody2D))]
public class S_Player : MonoBehaviour
{
    //
    [SerializeField] private S_OtherPlace S_OtherPlace;
    [SerializeField] private S_EnemyDog S_EnemyDog;
    //

    [Header("Prefabs")]   // Prefabs
    [SerializeField] private GameObject Bomb_prefab;
    [SerializeField] private GameObject Spirit;

    [Header("Sprites")] // Изображения 
    [SerializeField] private Sprite Way_Left;
    [SerializeField] private Sprite Way_Right;
    [SerializeField] private Sprite Way_Up;
    [SerializeField] private Sprite Way_Down;

    [Header("ButtonsUI")]// Buttons
    [SerializeField] private GameObject Left_Btn;
    [SerializeField] private GameObject Right_Btn;
    [SerializeField] private GameObject Up_Btn;
    [SerializeField] private GameObject Down_Btn;

    // позиция объекта
    [HideInInspector] public int x = 99;
    [HideInInspector] public int y = 99;

    // Хараетеристики 
    [SerializeField] private float WalkSpeed = 2f;
    [HideInInspector] public bool Dead;

    Vector3 Bomb = new Vector3(0, 0, 0);
    [HideInInspector] public Vector3 Target;


    // Кнопки 
    public void SetBomb()  // становится на позицию свиньи 
    {
        if (transform.position == Target && Bomb != transform.position)
        {
            Bomb = Instantiate(Bomb_prefab, transform.position, transform.rotation).transform.position;
        }

    }

    #region Направление движения
    public void MoveUp()
    {
        if (y != 0 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y - 1, x].position;
            y--;

            gameObject.GetComponent<SpriteRenderer>().sprite = Way_Up;

            if (S_EnemyDog != null)
            {
                S_EnemyDog.NeedMove = "y";
                S_EnemyDog.DogMov();
            }
        }

        BtnColors();
    }

    public void MoveDown()
    {
        if (y != S_OtherPlace.PosibleMovePosition.GetLength(0) - 1 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y + 1, x].position;
            y++;

            gameObject.GetComponent<SpriteRenderer>().sprite = Way_Down;

            if (S_EnemyDog != null)
            {
                S_EnemyDog.NeedMove = "y";
                S_EnemyDog.DogMov();
            }
        }

        BtnColors();
    }

    public void MoveLeft()
    {
        if (x != 0 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y, x - 1].position;
            x--;

            gameObject.GetComponent<SpriteRenderer>().sprite = Way_Left;

            if (S_EnemyDog != null)
            {
                S_EnemyDog.NeedMove = "x";
                S_EnemyDog.DogMov();
            }
        }

        BtnColors();
    }

    public void MoveRight()
    {
        if (x != S_OtherPlace.PosibleMovePosition.GetLength(1) - 1 && transform.position == Target)
        {
            Target = S_OtherPlace.PosibleMovePosition[y, x + 1].position;
            x++;

            gameObject.GetComponent<SpriteRenderer>().sprite = Way_Right;

            if (S_EnemyDog != null)
            {
                S_EnemyDog.NeedMove = "x";
                S_EnemyDog.DogMov();
            }
        }

        BtnColors();
    }
    #endregion

    public void BtnColors()
    {
        if (y == 0)
            Up_Btn.GetComponent<Image>().color = new Color(0, 0, 0, 0.1f);
        else
            Up_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

        if (y == S_OtherPlace.PosibleMovePosition.GetLength(0) - 1)
            Down_Btn.GetComponent<Image>().color = new Color(0, 0, 0, 0.1f);
        else
            Down_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

        if (x == 0)
            Left_Btn.GetComponent<Image>().color = new Color(0, 0, 0, 0.1f);
        else
            Left_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

        if (x == S_OtherPlace.PosibleMovePosition.GetLength(1) - 1)
            Right_Btn.GetComponent<Image>().color = new Color(0, 0, 0, 0.1f);
        else
            Right_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
    } // изменение цвета кнопок



    // Движение 
    private void FixedUpdate()
    {
        if (!Dead)
            transform.position = Vector2.MoveTowards(transform.position, Target, WalkSpeed * Time.deltaTime);
    }

    // Взаимодействия 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Instantiate(Spirit, transform.position, transform.rotation);
            GameObject.Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Pit")
        {
            Instantiate(Spirit, transform.position, transform.rotation);
            GameObject.FindGameObjectWithTag("Pit").gameObject.GetComponent<Animator>().enabled = true;
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog" || collision.gameObject.tag == "Farmer")
        {
            Instantiate(Spirit, transform.position, transform.rotation);
            GameObject.Destroy(gameObject);
        }

    }


}
