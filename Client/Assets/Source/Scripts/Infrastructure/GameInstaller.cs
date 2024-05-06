using Reflex.Core;
using Source.Scripts.GameCore.Deck.Service;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.Infrastructure.Services.Network;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.UI.Factory;
using Source.Scripts.UI.Services.Windows;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            Container container = builder
                .AddSingleton(typeof(UIFactory), typeof(IUIFactory))
                .AddSingleton(typeof(WindowService), typeof(IWindowService))
                .AddSingleton(typeof(StaticDataService), typeof(IStaticDataService))
                .AddSingleton(typeof(NetworkService), typeof(INetworkService))
                .AddSingleton(typeof(AuthorizationService), typeof(IAuthorizationService))
                .AddSingleton(typeof(DeckService), typeof(IDeckService))
                .Build();
            
            container.Single<IStaticDataService>().Load();
            container.Single<IAuthorizationService>().Initialize();
            container.Single<IDeckService>().Initialize();
        }
    }
}