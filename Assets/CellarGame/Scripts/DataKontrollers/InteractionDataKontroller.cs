using System.Collections.Generic;
using UnityEngine;
using Sadako;


namespace CellarGame
{
    public sealed class InteractionDataKontroller<T> : DataKontroller where T : System.Enum
    {
        #region Fields

        private readonly HashSet<T> _interactions = new HashSet<T>();

        #endregion


        #region ClassLifeCycle

        public InteractionDataKontroller(GameObject owner) : base(owner)
        { }

        #endregion


        #region Methods

        public void Add(T interaction) => _interactions.Add(interaction);

        public void Clear() => _interactions.Clear();

        public bool Contains(T interaction) => _interactions.Contains(interaction);

        #endregion
    }
}