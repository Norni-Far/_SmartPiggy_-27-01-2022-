using UnityEngine;

public class S_OtherPlace : MonoBehaviour
{
    // Скрипты
    [SerializeField] private S_Player S_Player;
    [SerializeField] private S_EnemyDog S_EnemyDog;
    [SerializeField] private S_EnemyFarm S_EnemyFarm;
    //

    [Header("gameObjects")]  // Объекты
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject EnemyFarm;
    [SerializeField] private GameObject EnemyDog;
    [SerializeField] private GameObject Music;
    //

    // Канвас
    [SerializeField] private GameObject WinPannel;
    [SerializeField] private GameObject LostPannel;

    [SerializeField] private Transform[] PositionMove_0_ = new Transform[9];
    [SerializeField] private Transform[] PositionMove_1_ = new Transform[9];
    [SerializeField] private Transform[] PositionMove_2_ = new Transform[9];
    [SerializeField] private Transform[] PositionMove_3_ = new Transform[9];
    [SerializeField] private Transform[] PositionMove_4_ = new Transform[9];

    [HideInInspector] public Transform[,] PosibleMovePosition = new Transform[5, 9];
    private Transform[] PositionMove = new Transform[9];

    void Start()
    {
        // Расстановка объектов
        {
            Player.transform.position = new Vector3(PositionMove_2_[0].position.x, PositionMove_2_[0].position.y, PositionMove_2_[0].position.z);
            EnemyFarm.transform.position = new Vector3(PositionMove_1_[7].position.x, PositionMove_1_[7].position.y, PositionMove_1_[7].position.z);
            EnemyDog.transform.position = new Vector3(PositionMove_2_[8].position.x, PositionMove_2_[8].position.y, PositionMove_2_[8].position.z);
        }

        // рассчитывание возможных позиций для перемемщения 
        StartPosition();
    }

    private void FixedUpdate()
    {
        if (S_Player == null)
        {
            LostPannel.SetActive(true);
            Music.SetActive(false);
        }
        else if (S_EnemyDog == null && S_EnemyFarm == null)
        {
            WinPannel.SetActive(true);
        }
    }

    private void StartPosition()     // рассчитывание возможных позиций для перемемщения 
    {
        for (int i = 0; i < PosibleMovePosition.GetLength(0); i++)
        {
            switch (i)
            {
                case 0:
                    PositionMove = PositionMove_0_;
                    break;
                case 1:
                    PositionMove = PositionMove_1_;
                    break;
                case 2:
                    PositionMove = PositionMove_2_;
                    break;
                case 3:
                    PositionMove = PositionMove_3_;
                    break;
                case 4:
                    PositionMove = PositionMove_4_;
                    break;
            }

            for (int v = 0; v < PosibleMovePosition.GetLength(1); v++)
            {
                PosibleMovePosition[i, v] = PositionMove[v];

                // Передача своих позиций объектам, что бы, приизменениях,
                // не менять во всех скриптах, а только сдесь
                if (Player.transform.position == PositionMove[v].position)
                {
                    S_Player.y = i;
                    S_Player.x = v;
                    S_Player.Target = PosibleMovePosition[i, v].position;
                }
                else if (EnemyFarm.transform.position == PositionMove[v].position)
                {
                    S_EnemyFarm.y = i;
                    S_EnemyFarm.x = v;
                    S_EnemyFarm.Target = PosibleMovePosition[i, v].position;
                }
                else if (EnemyDog.transform.position == PositionMove[v].position)
                {
                    S_EnemyDog.y = i;
                    S_EnemyDog.x = v;
                    S_EnemyDog.Target = PosibleMovePosition[i, v].position;
                }
            }
        }

        S_Player.BtnColors();   // возможности к еперемещению 
    }


}
