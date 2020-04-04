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
        private readonly Dictionary<Type, IModel> _models = new Dictionary<Type, IModel>();

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
        }

        private void Start() => Initialize();

        #endregion


        #region IEntity

        public virtual void On() => gameObject.SetActive(true);

        public virtual void Off() => gameObject.SetActive(false);

        public TModel GetModel<TModel, TEntityInterface>()
            where TModel : Model<TEntityInterface>
            where TEntityInterface : class, IEntityInterface
        {
            return (TModel)_models[typeof(TModel)];
        }

        public TModel GetModel<TModel>()
            where TModel : IModel
        {
            return (TModel)_models[typeof(TModel)];
        }

        public IModel GetModel(Type modelType)
        {
            IModel model = null;
            _models.TryGetValue(modelType, out model);

            return model;
        }

        #endregion


        #region IInitializable

        public abstract void Initialize();

        #endregion


        #region Methods

        public void AddModel<TModel, TEntityInterface>(TModel model = null)
            where TModel : Model<TEntityInterface>
            where TEntityInterface : class, IEntityInterface
        {
            if (_models.ContainsKey(typeof(TModel)))
            {
                return;
            }

            if (model == null)
            {
                model = ScriptableObject.CreateInstance<TModel>();
            }

            model.SetOnwer(this);
            _models[typeof(TModel)] = model;
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