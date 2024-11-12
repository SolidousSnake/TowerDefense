using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace _Project.Code.Services.Input
{
    public class InputService : IInputService, ITickable
    {
        public event Action OnFire1;
        public event Action OnFire2;
        public event Action OnExit;
        
        public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
        public Vector3 GetSelectedPosition() => UnityEngine.Input.mousePosition;

        public void Tick() 
        {
            if(UnityEngine.Input.GetMouseButtonDown(0))
                OnFire1?.Invoke();
            
            if(UnityEngine.Input.GetMouseButtonDown(1))
                OnFire2?.Invoke();
            
            if(UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                OnExit?.Invoke();
        }
    }
}