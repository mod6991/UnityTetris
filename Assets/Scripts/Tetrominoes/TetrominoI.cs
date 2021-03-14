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

        public override TetrominoRotation Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;

                switch (_rotation)
                {
                    case TetrominoRotation.Initial:
                        Grid = new int[4, 4]
                        {
                            { 0, 0, 0, 0 },
                            { 1, 1, 1, 1 },
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 }
                        };
                        break;
                    case TetrominoRotation.Right:
                        Grid = new int[4, 4]
                        {
                            { 0, 0, 1, 0 },
                            { 0, 0, 1, 0 },
                            { 0, 0, 1, 0 },
                            { 0, 0, 1, 0 }
                        };
                        break;
                    case TetrominoRotation.Twice:
                        Grid = new int[4, 4]
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                            { 1, 1, 1, 1 },
                            { 0, 0, 0, 0 }
                        };
                        break;
                    case TetrominoRotation.Left:
                        Grid = new int[4, 4]
                        {
                            { 0, 1, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 1, 0, 0 }
                        };
                        break;
                }
            }
        }

        public override Color Color
        {
            get { return _color; }
        }

        public override void RotateLeft(TetrisGameBoard gameBoard)
        {
            int x = X;
            int y = Y;

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

            for (int i = 0; i < 5; i++)
            {
                X = x + wkd[i, 0];
                Y = y - wkd[i, 1];
                if (!Collision(gameBoard))
                {
                    //Debug.Log($"success with {wkd[i, 0]}, {wkd[i, 1]}");
                    break;
                }
            }
        }

        public override void RotateRight(TetrisGameBoard gameBoard)
        {
            int x = X;
            int y = Y;

            TetrominoRotation previousR = Rotation;
            int[,] wkd = WKDRightI[previousR];

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

            for (int i = 0; i < 5; i++)
            {
                X = x + wkd[i, 0];
                Y = y - wkd[i, 1];

                if (!Collision(gameBoard))
                {
                    //Debug.Log($"success with {wkd[i, 0]}, {wkd[i, 1]}");
                    break;
                }
            }
        }

        public override void UpdateTilesPositions()
        {
            float x;
            float y;

            switch (Rotation)
            {
                case TetrominoRotation.Initial:
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
            int[,] collisionGrid = new int[4, 4]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };

            for (int i = Y, a = 0; i < (Y + GridSize); i++, a++)
            {
                for (int j = X, b = 0; j < (X + GridSize); j++, b++)
                {
                    if (i < 0 || i > 19 || j < 0 || j > 9)
                        collisionGrid[a, b] = 1;
                    else if (gameBoard.Board[i, j] != null)
                        collisionGrid[a, b] = 1;
                }
            }

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (Grid[i, j] == 1 && collisionGrid[i, j] == 1)
                        return true;
                }
            }

            return false;
        }
    }
}
