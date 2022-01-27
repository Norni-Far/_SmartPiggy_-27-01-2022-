using UnityEngine;

public class S_MainCamera : MonoBehaviour
{

    [SerializeField] private Transform Target;
    private float speedMove = 0.07f;

    private void FixedUpdate()
    {
        if (Target != null)
            if (Target.position.x + 0.1f < transform.position.x || Target.position.x - 0.1f > transform.position.x)
            {
                if (Target.position.x < transform.position.x && transform.position.x > -2.3f)
                    transform.position = new Vector3(transform.position.x - speedMove, 0, -10);
                else if (Target.position.x > transform.position.x && transform.position.x < 1.94f)
                    transform.position = new Vector3(transform.position.x + speedMove, 0, -10);
            }
    }

}
