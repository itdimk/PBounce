using System;
using System.Collections.Generic;
using UnityEngine;

public class OutsideOptimizer : MonoBehaviour
{
    private Rect _enableRect;
    private Rect _disableRect;

    [SerializeField] List<GameObject> Targets = new List<GameObject>();
    [SerializeField] Transform Viewer;
    [SerializeField] float DistanceX;
    [SerializeField] float DistanceY;
    [SerializeField] float NeutralBorder = 2;
    [SerializeField] float RefreshBordersInterval = 0.2f;

    private bool _isEnabled;

    private void Start()
    {
        foreach (var target in Targets)
            target.SetActive(false);
    }

    private Rect CalcEnableRect()
    {
        Targets.RemoveAll(t => !t.gameObject);

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        foreach (var target in Targets)
        {
            var pos = target.transform.position;

            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.y < minY) minY = pos.y;
            if (pos.y > maxY) maxY = pos.y;
        }

        minX -= DistanceX / 2;
        maxX += DistanceX / 2;

        minY -= DistanceY / 2;
        maxY += DistanceY / 2;

        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }

    private Rect CalcDisableRect(Rect enableRect)
    {
        return new Rect(
            enableRect.xMin - NeutralBorder / 2,
            enableRect.yMin - NeutralBorder / 2,
            enableRect.width + NeutralBorder,
            enableRect.height + NeutralBorder
        );
    }

    private void RefreshBordersIfRequired()
    {
        if (!ActionEx.CheckCooldown(RefreshBordersIfRequired, RefreshBordersInterval)) return;
        
        _enableRect = CalcEnableRect();
        _disableRect = CalcDisableRect(_enableRect);
    }

    private void Update()
    {
        RefreshBordersIfRequired();

        var pos = Viewer.transform.position;
        bool enableAll = _enableRect.Contains(pos);
        bool disableAll = !_disableRect.Contains(pos);

        if (enableAll && !_isEnabled)
            foreach (var t in Targets)
                t.SetActive(_isEnabled = true);

        else if (disableAll && _isEnabled)
            foreach (var t in Targets)
                t.SetActive(_isEnabled = false);
    }

    private void OnDrawGizmos()
    {
        RefreshBordersIfRequired();

        Gizmos.color = Color.white;
        GizmosEx.DrawRect(_enableRect);

        Gizmos.color = Color.gray;
        GizmosEx.DrawRect(_disableRect);
    }
}