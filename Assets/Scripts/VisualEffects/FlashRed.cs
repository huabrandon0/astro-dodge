using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashRed : MonoBehaviour
{
    Renderer _renderer;
    [SerializeField] private float _frequency = 5f;
    [SerializeField] private int _numberOfFlashes = 2;
    [SerializeField] private Color _flashColor;

    void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void Flash()
    {
        StartCoroutine(Flasher());
    }

    public void StopFlashing()
    {
        StopAllCoroutines();
    }

    IEnumerator Flasher()
    {
        for (int i = 0; i < _numberOfFlashes; i++)
        {
            var normalColors = new Dictionary<Material, Color>();

            foreach (Material mat in _renderer.materials)
            {
                normalColors.Add(mat, mat.color);
                mat.color = _flashColor;
            }

            yield return new WaitForSeconds(0.5f / _frequency);

            foreach (Material mat in _renderer.materials)
            {
                mat.color = normalColors[mat];
            }

            yield return new WaitForSeconds(0.5f / _frequency);
        }
    }
}
