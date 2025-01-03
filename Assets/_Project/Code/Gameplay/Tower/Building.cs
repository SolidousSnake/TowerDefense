﻿using UnityEngine;

namespace _Project.Code.Gameplay.Tower
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private Vector3[] _modelRotations;
        [SerializeField] private Renderer _mainRenderer;
        [SerializeField] private Vector2Int _originalSize;

        private int _rotationState = 0;

        public Vector2Int Size { get; private set; }

        private void Awake() => SetSize();

        public void SetTransparent(bool canPlace) => _mainRenderer.material.color = canPlace ? Color.green : Color.red;

        public void ResetColor() => _mainRenderer.material.color = Color.white;

        private void SetSize() => 
            Size = _rotationState is 1 or 3 ? new Vector2Int(_originalSize.y, _originalSize.x) : _originalSize;

        public void Rotate()
        {
            transform.Rotate(Vector3.up, 90);
            _rotationState = (_rotationState + 1) % 4;
            _model.transform.localPosition = _modelRotations[_rotationState];
            SetSize();
        }

        private void OnDrawGizmos()
        {
            var size = _originalSize;
            
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Gizmos.color = (i + j) % 2 == 0
                        ? new Color(0.95f, 0f, 0.76f)
                        : new Color(0.95f, 0.89f, 0f);

                    Gizmos.DrawCube(transform.position + new Vector3(i, 0, j)
                        , new Vector3(1, .1f, 1));
                }
            }
        }
    }
}