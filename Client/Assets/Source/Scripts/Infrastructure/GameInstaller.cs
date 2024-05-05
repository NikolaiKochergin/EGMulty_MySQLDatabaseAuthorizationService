using Reflex.Core;
using Source.Scripts.Services.StaticData;
using Source.Scripts.UI.Factory;
using Source.Scripts.UI.Services.Windows;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder
                .AddSingleton(typeof(UIFactory), typeof(IUIFactory))
                .AddSingleton(typeof(WindowService), typeof(IWindowService))
                .AddSingleton(typeof(StaticDataService), typeof(IStaticDataService));
    }
}