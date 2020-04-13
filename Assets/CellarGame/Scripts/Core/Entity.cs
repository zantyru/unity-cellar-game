using System;
using System.Collections.Generic;
using UnityEngine;


namespace CellarGame
{
    public abstract class Entity : MonoBehaviour, IInitializable, ICleanUpable, IHavePowerSwitch
    {
        #region Fields

        private bool _isVisible;
        private int _layer;
        private Color _color;
        private Transform _transfom;
        private readonly Dictionary<Type, Model> _models = new Dictionary<Type, Model>();

        #endregion


        #region Properties IHavePowerSwitch

        public bool IsEnabled => gameObject.activeSelf;

        #endregion


        #region Properties

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
            get => _transfom.name;
            set => _transfom.name = value;
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
        public Vector3 Position
        {
            get => _transfom.position;
            set => _transfom.position = value;
        }
        public Quaternion Rotation
        {
            get => _transfom.rotation;
            set => _transfom.rotation = value;
        }
        public Vector3 Scale
        {
            get => _transfom.localScale;
            set => _transfom.localScale = value;
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _transfom = GetComponent<Transform>();
            
            _layer = gameObject.layer;
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
        }

        private void Start() => Initialize();

        #endregion


        #region IHavePowerSwitch

        public virtual void On() => gameObject.SetActive(true);

        public virtual void Off() => gameObject.SetActive(false);

        #endregion


        #region IInitializable

        public abstract void Initialize();

        #endregion


        #region ICleanUpable

        public virtual void CleanUp()
        {
            foreach (Model model in _models.Values)
            {
                model.CleanUp();
            }

            _models.Clear();
        }

        #endregion


        #region Methods

        public bool AddModel<T>(T model)
            where T : Model
        {
            if ((model == null) || (_models.ContainsKey(typeof(T))))
            {
                return false;
            }

            _models[typeof(T)] = model;

            return true;
        }

        public bool RemoveModel<T>()
            where T : Model
        {
            return _models.Remove(typeof(T));
        }

        public bool HasModel<T>()
            where T : Model
        {
            return _models.ContainsKey(typeof(T));
        }

        public T GetModel<T>()
            where T : Model
        {
            return (T)_models[typeof(T)];
        }

        public T FetchComponent<T>(out bool isJustCreated)
            where T : UnityEngine.Component
        {
            T component;
            isJustCreated = false;

            if (TryGetComponent<T>(out var foundComponent))
            {
                component = foundComponent;
            }
            else
            {
                component = gameObject.AddComponent<T>();
                isJustCreated = true;
            }

            return component;
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