using System;
using Lib;
using UnityEngine;

public class PlantMenu : MonoConstruct
{
    [SerializeField] private GameObject _buttonContainer;
    [SerializeField] private AbstractPlantMenuButton[] _buttons;

    private PlayerController _playerController;
    private AbstractPlant _selectedPlant;
    private Action<AbstractPlant> _buttonAction;

    protected override void Construct(IMyContext context) => _playerController = context.Resolve<PlayerController>();

    private void Awake() => gameObject.SetActive(false);

    public void Open(AbstractPlantMenuButton actionButton, AbstractPlant abstractPlant)
    {
        bool hasOpened = false;

        _buttons.ForEach(button =>
        {
            button.gameObject.SetActive(button == actionButton);
            hasOpened = hasOpened || button == actionButton;
        });

        if (!hasOpened)
            return;

        _selectedPlant = abstractPlant;
        gameObject.SetActive(true);
        _buttonContainer.SetActive(true);
    }

    public void Close() => gameObject.SetActive(false);

    public void Selected(Action<AbstractPlant> buttonAction)
    {
        _buttonAction = buttonAction;
        _buttonContainer.SetActive(false);
        _playerController.GoToPoint(_selectedPlant.transform.position, Completed, Failed);
    }

    private void Completed()
    {
        gameObject.SetActive(false);
        _buttonAction.Invoke(_selectedPlant);
    }

    static void Failed() => throw new Exception("Не удаётся найти путь к ячейке");
}