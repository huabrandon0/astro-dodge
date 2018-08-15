using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFade : MonoBehaviour
{
    bool _isFaded = false;

    Renderer _renderer;

    [Range(0, 255)]
    [SerializeField] int _onAlpha = 255;

    [Range(0, 255)]
    [SerializeField] int _offAlpha = 0;

    void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void ToggleFade()
    {
        if (_isFaded)
            FadeIn();
        else
            FadeOut();

        Debug.Log("toggle!");
    }

    public void FadeIn()
    {
        _isFaded = false;
        foreach (Material mat in _renderer.materials)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, _onAlpha / 255f);
        }
    }

    public void FadeOut()
    {
        _isFaded = true;
        foreach (Material mat in _renderer.materials)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, _offAlpha / 255f);
        }
    }
}
