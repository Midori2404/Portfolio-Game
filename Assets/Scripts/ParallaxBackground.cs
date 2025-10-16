using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 lastCameraPosition;

    private float textureUnitSizeX;

    [SerializeField] private Vector2 parallaxMultiplier;

    private void Start()
    {
        
        lastCameraPosition = playerTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = playerTransform.position - lastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y);
        lastCameraPosition = playerTransform.position;

        if (Mathf.Abs(playerTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (playerTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(playerTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
