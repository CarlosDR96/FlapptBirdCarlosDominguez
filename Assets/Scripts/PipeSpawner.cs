using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject topPipePrefab;
    public GameObject bottomPipePrefab;
    public float spawnRate = 3f;
    public float minYPosition = 1f; // Posición mínima para que el borde del pipe siempre toque el borde de la pantalla
    public float maxYPosition = 5f; // Posición máxima para el desplazamiento

    void Start()
    {
        InvokeRepeating("SpawnPipe", 0f, spawnRate);
    }

    void SpawnPipe()
    {
        bool spawnTop = Random.value > 0.5f; // Decidir si spawnea arriba o abajo
        float yOffset = Random.Range(minYPosition, maxYPosition);
        GameObject pipe;
        float yPos;

        if (spawnTop)
        {
            // Spawnear el pipe desde el borde superior y moverlo hacia abajo
            yPos = Camera.main.orthographicSize + yOffset;
            pipe = Instantiate(topPipePrefab, new Vector3(10f, yPos, 0f), Quaternion.identity);
        }
        else
        {
            // Spawnear el pipe desde el borde inferior y moverlo hacia arriba
            yPos = -Camera.main.orthographicSize - yOffset;
            pipe = Instantiate(bottomPipePrefab, new Vector3(10f, yPos, 0f), Quaternion.identity);
        }
    }
}
