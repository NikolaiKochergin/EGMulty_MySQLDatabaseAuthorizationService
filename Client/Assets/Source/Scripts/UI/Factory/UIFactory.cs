using System;
using Reflex.Core;
using Reflex.Injectors;
using Source.Scripts.Extensions;
using Source.Scripts.Services.StaticData;
using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly Container _container;
        
        private Transform _uiRoot;

        public UIFactory(IStaticDataService staticData, Container container)
        {
            _staticData = staticData;
            _container = container;
        }

        public void CreateUIRoot()
        {
            UIRoot prefab = _staticData.ForUIRoot();

            if (prefab == null) 
                throw new NullReferenceException("UIRoot reference not set in UIConfig.");
            
            _uiRoot = InstantiateAndInject(prefab).transform;
        }

        public WindowBase CreateWindow(WindowId id)
        {
            WindowBase prefab = _staticData.ForWindow(id);
            
            if(prefab == null)
                throw new ArgumentException($"Window Config with id: {id} not found");
                
            return InstantiateAndInject(prefab, _uiRoot);
        }

        public void Cleanup()
        {
            if(_uiRoot != null)
                _uiRoot.Destroy();
        }

        private T InstantiateAndInject<T>(T prefab, Transform parent = null) where T : Component
        {
            T instance = Object.Instantiate(prefab, parent);
            
            GameObjectInjector.InjectRecursive(instance.gameObject, _container);

            return instance;
        }
    }
}