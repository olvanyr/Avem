using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    [SerializeField]
    private int playerHealth;
    public int maxHealth;

    public float invulnerabilityTime;
    public float timer;


    public bool isInvincible = false;
    public float invicibilityFlashDelay;
    public float invicibilityDelay;

    public SpriteRenderer graphics;

    public Color flashColor;

    public static PlayerHealth instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de PlayerHealth dans la scéne");
            return;
        }

        instance = this;
    }


        
    void Start()
    {
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void ReduceHealth(int damage)
    {
        if (!isInvincible)
        {
            if (playerHealth == 1)
            {
                PlayerMovement.instance.state = "die";
                PlayerDeath();
                playerHealth -= damage;
                StopAllCoroutines();
                return;
            }
            CameraFollow.instance.StartScreenShake(invicibilityFlashDelay, 0.1f, 0.9f);
            playerHealth -= damage;
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                return;
            }

            isInvincible = true;
            StopCoroutine(InvincibilityFlash());
            StopCoroutine(HandleInvincibilityDelay());
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
            
        }
        
    }

    public void AddHealth(int heal)
    {
        playerHealth += heal;
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
    }

    public void PlayerDeath()
    {
        Restart.instance.RestartGame();
        CameraFollow.instance.StartScreenShake(2f, 0.2f, 1f);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = flashColor;
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }
    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityDelay);
        isInvincible = false;

    }
}
