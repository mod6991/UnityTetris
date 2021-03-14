using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetrominoes
{
    public class TetrominoI : Tetromino
    {
        private static Color _color = new Color(0, 1, 1, 1);

        public TetrominoI(NewTileDelegate del, float panelWidth, float panelHeight,
                                               float tileWidth, float tileHeight)
            : base(panelWidth, panelHeight, tileWidth, tileHeight)
        {
            X = 3;
            Y = -1;
            Rotation = TetrominoRotation.Initial;
            Tiles = new GameObject[4];
            Tiles[0] = del(Color);
            Tiles[1] = del(Color);
            Tiles[2] = del(Color);
            Tiles[3] = del(Color);

            UpdateTilesPositions();
        }

        public override int GridSize
        {
            get { return 4; }
        }

        public override Color Color
        {
            get { return _color; }
        }

        public override void RotateLeft(TetrisGameBoard gameBoard)
        {
            float x = X;
            float y = Y;

            TetrominoRotation previousR = Rotation;
            int[,] wkd = WKDLeftI[previousR];

            switch (Rotation)
            {
                case TetrominoRotation.Initial:
                    Rotation = TetrominoRotation.Left;
                    break;
                case TetrominoRotation.Right:
                    Rotation = TetrominoRotation.Initial;
                    break;
                case TetrominoRotation.Twice:
                    Rotation = TetrominoRotation.Right;
                    break;
                case TetrominoRotation.Left:
                    Rotation = TetrominoRotation.Twice;
                    break;
            }

            bool success = false;

            for (int i = 0; i < 5; i++)
            {
                X = x + wkd[i, 0];
                Y = y - wkd[i, 1];
                if (!Collision(gameBoard))
                {
                    Debug.Log($"success with {wkd[i, 0]}, {wkd[i, 1]}");
                    success = true;
                    break;
                }
            }

            if (!success)
            {
                Debug.Log("rotate failed");
                Rotation = previousR;
            }
            
            UpdateTilesPositions();
        }

        public override void RotateRight(TetrisGameBoard gameBoard)
        {
            switch (Rotation)
            {
                case TetrominoRotation.Initial:
                    Rotation = TetrominoRotation.Right;
                    break;
                case TetrominoRotation.Right:
                    Rotation = TetrominoRotation.Twice;
                    break;
                case TetrominoRotation.Twice:
                    Rotation = TetrominoRotation.Left;
                    break;
                case TetrominoRotation.Left:
                    Rotation = TetrominoRotation.Initial;
                    break;
            }

            UpdateTilesPositions();
        }

        public override void UpdateTilesPositions()
        {
            float x;
            float y;

            switch (Rotation)
            {
                case TetrominoRotation.Initial:
                    Grid = new int[4, 4]
                    {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 0) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 1) * TileHeight;
                    Tiles[0].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 1) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 1) * TileHeight;
                    Tiles[1].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 2) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 1) * TileHeight;
                    Tiles[2].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 3) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 1) * TileHeight;
                    Tiles[3].transform.localPosition = new Vector2(x, y);

                    break;
                case TetrominoRotation.Right:
                    Grid = new int[4, 4]
                    {
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 }
                    };

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 2) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 0) * TileHeight;
                    Tiles[0].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 2) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 1) * TileHeight;
                    Tiles[1].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 2) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 2) * TileHeight;
                    Tiles[2].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 2) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 3) * TileHeight;
                    Tiles[3].transform.localPosition = new Vector2(x, y);

                    break;
                case TetrominoRotation.Twice:
                    Grid = new int[4, 4]
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 }
                    };

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 0) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 2) * TileHeight;
                    Tiles[0].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 1) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 2) * TileHeight;
                    Tiles[1].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 2) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 2) * TileHeight;
                    Tiles[2].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 3) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 2) * TileHeight;
                    Tiles[3].transform.localPosition = new Vector2(x, y);

                    break;
                case TetrominoRotation.Left:
                    Grid = new int[4, 4]
                    {
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 }
                    };

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 1) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 0) * TileHeight;
                    Tiles[0].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 1) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 1) * TileHeight;
                    Tiles[1].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 1) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 2) * TileHeight;
                    Tiles[2].transform.localPosition = new Vector2(x, y);

                    x = -PanelWidth / 2 + TileWidth / 2 + (X + 1) * TileWidth;
                    y = PanelHeight / 2 - TileHeight / 2 - (Y + 3) * TileHeight;
                    Tiles[3].transform.localPosition = new Vector2(x, y);

                    break;
            }
        }
        
        public override bool Collision(TetrisGameBoard gameBoard)
        {
            return false;
        }
    }
}
