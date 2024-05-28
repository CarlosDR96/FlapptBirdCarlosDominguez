using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento de los pipes
    public float leftBound = -10f; // Límite izquierdo para despawnear

    void Update()
    {
        // Mueve el pipe hacia la izquierda
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Despawnea el pipe cuando sale de la pantalla
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
