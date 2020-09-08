using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private PlayerBase playerBase;
    [SerializeField] private bool facesRight;

    private Rigidbody2D rb2d;
    private int playerLayerIndex;
    private int direction;

    private void Awake()
    {
        rb2d = playerBase.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerLayerIndex = LayerMask.NameToLayer("Player");
        direction = facesRight ? 1 : -1;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            if (!playerBase.IsClimbingLadder() && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                playerBase.PlayLadderClimbAnim(true);
                playerBase.transform.localScale = new Vector2(transform.localScale.x * direction, playerBase.transform.localScale.y);
                playerBase.transform.position = new Vector2(transform.position.x + 0.5f * direction, playerBase.transform.position.y);                
            }
        }
    }
}
