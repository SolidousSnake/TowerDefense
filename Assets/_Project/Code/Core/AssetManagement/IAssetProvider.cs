using UnityEngine;

namespace _Project.Code.Core.AssetManagement
{
    public interface IAssetProvider
    {
        public T Load<T>(string path) where T : Object;

        public T[] LoadAll<T>(string path) where T : Object;
    }
}