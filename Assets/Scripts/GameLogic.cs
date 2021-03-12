using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    RectTransform _panelRT;
    GameObject _refTile;

    float _panelWidth;
    float _panelHeight;

    float _tileWidth;
    float _tileHeight;

    TetrisGameBoard GameBoard;

    // Start is called before the first frame update
    void Start()
    {
        _panelRT = (RectTransform)GetComponent("RectTransform");
        _refTile = (GameObject)Resources.Load("Tile");

        _panelWidth = _panelRT.sizeDelta.x;
        _panelHeight = _panelRT.sizeDelta.y;

        _tileWidth = _panelWidth / 10;
        _tileHeight = _panelHeight / 20;

        NewTile(new Vector2(-_panelWidth / 2, _panelHeight / 2));
        GameBoard = new TetrisGameBoard();
        DrawGameBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

        }
        if (Input.GetKey(KeyCode.Q))
        {

        }
        if (Input.GetKey(KeyCode.W))
        {

        }
    }

    void NewTile(Vector2 vect)
    {
        GameObject tile = Instantiate(_refTile, transform);
        RectTransform tileRT = (RectTransform)tile.GetComponent("RectTransform");
        tileRT.sizeDelta = new Vector2(_tileWidth, _tileHeight);
        tile.transform.localPosition = vect;
        //tile.transform.localScale = new Vector2(0.95f, 0.95f);
    }

    void DrawGameBoard()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                NewTile(new Vector2(-_panelWidth / 2 + _tileWidth / 2 + j * _tileWidth,
                                    _panelHeight / 2 - _tileHeight / 2 - i * _tileHeight));
            }
        }
    }
}
