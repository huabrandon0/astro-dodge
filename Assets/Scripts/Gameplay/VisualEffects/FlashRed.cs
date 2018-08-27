using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashRed : MonoBehaviour
{
    Renderer _renderer;
    [SerializeField] private float _frequency = 5f;
    [SerializeField] private int _numberOfFlashes = 2;
    [SerializeField] private Color _flashColor;

    Dictionary<Material, Color> _normalColors;

    void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _normalColors = new Dictionary<Material, Color>();
        foreach (Material mat in _renderer.materials)
        {
            _normalColors.Add(mat, mat.color);
        }
    }

    public void Flash()
    {
        StopFlashing();
        StartCoroutine(Flasher());
    }

    public void StopFlashing()
    {
        Reset();
        StopAllCoroutines();
    }

    IEnumerator Flasher()
    {
        for (int i = 0; i < _numberOfFlashes; i++)
        {
            foreach (Material mat in _renderer.materials)
            {
                mat.color = _flashColor;
            }

            yield return new WaitForSeconds(0.5f / _frequency);

            Reset();

            yield return new WaitForSeconds(0.5f / _frequency);
        }
    }

    void Reset()
    {
        foreach (Material mat in _renderer.materials)
        {
            mat.color = _normalColors[mat];
        }
    }

    void OnDisable()
    {
        StopFlashing();
    }
}
