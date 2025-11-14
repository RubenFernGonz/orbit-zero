using UnityEngine;

public class Ruble : MonoBehaviour
{
    [Header("Opcional: efecto al destruirse")]
    public GameObject destroyEffect;

    void OnCollisionEnter(Collision collision)
    {
        // Si el proyectil usa collider normal
        if (collision.collider.CompareTag("PlayerProjectile"))
        {
            DestroyRubble();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el proyectil usa trigger
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other);
            Instantiate(destroyEffect,transform);
            DestroyRubble();
            
            
        }
    }

    private void DestroyRubble()
    {
        if (destroyEffect != null)
            Instantiate(destroyEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
