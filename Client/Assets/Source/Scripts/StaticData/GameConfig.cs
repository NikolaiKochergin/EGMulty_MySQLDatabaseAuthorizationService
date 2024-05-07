using System.Collections.Generic;
using System.Linq;
using Source.Scripts.GameCore.Deck.StaticData;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Static Data/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private string _mainUrl;
        [SerializeField, Min(0)] private int _handCapacity = 5;
        [SerializeField] private List<CardInfo> _cards;

        public string MainUrl => _mainUrl;
        public int HandCapacity => _handCapacity;
        public IEnumerable<CardInfo> Cards => _cards;

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (CardInfo card in _cards) 
                card.Validate();
            
            List<int> duplicates = _cards
                .GroupBy(x => x.Id)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            
            if (duplicates.Count <= 0) 
                return;

            string duplicateIds = duplicates
                .Aggregate(string.Empty, (current, duplicate) => current + $" {duplicate},")
                .TrimEnd(',');

            Debug.LogError("Cards List has duplicates with ids:" + duplicateIds);
        }
#endif
    }
}