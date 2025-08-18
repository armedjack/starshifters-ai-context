using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

namespace StarShifters.Core.Integration.CardHouse
{
    /// <summary>
    /// Компонент, который вешается на префаб карточки CardHouse.
    /// Держит ссылку на наш CardRuntime и обновляет визуал (иконка/название/стоимость).
    /// </summary>
    [DisallowMultipleComponent]
    public class CardHouseCardLink : MonoBehaviour
    {
        [Header("UI-элементы (назначь в префабе карт CardHouse)")]
        [SerializeField] private Image icon;
        [SerializeField] private MeshRenderer artRenderer;
        [SerializeField] private TextMeshPro title;
        [SerializeField] private TextMeshPro cost;
        [SerializeField] private TextMeshPro type;
        [SerializeField] private TextMeshPro description;

        [Header("Настройки шейдера арта")]
        [Tooltip("URP обычно '_BaseMap', Built-in обычно '_MainTex'")]
        [SerializeField] private string texturePropertyName = "_BaseMap";

        // Кэш и служебные поля
        public CardRuntime Runtime { get; private set; }
        private MaterialPropertyBlock _mpb;
        private string _resolvedTexProp; // реальное имя текстурного свойства в материале

        private static readonly string PROP_BASEMAP = "_BaseMap";
        private static readonly string PROP_MAINTEX = "_MainTex";

        private void Awake()
        {
            if (_mpb == null) _mpb = new MaterialPropertyBlock();
            ResolveTexturePropertyName(); // сразу определим корректный слот текстуры
        }

        /// <summary>Связать визуальную карточку с нашими данными.</summary>
        public void Bind(CardRuntime runtime)
        {
            Runtime = runtime;
            RefreshAll();
        }

        /// <summary>Обновить все визуальные поля из CardDef.</summary>
        public void RefreshAll()
        {
            var def = Runtime?.Def;

            if (def == null)
            {
                if (title) title.text = "<empty>";
                if (cost) cost.text = "-";
                if (type) type.text = string.Empty;
                if (description) description.text = string.Empty;

                // Фолбэк: очищаем 2D-иконку (если использовалась) и арт на 3D
                if (icon) icon.sprite = null;
                SetArtFromSprite(null);
                return;
            }

            // Тексты
            if (title) title.text = def.DisplayName ?? string.Empty;
            if (type) type.text = def.Type.ToString();
            if (cost) cost.text = def.EnergyCost.ToString();
            if (description) description.text = def.Description ?? string.Empty;

            // Загружаем спрайт из IconPath
            var sprite = LoadSprite(def.IconPath);

            // Обновляем 2D-иконку и 3D арт
            if (icon) icon.sprite = sprite;
            SetArtFromSprite(sprite);
        }

        private void SetArtFromSprite(Sprite sprite)
        {
            if (artRenderer == null)
                return;

            if (_mpb == null)
                _mpb = new MaterialPropertyBlock();

            if (string.IsNullOrEmpty(_resolvedTexProp))
                ResolveTexturePropertyName();

            artRenderer.GetPropertyBlock(_mpb);

            // Если спрайта нет — очистим слот текстуры, будет виден "чистый" материал (например, белый).
            Texture tex = sprite != null ? sprite.texture : null;
            _mpb.SetTexture(_resolvedTexProp, tex);

            artRenderer.SetPropertyBlock(_mpb);

            // По желанию можно скрывать/показывать MeshRenderer
            // artRenderer.enabled = (tex != null);
        }

        /// <summary>Загрузить спрайт по указанному пути.</summary>
        private Sprite LoadSprite(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var normalized = Path.ChangeExtension(path, null);
            return Resources.Load<Sprite>(normalized);
        }

        /// <summary>Определить имя текстурного свойства для текущего материала арта.</summary>
        private void ResolveTexturePropertyName()
        {
            _resolvedTexProp = texturePropertyName;

            var mat = artRenderer != null ? artRenderer.sharedMaterial : null;
            if (mat != null)
            {
                // Если руками заданный слот отсутствует — пробуем известные дефолты.
                if (!mat.HasProperty(_resolvedTexProp))
                {
                    if (mat.HasProperty(PROP_BASEMAP)) _resolvedTexProp = PROP_BASEMAP;
                    else if (mat.HasProperty(PROP_MAINTEX)) _resolvedTexProp = PROP_MAINTEX;
                    // Иначе остаёмся на заданном пользователем имени (на случай кастомного шейдера).
                }
            }
        }

        public void Bind1(CardRuntime runtime)
        {
            Runtime = runtime;
            var def = runtime?.Def;

            if (def == null)
            {
                if (title) title.text = "<empty>";
                if (cost) cost.text = "-";
                if (type) type.text = string.Empty;
                if (description) description.text = string.Empty;
                if (icon) icon.sprite = null;
                return;
            }

            var sprite = LoadSprite(def.IconPath);
            if (icon) icon.sprite = sprite;
            if (title) title.text = def.DisplayName;
            if (type) type.text = def.Type.ToString();
            if (cost) cost.text = def.EnergyCost.ToString();
            if (description) description.text = def.Description;
        }
    }
}
