using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        [Inject]
        private void Construct(Container container) => 
            _game = new Game(container);

        private void Start() => 
            _game.Start();
    }
}