using Assets.Scripts;
using Assets.Scripts.Tetrominoes;
using System.Collections;
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

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _tetromino.MoveLeft();
            if (_tetromino.CollisionX(_gameBoard))
                _tetromino.MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _tetromino.MoveRight();
            if (_tetromino.CollisionX(_gameBoard))
                _tetromino.MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _tetromino.RotateLeft(_gameBoard);
            //if (_tetromino.Collision(_gameBoard))
            //    _tetromino.RotateRight(_gameBoard);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _tetromino.RotateRight(_gameBoard);
            //if (_tetromino.Collision(_gameBoard))
            //    _tetromino.RotateLeft(_gameBoard);
        }

        if(_timer * 1 > _dropTime || (_timer * 1 > _quickDropTime && Input.GetKey(KeyCode.DownArrow)))
        {
            _tetromino.Y++;
            _tetromino.UpdateTilesPositions();
            _timer = 0;

            if (_tetromino.CollisionY(_gameBoard))
            {
                _tetromino.Y--;
                //TODO: If collision while dropping -> store position (with Y--) in game board and Spawn new tetromino
            }
        }
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
        _tetromino = new TetrominoI(NewTile, _panelWidth, _panelHeight, _tileWidth, _tileHeight);
    }
}
