using Lib;
using UnityEngine;

public class Greed : MonoConstruct
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Vector2 _size;

    private IMyContext _context;

    protected override void Construct(IMyContext context) => _context = context;

    private void Awake()
    {
        for (int x = 0; x < _size.x; x++)
        for (int y = 0; y < _size.y; y++)
            _context.Instantiate(_cellPrefab, transform).transform.localPosition = new Vector3(x, 0, y);
    }
}