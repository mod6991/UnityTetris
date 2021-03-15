using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetrominoes
{
    public class TetrominoI : Tetromino
    {
        private static Color _color = new Color(0, 1, 1, 1);

        public TetrominoI(NewTileDelegate del,
                          float panelWidth, float panelHeight,
                          float tileWidth, float tileHeight)
            : base(del, panelWidth, panelHeight, tileWidth, tileHeight)
        {
            X = 3;
            Y = -1;
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
            int x = X;
            int y = Y;

            int[,] wkd = WKDLeftI[Rotation];

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

            int[,] wkd = WKDRightI[Rotation];

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

        public override void UpdateGrid(TetrominoRotation rotation)
        {
            switch (rotation)
            {
                case TetrominoRotation.Initial:
                    Grid = new int[,]
                    {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case TetrominoRotation.Right:
                    Grid = new int[,]
                    {
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 }
                    };
                    break;
                case TetrominoRotation.Twice:
                    Grid = new int[,]
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case TetrominoRotation.Left:
                    Grid = new int[,]
                    {
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 }
                    };
                    break;
            }
        }

        public override bool Collision(TetrisGameBoard gameBoard)
        {
            // Create an empty collision grid of size GridSize
            List<List<int>> collisionGrid = new List<List<int>>();
            for (int i = 0; i < GridSize; i++)
            {
                collisionGrid.Add(new List<int>());
                for (int j = 0; j < GridSize; j++)
                    collisionGrid[i].Add(0);
            }

            // Add collision data from wall or gameboard
            for (int i = Y, a = 0; i < (Y + GridSize); i++, a++)
            {
                for (int j = X, b = 0; j < (X + GridSize); j++, b++)
                {
                    if (i < 0 || i > 19 || j < 0 || j > 9)
                        collisionGrid[a][b] = 1;
                    else if (gameBoard.Board[i, j] != null)
                        collisionGrid[a][ b] = 1;
                }
            }

            // Check for collision between Grid and collision grid
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (Grid[i, j] == 1 && collisionGrid[i][j] == 1)
                        return true;
                }
            }

            return false;
        }
    }
}
