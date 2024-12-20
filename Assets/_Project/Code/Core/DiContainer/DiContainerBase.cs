﻿using System.Linq;
using _Project.Code.Utils;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Core.DiContainer
{
    public abstract class DiContainerBase : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            AddDependencies(builder);
            // FindAutoInjectObject();
        }

        protected abstract void AddDependencies(IContainerBuilder builder);

        private void FindAutoInjectObject()
        {
            var objects = FindObjectsOfType<AutoInject>(true)
                .Select(x => x.gameObject).ToList();
            autoInjectGameObjects = objects;
        }
    }
}