using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.View
{
    public sealed class SpaceshipHealthBarUi : BaseObjectScene
    {
        private Image _bar;

        public float Fill
        {
            set { _bar.fillAmount = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            _bar = GetComponent<Image>();
        }

        public void SetColor(Color color)
        {
            _bar.color = color;
        }

        public new void SetActive(bool value)
        {
            _bar.gameObject.SetActive(value);
        }
    }
}