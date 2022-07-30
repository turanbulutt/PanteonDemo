using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Paint : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Vector2Int textureArea;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float BrushAreaSize = 10f;

    Enable enable;
    LevelManager levelManager;
    Texture2D texture;
    Vector2 mousePos;
    bool isPainting;
    bool isEnd = false;
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        enable = FindObjectOfType<Enable>();
    }
    void Start()
    {
        texture = new Texture2D(textureArea.x, textureArea.y, TextureFormat.ARGB32, false);
        meshRenderer.material.mainTexture = texture;
        enable.SetDisable();
        StartCoroutine(PrintPercentage());
    }


    void Update()
    {
        if (isPainting && isEnd)
        {
            RaycastHit ray;
            //if we hit the wall we will paint
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out ray))
            {
                ChangePixel(ray.textureCoord);
            }
        }
    }

    private IEnumerator PrintPercentage()
    {
        while (true)
        {
            if (isEnd)
            {
                //checking all pixels of the wall in order to get percentage of red colored pixels
                int RedCount = 0;
                Color32[] texture32 = texture.GetPixels32();

                foreach (Color32 pixel in texture32)
                {
                    if (pixel.Equals(new Color32(255, 0, 0, 255)))
                        RedCount++;
                }
                float percentage = (100.0f * RedCount) / (texture.width * texture.height * 1.0f);
                string newText = ("Wall=% " + percentage.ToString());
                text.SetText(newText);

                if (percentage > 50f)
                {
                    enable.SetEnable();
                    levelManager.LoadEndGame();
                }
                    
                
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void ChangePixel(Vector2 coordinate)
    {
        coordinate.x *= texture.width;
        coordinate.y *= texture.height;
        Color32[] texture32 = texture.GetPixels32();

        //changing in particular area of the wall with brush size
        for (int x = (int)(coordinate.x - BrushAreaSize); x < (int)(coordinate.x + BrushAreaSize); x++)
        {
            if (x >= texture.width || x < 0)
                continue;
            for (int y = (int)(coordinate.y - BrushAreaSize); y < (int)(coordinate.y + BrushAreaSize); y++)
            {
                if (y >= texture.height || y < 0)
                    continue;
                texture32[x + (y * texture.width)] = new Color32(255, 0, 0, 255);
            }
        }

        texture.SetPixels32(texture32);
        texture.Apply();
    }

    void OnFire(InputValue value)
    {
        isPainting = value.isPressed;
    }

    void OnMousePosition(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    public void SetEnd(bool newEnd)
    {
        isEnd = newEnd;
    }
}
