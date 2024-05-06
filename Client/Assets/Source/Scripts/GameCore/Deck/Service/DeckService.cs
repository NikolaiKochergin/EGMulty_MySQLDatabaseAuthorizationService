using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.Deck.Data;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.Infrastructure.Services.Network;
using Source.Scripts.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck.Service
{
    public class DeckService : IDeckService
    {
        private const string UserID = "userID";
        private const string GetDeckInfo = "getDeckInfo.php";
        
        private readonly INetworkService _network;
        private readonly IStaticDataService _staticData;
        private readonly IAuthorizationService _authorization;
        private readonly List<CardInfo> _selectedCards = new List<CardInfo>();
        private readonly List<CardInfo> _availableCards = new List<CardInfo>();

        private string _deckURL;
        
        public DeckService(INetworkService network, IStaticDataService staticData, IAuthorizationService authorization)
        {
            _network = network;
            _staticData = staticData;
            _authorization = authorization;
        }
        
        public bool IsLoaded { get; private set; }
        public event Action Updated;

        public IReadOnlyList<CardInfo> SelectedCards => _selectedCards;
        public IReadOnlyList<CardInfo> AvailableCards => _availableCards;

        public void Initialize()
        {
            _deckURL = _staticData.ForMainUrl() + GetDeckInfo;
            
            if(_authorization.IsAuthorized)
                LoadDeck();
            else
                _authorization.AuthorizationHappened += OnAuthorizationHappened;
        }

        private void OnAuthorizationHappened()
        {
            _authorization.AuthorizationHappened -= OnAuthorizationHappened;
            LoadDeck();
        }

        private void LoadDeck()
        {
            Dictionary<string, string> data = new()
            {
                { UserID, _authorization.UserId.ToString() },
            };

            _network.SendRequest(_deckURL, data, OnSuccess, OnError);
        }

        private void OnSuccess(string data)
        {
            DeckData deckData = JsonUtility.FromJson<DeckData>(data);

            foreach (string selectedID in deckData.selectedIDs)
                if (int.TryParse(selectedID, out int id)) 
                    _selectedCards.Add(_staticData.ForCard(id));

            foreach (AvailableCards card in deckData.availableCards)
                if(int.TryParse(card.id, out int id))
                    _availableCards.Add(_staticData.ForCard(id));

            foreach (CardInfo card in _selectedCards) 
                _availableCards.Remove(card);

            IsLoaded = true;
            Updated?.Invoke();
        }

        private void OnError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }
    }
}