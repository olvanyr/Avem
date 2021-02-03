using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownWayPlatform : MonoBehaviour
{

    public PlatformEffector2D platformEffector;

    public BoxCollider2D boxCollider;

    public Transform playerTransform;
    public Transform selfTransform;

    public SpriteRenderer spriteRenderer;

    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public void Start()
    {
        playerInput = playerMovement.playerInput;

        //playerInput.NormalMovement.Action1.performed += context => GoToNextSene();
        playerInput.NormalMovement.Move.performed += context => GoDown(context.ReadValue<Vector2>());

        SetColliderSize();
    }


    private void Update()
    {
        if (playerTransform.position.y > selfTransform.position.y)
        {
            boxCollider.enabled = true;
            
        }
        else
        {
            boxCollider.enabled = false;
            
        }
    }
    public void GoDown(Vector2 direction)
    {
        if (direction.y == -1)
        {
            platformEffector.rotationalOffset = 180;
            boxCollider.enabled = false;
        }
        
        if (direction.y == 0)
        {
            platformEffector.rotationalOffset = 0;
        }
    }

    private void SetColliderSize()
    {
        boxCollider.size = new Vector2(spriteRenderer.size.x, boxCollider.size.y);
    }

}
