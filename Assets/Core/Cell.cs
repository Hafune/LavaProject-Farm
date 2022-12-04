using System.Collections;
using Core.Views;
using DG.Tweening;
using Lib;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoConstruct, IPointerClickHandler
{
    [SerializeField] private ImageProgressBar _progressBar;
    [SerializeField] private FocusBehavior _focusBehavior;

    private IMyContext _context;
    private SeedMenu _seedMenu;
    private Storage _storage;
    private bool _isBusy;
    private CoroutineFloatIntValueFollower _valueCoroutine;

    protected override void Construct(IMyContext context)
    {
        _context = context;
        _seedMenu = context.Resolve<SeedMenu>();
        _storage = context.Resolve<Storage>();
        _valueCoroutine = new CoroutineFloatIntValueFollower(this);
        _valueCoroutine.SetOnChange(value => _progressBar.SetPercent(value));
        _progressBar.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isBusy)
            return;

        _isBusy = true;
        _seedMenu.Open(this);
    }

    public void Unlock() => _isBusy = false;

    public void GrowSeed(PlantData plantData)
    {
        var plant = _context.Instantiate(plantData.PlantPrefab, transform);
        var startScale = plant.transform.localScale;
        plant.transform.localPosition = Vector3.zero;
        plant.transform.localScale = Vector3.zero;
        plant.SetCell(this);

        plant.transform.DOScale(startScale, plantData.GrowTime).OnComplete(plant.Activate);
        _focusBehavior.BeginFocus();
        
        _progressBar.gameObject.SetActive(true);
        _valueCoroutine.Speed = 1 / plantData.GrowTime;
        _valueCoroutine.Currentvalue = 0;
        _valueCoroutine.OnComplete(() =>
        {
            _progressBar.gameObject.SetActive(false);
            _storage.AddExperience(plantData.GrowTime);
        });
        _valueCoroutine.StartFollowFor(1);
    }
}