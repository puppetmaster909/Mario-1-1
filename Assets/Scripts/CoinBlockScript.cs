using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlockScript : MonoBehaviour {
    private Animator anim;

    private bool playerHit;
    private bool justHit;

    public Transform playerHitBox;
    public LayerMask isPlayer;

    public float wallHitHeight;
    public float wallHitWidth;

    public int numCoins;

    // Use this for initialization
    void Start()
    {

        //anim = GetComponent<Animator>();
        justHit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHit = Physics2D.OverlapBox(playerHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isPlayer);

        if (playerHit == true && numCoins > 0 && justHit == false)
        {
            numCoins -= 1;
            
        }
        else if (playerHit == true && numCoins == 0)
        {
            //anim.SetBool("emptyBlock", true);
            
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(playerHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));
    }

    IEnumerator turnOffJustHit(float time)
    {
        yield return new WaitForSeconds(time);
        
    }

}
