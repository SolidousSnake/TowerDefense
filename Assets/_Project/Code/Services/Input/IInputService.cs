﻿using System;

namespace _Project.Code.Services.Input
{
    public interface IInputService
    {
        public event Action OnClicked;
        public event Action OnExit;
    }
}