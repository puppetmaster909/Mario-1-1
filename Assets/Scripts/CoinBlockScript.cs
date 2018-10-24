using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlockScript : MonoBehaviour {
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool playerHit;

    public Transform playerHitBox;
    public LayerMask isPlayer;
    public AudioClip coinSound;
    public AudioClip bumpSound;
    public Sprite deadSprite;

    public float wallHitHeight;
    public float wallHitWidth;

    public int numCoins;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHit = Physics2D.OverlapBox(playerHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isPlayer);

        if (playerHit == true && numCoins > 0)
        {
            numCoins -= 1;
            GetComponent<AudioSource>().PlayOneShot(coinSound);
            collision.gameObject.GetComponent<PlayerController>().addCoin();
            collision.gameObject.GetComponent<PlayerController>().SetCountText();
            if (numCoins == 0)
            {
                spriteRenderer.sprite = deadSprite;
            }
        }
        else if (playerHit == true && numCoins == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(bumpSound);
            
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(playerHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));
    }

}
