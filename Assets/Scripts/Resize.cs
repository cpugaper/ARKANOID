using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizable : MonoBehaviour
{
    public Vector2 sizePortrait;
    public Vector2 sizeLandscape;

    public Vector2 positionPortrait; 
    public Vector2 positionLandscape;

    void Start()
    {
        AdjustSize(); 
        AdjustPosition(); 
    }

    void Update()
    {
        AdjustSize();
        AdjustPosition();
    }

    public void AdjustSize()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        if (aspectRatio > 1) // Landscape
        {
            transform.localScale = new Vector3(sizeLandscape.x, sizeLandscape.y, 1f);
        }
        else // Portrait
        {
            transform.localScale = new Vector3(sizePortrait.x, sizePortrait.y, 1f);
        }

        LimitWithinCamera();
    }

    public void AdjustPosition()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        if (aspectRatio > 1) // Landscape
        {
            transform.localPosition = new Vector3(positionLandscape.x, positionLandscape.y, transform.localPosition.z);
        }
        else // Portrait
        {
            transform.localPosition = new Vector3(positionPortrait.x, positionPortrait.y, transform.localPosition.z);
        }
    }

    private void LimitWithinCamera()
    {
        Camera cam = Camera.main;
        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);
        float width = transform.localScale.x * 0.5f;
        float height = transform.localScale.y * 0.5f;

        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            transform.position = new Vector3(
                Mathf.Clamp(viewportPos.x, width, 1 - width),
                Mathf.Clamp(viewportPos.y, height, 1 - height),
                transform.position.z);
        }
    }
}
