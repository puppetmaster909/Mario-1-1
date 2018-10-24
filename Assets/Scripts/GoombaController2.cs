using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoombaController2 : MonoBehaviour {

    private Animator anim;
    public LayerMask isGround;
    public LayerMask isPlayer;
    public Transform wallHitBox;
    public Transform canKillBox;
    public AudioSource stomp;
    private BoxCollider2D bc;

    private bool wallHit;
    private bool killHit;
    private AudioSource audioS;

    public float canKillHeight;
    public float canKillWidth;
    public float wallHitHeight;
    public float wallHitWidth;
    public float speed;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed *= -1;
        }

        killHit = Physics2D.OverlapBox(canKillBox.position, new Vector2(canKillWidth, canKillHeight), 0, isPlayer);
        if (killHit)
        {
            audioS.Play();
            anim.SetBool("isDead", true);
            bc.enabled = false;
            Destroy(gameObject, 1);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));

        Gizmos.color = Color.green;
        Gizmos.DrawCube(canKillBox.position, new Vector3(canKillWidth, canKillHeight, 1));
    }

}
