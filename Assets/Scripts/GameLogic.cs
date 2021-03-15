using Assets.Scripts;
using Assets.Scripts.Tetrominoes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    RectTransform _panelRT;
    GameObject _refTile;

    float _panelWidth;
    float _panelHeight;

    float _tileWidth;
    float _tileHeight;

    TetrisGameBoard _gameBoard;
    Tetromino _tetromino;

    float _dropTime = 1f;
    float _quickDropTime = .05f;
    float _timer = 0f;

    System.Random _random;

    // Start is called before the first frame update
    void Start()
    {
        _panelRT = (RectTransform)GetComponent("RectTransform");
        _refTile = (GameObject)Resources.Load("Tile");

        _panelWidth = _panelRT.sizeDelta.x;
        _panelHeight = _panelRT.sizeDelta.y;

        _tileWidth = _panelWidth / 10;
        _tileHeight = _panelHeight / 20;

        _gameBoard = new TetrisGameBoard(_panelWidth, _panelHeight, _tileWidth, _tileHeight);
        _random = new System.Random();

        SpawnNewTetromino();
        //DrawGameBoard();
        GameObject t1 = NewTile(Color.yellow);
        _gameBoard.Board[19, 0] = t1;
        _gameBoard.UpdateTilesPositions();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        bool merge = false;

        int x = _tetromino.X;
        int y = _tetromino.Y;
        TetrominoRotation rotation = _tetromino.Rotation;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _tetromino.X--;
            //_tetromino.MoveLeft();
            //if (_tetromino.Collision(_gameBoard))
            //    _tetromino.MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _tetromino.X++;
            //_tetromino.MoveRight();
            //if (_tetromino.Collision(_gameBoard))
            //    _tetromino.MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _tetromino.RotateLeft(_gameBoard);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _tetromino.RotateRight(_gameBoard);
        }

        if(_timer * 1 > _dropTime || (_timer * 1 > _quickDropTime && Input.GetKey(KeyCode.DownArrow)))
        {
            _tetromino.Y++;
            _timer = 0;

            if (_tetromino.Collision(_gameBoard))
                merge = true;
        }

        if (_tetromino.Collision(_gameBoard))
        {
            _tetromino.X = x;
            _tetromino.Y = y;
            _tetromino.Rotation = rotation;
        }

        if (merge)
        {
            _gameBoard.MergeTetromino(_tetromino);
            SpawnNewTetromino();
        }

        _tetromino.UpdateTilesPositions();
    }

    GameObject NewTile(Color color)
    {
        GameObject tile = Instantiate(_refTile, transform);
        RectTransform tileRT = (RectTransform)tile.GetComponent("RectTransform");
        tileRT.sizeDelta = new Vector2(_tileWidth, _tileHeight);
        tile.transform.localScale = new Vector2(0.9f, 0.9f);
        Image img = (Image)tile.GetComponent("Image");
        img.color = color;
        return tile;
    }

    void SpawnNewTetromino()
    {
        List<Action> list = new List<Action>()
        {
            SpawnI,
            SpawnJ,
            SpawnL,
            SpawnO,
            SpawnS,
            SpawnT,
            SpawnZ
        };

        list[_random.Next() % 7]();
    }

    void SpawnI()
    {
        _tetromino = new TetrominoI(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }

    void SpawnJ()
    {
        _tetromino = new TetrominoJ(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }

    void SpawnL()
    {
        _tetromino = new TetrominoL(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }

    void SpawnO()
    {
        _tetromino = new TetrominoO(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }

    void SpawnS()
    {
        _tetromino = new TetrominoS(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }

    void SpawnT()
    {
        _tetromino = new TetrominoT(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }

    void SpawnZ()
    {
        _tetromino = new TetrominoZ(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }
}
