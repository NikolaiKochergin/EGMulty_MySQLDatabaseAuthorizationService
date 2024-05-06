using System;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck.StaticData
{
    [Serializable]
    public class CardInfo
    {
        [field: SerializeField, Delayed] public string Name;
        [field: SerializeField, Delayed] public string Id;
        [field: SerializeField] public Sprite Icon;
        [field: SerializeField, TextArea(minLines: 2, maxLines: 5), Delayed] public string Description;

#if UNITY_EDITOR
        public void Validate()
        {
            Id = Id.Trim(' ');
            Name = Name.Trim(' ');
            Description = Description.Trim(' ');
        }
#endif
    }
}