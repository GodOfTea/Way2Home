using Infrastructure.Services;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.State;

namespace Game
{
    public class QuitGameplayButton : GameplayMenuButton
    {
        protected override void OnClicked()
        {
            base.OnClicked();
            var progressesService = AllServices.Container.Single<IPersistentProgressesService>();
            AllServices.Container.Single<IStateSwitcher>().Enter<LoadMainMenuState, string>(
                progressesService.GameProgress.MainMenuScene);
        }
            
    }
}