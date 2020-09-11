using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.View
{
    /// <summary>
    /// Доработать класс, чтобы он отображал оставшееся время бафа.
    /// </summary>
    public sealed class RapidFireItemUi : BaseObjectScene
    {
        private Text _text;

        public float Text
        {
            set { _text.text = $"{value:0}"; }
        }

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<Text>();
        }

        public new void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }
    }
}