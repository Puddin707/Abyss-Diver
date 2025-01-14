using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.5f; // Thời gian tồn tại của after image
    private float timeSpawned;
    private SpriteRenderer spriteRenderer;
    private Color color;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeSpawned = Time.time;
    }

    private void Update()
    {
        float timeElapsed = Time.time - timeSpawned;

        // Giảm dần alpha theo thời gian
        color.a = Mathf.Lerp(1f, 0f, timeElapsed / lifeTime);
        spriteRenderer.color = color;

        // Hủy đối tượng khi alpha gần bằng 0
        if (timeElapsed >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetUp(Sprite sprite, Vector3 position, Vector3 scale, Color startColor)
    {
        spriteRenderer.sprite = sprite;
        transform.position = position;
        transform.localScale = scale;
        color = startColor;
    }
}
