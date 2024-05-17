using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxOpening : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<ParticleSystem> _particles;
    [SerializeField] private GameObject _reward;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _text.enabled = false;
            _animator.SetBool("IsOpen", true);
        }
    }

    public void Award()
    {
        foreach (ParticleSystem particle in _particles)
        {
            particle.Play();
        }
    }

    public void DestroyBox()
    {
        gameObject.SetActive(false);
    }

    public void Reward()
    {
        _reward.SetActive(true);
    }
}
