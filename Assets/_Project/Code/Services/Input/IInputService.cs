using System;
using UnityEngine;

namespace _Project.Code.Services.Input
{
    public interface IInputService
    {
        public event Action OnClicked;
        public event Action OnExit;
        public bool IsPointerOverUI();
        public Vector3 GetSelectedPosition();
    }
}