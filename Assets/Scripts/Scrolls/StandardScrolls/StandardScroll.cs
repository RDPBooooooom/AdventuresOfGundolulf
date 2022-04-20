namespace Scrolls.StandardScrolls
{
    public abstract class StandardScroll : Scroll
    {
        protected UI.InGameUI inGameUI = Managers.GameManager.Instance.UIManager.MainCanvas.GetComponent<UI.InGameUI>();
    }
}