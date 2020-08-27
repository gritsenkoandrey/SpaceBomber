using Assets.Scripts.Interface;


namespace Assets.Scripts.Controller
{
    public sealed class ScoreController : BaseController, IExecute
    {
        public void Execute()
        {
            uiInterface.ScoreUI.ShowScore();
        }
    }
}