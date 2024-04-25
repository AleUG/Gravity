using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private Texture2D[] cursorTexture;
    [SerializeField] private Vector2 cursorHotspot;

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture[0], cursorHotspot, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexture[1], cursorHotspot, CursorMode.Auto);
    }
}
