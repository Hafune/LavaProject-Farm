using System;
using Lib;
using UnityEngine;

public class SeedMenu : MonoConstruct
{
    [SerializeField] private GameObject _buttonContainer;

    private PlayerController _playerController;
    private PlantData _plantData;
    private Cell _selectedCell;

    protected override void Construct(IMyContext context)
    {
        _playerController = context.Resolve<PlayerController>();
    }

    private void Awake() => gameObject.SetActive(false);

    public void Open(Cell cell)
    {
        gameObject.SetActive(true);
        _buttonContainer.SetActive(true);
        _selectedCell = cell;
    }

    public void Closed()
    {
        gameObject.SetActive(false);
        _selectedCell.Unlock();
    }

    public void Selected(PlantData seedButton)
    {
        _plantData = seedButton;
        _buttonContainer.SetActive(false);
        _playerController.GoToPoint(_selectedCell.transform.position, Completed, Failed);
    }

    private void Completed()
    {
        gameObject.SetActive(false);
        _selectedCell.GrowSeed(_plantData);
    }

    static void Failed() => throw new Exception("Не удаётся найти путь к ячейке");
}