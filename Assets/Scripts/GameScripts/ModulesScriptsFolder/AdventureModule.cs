using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventureModule : Module
{
    #region Fields
    //[SerializeField] private TextMeshProUGUI[] _gridElements;
    [SerializeField] private Transform _controls, _gameSection;
    [SerializeField] private int _columns = 7;
    [SerializeField] private int _rows = 6;
    //[SerializeField] private Button[] _navigationButtons;
    #endregion

    #region Variables
    private TextMeshProUGUI[] _gridElements;
    private Button[] _navigationButtons;
    private int[,] _gridNumbers;
    #endregion

    #region Unity-API
    private void Awake()
    {
        _gridNumbers = new int[_rows, _columns];

        _gridElements = _gameSection.GetComponentsInChildren<TextMeshProUGUI>();
        _navigationButtons = _controls.GetComponentsInChildren<Button>();

        GenerateRandomNumbers();
        UpdateGridDisplay();
    }
    #endregion

    #region Logic
    private void GenerateRandomNumbers()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < _rows * _columns; i++)
            indices.Add(i);

        ShuffleList(indices);
        HashSet<int> onesIndices = new HashSet<int>(indices.GetRange(0, 6));

        for (int y = 0; y < _rows; y++)
        {
            for (int x = 0; x < _columns; x++)
            {
                int index = y * _columns + x;
                _gridNumbers[y, x] = onesIndices.Contains(index) ? 1 : 0;
            }
        }
    }

    private void UpdateGridDisplay()
    {
        for (int y = 0; y < _rows; y++)
        {
            for (int x = 0; x < _columns; x++)
            {
                _gridElements[y * _columns + x].text = _gridNumbers[y, x].ToString();
            }
        }
    }

    public void SpreadNumbers(string direction)
    {
        switch (direction)
        {
            case "Right":
                for (int y = 0; y < _rows; y++)
                {
                    for (int x = _columns - 2; x >= 0; x--)
                    {
                        if (_gridNumbers[y, x] == 1)
                        {
                            if (_gridNumbers[y, x + 1] == 0)
                            {
                                _gridNumbers[y, x + 1] = 1;
                            }
                            else if (_gridNumbers[y, x + 1] == 1)
                            {
                                _gridNumbers[y, x + 1] = 0;
                            }
                        }
                    }
                }
                break;
            case "Left":
                for (int y = 0; y < _rows; y++)
                {
                    for (int x = 1; x < _columns; x++)
                    {
                        if (_gridNumbers[y, x] == 1)
                        {
                            if (_gridNumbers[y, x - 1] == 0)
                            {
                                _gridNumbers[y, x - 1] = 1;
                            }
                            else if (_gridNumbers[y, x - 1] == 1)
                            {
                                _gridNumbers[y, x - 1] = 0;
                            }
                        }
                    }
                }
                break;
            case "Down":
                for (int y = _rows - 2; y >= 0; y--)
                {
                    for (int x = 0; x < _columns; x++)
                    {
                        if (_gridNumbers[y, x] == 1)
                        {
                            if (_gridNumbers[y + 1, x] == 0)
                            {
                                _gridNumbers[y + 1, x] = 1;
                            }
                            else if (_gridNumbers[y + 1, x] == 1)
                            {
                                _gridNumbers[y + 1, x] = 0;
                            }
                        }
                    }
                }
                break;
            case "Up":
                for (int y = 1; y < _rows; y++)
                {
                    for (int x = 0; x < _columns; x++)
                    {
                        if (_gridNumbers[y, x] == 1)
                        {
                            if (_gridNumbers[y - 1, x] == 0)
                            {
                                _gridNumbers[y - 1, x] = 1;
                            }
                            else if (_gridNumbers[y - 1, x] == 1)
                            {
                                _gridNumbers[y - 1, x] = 0;
                            }
                        }
                    }
                }
                break;
        }
        UpdateGridDisplay();
        CheckFullRowsAndColumns();
    }

    public void CheckFullRowsAndColumns()
    {
        bool isFull;
        for (int y = 0; y < _rows; y++)
        {
            isFull = true;
            for (int x = 0; x < _columns; x++)
            {
                if (_gridNumbers[y, x] != 1)
                {
                    isFull = false;
                    break;
                }
            }
            if (isFull)
            {
                DisableThisModule();
                return;
            }
        }

        for (int x = 0; x < _columns; x++)
        {
            isFull = true;
            for (int y = 0; y < _rows; y++)
            {
                if (_gridNumbers[y, x] != 1)
                {
                    isFull = false;
                    break;
                }
            }
            if (isFull)
            {
                DisableThisModule();
                return;
            }
        }
    }


    private void DisableThisModule()
    {
        active = true;
        Bomb.instance.CheckIfBombIsDefused();

        foreach (var btn in _navigationButtons) btn.interactable = false;

        foreach(var element in _gridElements) Destroy(element.gameObject);
    }

    #endregion
}