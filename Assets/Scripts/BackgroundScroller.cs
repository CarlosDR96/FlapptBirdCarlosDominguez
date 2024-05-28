using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public float scrollSpeed = 2f;
    private GameObject[] backgrounds;
    private float backgroundWidth;

    void Start()
    {
        // Instanciar los tres fondos
        backgrounds = new GameObject[3];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i] = Instantiate(backgroundPrefab, new Vector3(i * backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0), Quaternion.identity);
        }

        // Obtener el ancho del fondo
        backgroundWidth = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Mover los fondos de izquierda a derecha
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        // Verificar si algún fondo ha salido de la pantalla y reposicionarlo
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (backgrounds[i].transform.position.x < -backgroundWidth)
            {
                // Encontrar el fondo más a la derecha
                float rightMostX = backgrounds[0].transform.position.x;
                for (int j = 1; j < backgrounds.Length; j++)
                {
                    if (backgrounds[j].transform.position.x > rightMostX)
                    {
                        rightMostX = backgrounds[j].transform.position.x;
                    }
                }

                // Mover el fondo que salió de la pantalla a la derecha del fondo más a la derecha
                backgrounds[i].transform.position = new Vector3(rightMostX + backgroundWidth, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            }
        }
    }
}
