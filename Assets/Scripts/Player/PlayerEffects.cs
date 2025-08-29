using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Effect Settings")]
    [SerializeField] private ParticleSystem deliveredEffect;
    [SerializeField] private float destroyEffectTime;

    private void Start()
    {
        if (FlatbedItemHolder.Instance != null)
            FlatbedItemHolder.Instance.OnFlatbedLoaded += Instance_OnFlatbedLoaded;
    }
    private void OnDisable()
    {
        FlatbedItemHolder.Instance.OnFlatbedLoaded -= Instance_OnFlatbedLoaded;
    }

    private void Instance_OnFlatbedLoaded()
    {
        PlayDeliveredEffect(transform.position);
    }
    public void PlayDeliveredEffect(Vector3 position)
    {
        var effect = Instantiate(deliveredEffect);
        var offsetEffect = 2f;
        effect.gameObject.transform.position = new Vector3(position.x, position.y + offsetEffect, position.z);
        Destroy(effect.gameObject, destroyEffectTime);
    }
}
