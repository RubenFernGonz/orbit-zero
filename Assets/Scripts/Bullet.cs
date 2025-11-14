using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    float speed;

    public void Init(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
        Destroy(gameObject, 5f);
    }

    void Update()
{
    // Mover
    transform.position += direction * speed * Time.deltaTime;

    // Alinear visualmente la bala con su movimiento
   // transform.rotation = Quaternion.LookRotation(direction);
}

}
