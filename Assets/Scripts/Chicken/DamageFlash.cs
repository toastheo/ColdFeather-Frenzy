using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{

    [SerializeField] private Color flashColor = Color.green;
    [SerializeField] private float flashTime = 0.5f;

    // this is more important if the sprite has legs, arms, etc.
    private SpriteRenderer[] spriteRenders;
    private Material[] materials;

    private Coroutine damageFlashCoroutine;

    private void Awake()
    {
        spriteRenders = GetComponentsInChildren<SpriteRenderer>();
        
        Init();
    }

    private void Init()
    {
        materials = new Material[spriteRenders.Length];

        // assign sprite renderer material to material 
        for (int i = 0; i < spriteRenders.Length; i++)
        {
            materials[i] = spriteRenders[i].material;
        }
    }

    public void CallDamageFlash()
    {
        damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        // set the color
        SetFlashColor();
        // lerp the flash amount
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        // how long it takes for the flash to go 0
        while (elapsedTime < flashTime)
        {
            // iterate elapsedTime
            elapsedTime += Time.deltaTime;
            
            /// lerp the flash amount
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_FlashColor", flashColor);
        }
    }

    private void SetFlashAmount(float amount)
    {
        // set flash amount
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", amount);
        }
    }
}
