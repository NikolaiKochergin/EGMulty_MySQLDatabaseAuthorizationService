using System;
using System.Collections.Generic;
using System.Linq;
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
        private const string SelectedIDs = "selectedIDs";
        private const string GetDeckInfo = "getDeckInfo.php";
        private const string UpdateSelected = "updateSelected.php";
        
        private readonly INetworkService _network;
        private readonly IStaticDataService _staticData;
        private readonly IAuthorizationService _authorization;
        private readonly List<CardInfo> _selectedCards = new List<CardInfo>();
        private readonly List<CardInfo> _availableCards = new List<CardInfo>();

        private string _loadDeckURL;
        private string _updateDeckUrl;

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
            _loadDeckURL = _staticData.ForMainUrl() + GetDeckInfo;
            _updateDeckUrl = _staticData.ForMainUrl() + UpdateSelected;
            
            if(_authorization.IsAuthorized)
                LoadDeck();
            else
                _authorization.AuthorizationHappened += OnAuthorizationHappened;
        }

        public bool TrySelect(int cardId)
        {
            CardInfo card = _staticData.ForCard(cardId);

            if (_selectedCards.Contains(card) || _selectedCards.Count >= _staticData.ForHandCapacity())
                return false;
            
            _availableCards.Remove(card);
            _selectedCards.Add(card);
            return true;
        }

        public bool TryUnselect(int cardId)
        {
            CardInfo card = _staticData.ForCard(cardId);
            
            if (_selectedCards.Contains(card) == false) 
                return false;
            
            _selectedCards.Remove(card);
            _availableCards.Add(card);
            return true;
        }

        public void UpdateSelectedCards()
        {
            string ids = _selectedCards
                .Aggregate("[", (current, card) => current + $" {card.Id},")
                .TrimEnd(',') + " ]";

            Dictionary<string, string> data = new()
            {
                { SelectedIDs, ids },
            };

            _network.SendRequest(_updateDeckUrl, data, s => { Debug.Log("Success " + s);}, s => { Debug.Log("Error " + s);});
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

            _network.SendRequest(_loadDeckURL, data, OnSuccess, OnError);
        }

        private void OnSuccess(string data)
        {
            _selectedCards.Clear();
            _availableCards.Clear();
            
            DeckData deckData = JsonUtility.FromJson<DeckData>(data);

            foreach (AvailableCards card in deckData.availableCards)
            {
                if(int.TryParse(card.id, out int id) == false)
                    return;
                
                if(deckData.selectedIDs.Contains(card.id))
                    _selectedCards.Add(_staticData.ForCard(id));
                else
                    _availableCards.Add(_staticData.ForCard(id));
            }

            IsLoaded = true;
            Updated?.Invoke();
        }

        private void OnError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }
    }
}