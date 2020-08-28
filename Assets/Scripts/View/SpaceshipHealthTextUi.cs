using Assets.Scripts.Model;
using UnityEngine.UI;


namespace Assets.Scripts.View
{
    public sealed class SpaceshipHealthTextUi : BaseObjectScene
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