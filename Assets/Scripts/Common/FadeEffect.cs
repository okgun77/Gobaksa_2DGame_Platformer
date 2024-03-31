using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public static class FadeEffect
{
    public static IEnumerator Fade(Tilemap _target, float _start, float _end, float _fadeTime=1, UnityAction _action=null)
    {
        if (_target == null) yield break;

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / _fadeTime;

            Color color     = _target.color;
            color.a         = Mathf.Lerp(_start, _end, percent);
            _target.color   = color;

            yield return null;
        }

        _action?.Invoke();
    }

    public static IEnumerator Fade(SpriteRenderer _target, float _start, float _end, float _fadeTime=1, UnityAction _action=null)
    {
        if (_target == null) yield break;

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / _fadeTime;

            Color color     = _target.color;
            color.a         = Mathf.Lerp(_start, _end, percent);
            _target.color   = color;

            yield return null;
        }

        _action?.Invoke();
    }

}
