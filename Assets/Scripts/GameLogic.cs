using Assets.Scripts;
using Assets.Scripts.Tetrominoes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    Text ScoreText;

    [SerializeField]
    Text LevelText;

    [SerializeField]
    GameObject NextTetrominoPanel;

    RectTransform _panelRT;
    GameObject _refTile;

    float _panelWidth;
    float _panelHeight;

    float _tileWidth;
    float _tileHeight;

    TetrisGameBoard _gameBoard;
    Tetromino _tetromino;
    bool _gameover = false;

    float _dropTime = 1f;
    float _quickDropTime = .05f;
    float _moveTime = .1f;
    float _moveTimer = 0f;
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

        _gameover = false;

        SpawnNewTetromino();
        GameObject test = NewTile2(Color.red);
        test.transform.localPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Update timers
        _timer += Time.deltaTime;
        _moveTimer += Time.deltaTime;

        // Backup tetromino location and rotation
        int x = _tetromino.X;
        int y = _tetromino.Y;
        TetrominoRotation rotation = _tetromino.Rotation;

        bool merge = false;

        // If Collision without action -> gameover
        if (_tetromino.Collision(_gameBoard))
            _gameover = true;

        if (_gameover)
            return;

        // Left movement
        if (_moveTimer > _moveTime && Input.GetKey(KeyCode.LeftArrow))
        {
            _tetromino.X--;
            _moveTimer = 0;
        }

        // Right movement
        if (_moveTimer > _moveTime && Input.GetKey(KeyCode.RightArrow))
        {
            _tetromino.X++;
            _moveTimer = 0;
        }

        // Counter-clockwise rotation
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _tetromino.RotateLeft(_gameBoard);
        }

        // Clockwise rotation
        if (Input.GetKeyDown(KeyCode.W))
        {
            _tetromino.RotateRight(_gameBoard);
        }

        // Tetromino fall
        if(_timer * 1 > _dropTime || (_timer * 1 > _quickDropTime && Input.GetKey(KeyCode.DownArrow)))
        {
            _tetromino.Y++;
            _timer = 0;

            if (_tetromino.Collision(_gameBoard))
                merge = true;
        }

        // If collision while moving or falling, revert the last movement
        if (_tetromino.Collision(_gameBoard))
        {
            _tetromino.X = x;
            _tetromino.Y = y;
            _tetromino.Rotation = rotation;
        }

        // If tetromino collision because of the fall, merge it into the 
        // gameboard and spawn a new one
        if (merge)
        {
            _gameBoard.MergeTetromino(_tetromino);
            _gameBoard.UpdateTilesPositions();

            List<int> fullLines = _gameBoard.CheckFullLines();
            if (fullLines.Count > 0)
                _gameBoard.RemoveLines(fullLines);

            SpawnNewTetromino();
        }

        // Update tetromino tiles on screen
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

    GameObject NewTile2(Color color)
    {
        GameObject tile = Instantiate(_refTile, NextTetrominoPanel.transform);
        RectTransform tileRT = (RectTransform)tile.GetComponent("RectTransform");
        tileRT.sizeDelta = new Vector2(20, 20);
        tile.transform.localScale = new Vector2(1f, 1f);
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
