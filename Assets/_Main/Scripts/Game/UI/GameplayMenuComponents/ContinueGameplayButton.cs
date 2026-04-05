namespace Game
{
    public class ContinueGameplayButton : GameplayMenuButton
    {
        protected override void OnClicked()
        {
            base.OnClicked();
            _menuScreen.Hide();
        }
    }
}