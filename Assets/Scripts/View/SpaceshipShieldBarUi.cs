using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.View
{
    public sealed class SpaceshipShieldBarUi : BaseObjectScene
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
    }
}