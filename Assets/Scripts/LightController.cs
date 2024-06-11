using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _percent = 0.0f;
    [SerializeField, Range(0f, 5f)] private float _duration = 0.0f;
    [SerializeField] private Ease _ease = Ease.InOutSine;

    [SerializeField] private Light _light;

    [SerializeField] private List<LightsTransition> _transitions = new List<LightsTransition>();

    [SerializeField] private Era _nextEra = Era.Past;

    Tween _tween = null;

    private Era _lastEra = Era.Past;

    public void NextEra(Era nextEra)
    {
        if (_lastEra != _nextEra)
        {
            _lastEra = _nextEra;
        }
        else
        {
            return;
        }

        _percent = 0f;
        _tween?.Kill();
        _tween = DOTween.To(() => _percent,
            (x) => _percent = x,
            1.0f,
            _duration)
            .OnUpdate(Transition)
            .SetEase(_ease);
    }

    private void Transition()
    {
        Light _last = null;
        Light _current = null;

        for (int i = 0; i < _transitions.Count; i++)
        {
            if (_transitions[i].era != _nextEra)
            {
                continue;
            }

            _last = _transitions[i].LastLightData;
            _current = _transitions[i].CurrentLightData;

            break;
        }

        Quaternion currentRotation = Quaternion.Lerp(_last.transform.rotation, _current.transform.rotation, _percent);
        float intensity = Mathf.Lerp(_last.intensity, _current.intensity, _percent);
        Color color = Color.Lerp(_last.color, _current.color, _percent);

        _light.transform.rotation = currentRotation;
        _light.intensity = intensity;
        _light.color = color;
    }
}

[Serializable]
public class LightsTransition
{
    public Era era;
    public Light LastLightData, CurrentLightData;
}

public enum Era
{
    Past,
    Present,
    Futur,
}