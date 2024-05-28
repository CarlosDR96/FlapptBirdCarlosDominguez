using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public float upForce = 200f;
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip flapSound;
    public AudioClip hitSound;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb2d.isKinematic = true;
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Flap();
            }
        }
    }

    void Flap()
    {
        rb2d.isKinematic = false;
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(0, upForce));
        anim.SetTrigger("Flap");
        audioSource.PlayOneShot(flapSound);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        audioSource.PlayOneShot(hitSound);
        GameController.instance.BirdDied();
    }

    void FixedUpdate()
    {
        if (rb2d.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 30);
        }
        else if (rb2d.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
