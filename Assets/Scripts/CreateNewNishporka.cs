using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewNishporka : MonoBehaviour
{
    [SerializeField] private GameObject _nishporkaPrefab;

    private int _currentNumberOfArtefacts = 3;
    private Vector3 _nishporkaTerritoryPosition;

    private void Start()
    {
        _nishporkaTerritoryPosition = transform.position;
        _nishporkaTerritoryPosition.y += 2;
    }

    private void Update()
    {
        if (Nishporka._NumberOfArtefacts >= _currentNumberOfArtefacts) CreateNishporka();  //створюємо Нишпорку, якщо знайдено достатньо артефактів
    }
    
    private void CreateNishporka()
    {
        Instantiate(_nishporkaPrefab, _nishporkaTerritoryPosition, Quaternion.identity);
        Nishporka._NumberOfArtefacts -= _currentNumberOfArtefacts;
    }
}
