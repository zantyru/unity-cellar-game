using System;
using System.Collections.Generic;
using UnityEngine;


namespace CellarGame
{
    public abstract class Entity : MonoBehaviour, IEntity, IInitializable
    {
        #region Fields

        private bool _isEnabled;
        private bool _isVisible;
        private int _layer;
        private Color _color;
        private Transform _transfom;
        private readonly Dictionary<Type, Model> _models = new Dictionary<Type, Model>();

        #endregion


        #region Properties IEntity

        public bool IsEnabled => _isEnabled;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                PropagateVisibility(_transfom);
            }   
        }
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                PropagateLayer(_transfom);
            }
        }
        public string Name
        {
            get => Transform.name;
            set => Transform.name = value;
        }
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                PropagateColor(_transfom);
            }
        }
        public Transform Transform => _transfom;

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            _transfom = GetComponent<Transform>();
            
            _layer = gameObject.layer;
            _isEnabled = true;
            _isVisible = false;
            _color = Color.white;

            if (TryGetComponent<Renderer>(out var renderer))
            {
                _isVisible = renderer.enabled;
                if (renderer.material != null)
                {
                    _color = renderer.material.color;
                }
            }
            
            //Initialize();
        }

        private void Start() => Initialize();

        #endregion


        #region IEntity

        public virtual void On() => gameObject.SetActive(true);

        public virtual void Off() => gameObject.SetActive(false);

        public T GetModel<T>() where T : Model => (T)_models[typeof(T)];

        #endregion


        #region IInitializable

        public abstract void Initialize();

        #endregion


        #region Methods

        public void AddModel<T>(T model = null) where T : Model
        {
            if (model == null)
            {
                if (!_models.ContainsKey(typeof(T)))
                {
                    model = ScriptableObject.CreateInstance<T>();
                }
            }

            Type modelType = model.GetType();

            if (!_models.ContainsKey(modelType))
            {
                model.SetOwner(this);
                _models[modelType] = model;

                WorldProcessor.RegisterModel(model);
            }
        }

        private void PropagateLayer(Transform rootTransform)
        {
            rootTransform.gameObject.layer = _layer;
            foreach (Transform childTransform in rootTransform)
            {
                PropagateLayer(childTransform);
            }
        }

        private void PropagateVisibility(Transform rootTransform)
        {
            if (rootTransform.TryGetComponent<Renderer>(out var renderer))
			{
                renderer.enabled = _isVisible;
            }
            foreach (Transform childTransform in rootTransform)
            {
                PropagateVisibility(childTransform);
            }
        }

        private void PropagateColor(Transform rootTransform)
        {
            if (rootTransform.TryGetComponent<Renderer>(out var renderer))
			{
				foreach (var currentMaterial in renderer.materials)
				{
					currentMaterial.color = _color;
				}
			}
			foreach (Transform childTransform in rootTransform)
			{
				PropagateColor(childTransform);
			}
        }

        #endregion
    }
}