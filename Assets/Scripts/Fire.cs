using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public Camera cam;

    public float fireCooldown = 0.2f;
    bool canShoot = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
            Shoot();
    }

    void Shoot()
    {
        canShoot = false;

        // Raycast desde el centro de la cámara
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Vector3 dir;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            dir = (hit.point - muzzle.position).normalized; // hacia el punto de impacto
        else
            dir = ray.direction; // dirección de la cámara si no hay colisión

        // Rotación que apunta hacia la dirección con offset para que el mesh visual quede correcto
        Quaternion rot = Quaternion.LookRotation(dir) * Quaternion.Euler(0, -90, 0);

        // Instanciar proyectil
        GameObject b = Instantiate(bulletPrefab, muzzle.position, rot);

        // Inicializar movimiento
        b.GetComponent<Bullet>().Init(dir, bulletSpeed);

        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(fireCooldown);
        canShoot = true;
    }
}
