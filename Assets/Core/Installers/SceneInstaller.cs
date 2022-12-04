using Core.Views;
using Lib;
using Reflex;
using Reflex.Scripts;
using UnityEngine;

public class SceneInstaller : Installer
{
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private GameObject _uiModule;

    public override void InstallBindings(Container container)
    {
        var playerController = Instantiate(_playerPrefab);
        playerController.Warp(_playerStartPosition.position);

        var camera = FindObjectOfType<Camera>();
        var uiCamera = FindObjectOfType<UICameraMarker>();
        var seedMenu = _uiModule.GetComponentInChildren<SeedMenu>(true);
        var plantMenu = _uiModule.GetComponentInChildren<PlantMenu>(true);
        var plantMenuButtonDrop = _uiModule.GetComponentInChildren<PlantMenuButtonDrop>(true);
        var plantMenuButtonCollect = _uiModule.GetComponentInChildren<PlantMenuButtonCollect>(true);
        var experienceView = _uiModule.GetComponentInChildren<ExperienceView>(true);
        var carrotsView = _uiModule.GetComponentInChildren<CarrotsView>(true);

        container.BindInstanceAs(camera);
        container.BindInstanceAs(uiCamera);
        container.BindInstanceAs(playerController);
        container.BindInstanceAs(seedMenu);
        container.BindInstanceAs(plantMenu);
        container.BindInstanceAs(plantMenuButtonDrop);
        container.BindInstanceAs(plantMenuButtonCollect);
        container.BindInstanceAs(experienceView);
        container.BindInstanceAs(carrotsView);
        container.BindInstanceAs(new Storage(carrotsView, experienceView));

        container.BindInstanceAs(new MyContext(container) as IMyContext);
    }
}