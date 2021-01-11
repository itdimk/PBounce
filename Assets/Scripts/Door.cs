using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 OpenOffset;
    public Vector2 CloseOffset;

    public float OpenSpeed = 5;
    public float CloseSpeed = 5;
    public float Smoothness = 0.1F;

    private bool _isOpening;
    private bool _isClosing;

    private Vector2 _openPoint;
    private Vector2 _closePoint;
    private Vector2 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _openPoint = (Vector2) transform.position + OpenOffset;
        _closePoint = (Vector2) transform.position + CloseOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (_isOpening)
        {
            transform.position = Vector2.SmoothDamp(pos, _openPoint, ref _velocity,
                Smoothness, OpenSpeed);
        }
        else if (_isClosing)
        {
            transform.position = Vector2.SmoothDamp(pos, _closePoint, ref _velocity,
                Smoothness, CloseSpeed);
        }
    }

    public void Open()
    {
        _isOpening = true;
        _isClosing = false;
    }

    public void Close()
    {
        _isOpening = false;
        _isClosing = true;
    }
}