using NaughtyAttributes;
using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasScreenModeCameraLinker : MonoBehaviour
    {
        [SerializeField, SortingLayer] private int _sortingLayer;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;
            _canvas.sortingLayerID = _sortingLayer;
        }

        private void Update()
        {
            if (!_canvas.worldCamera)
            {
                _canvas.worldCamera = Camera.main;
                _canvas.sortingLayerID = _sortingLayer;
            }
        }
    }
}
