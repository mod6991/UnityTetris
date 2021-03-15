using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tetrominoes
{
    /// <summary>
    /// Tetromino base abstract class
    /// </summary>
    public abstract class Tetromino
    {
        private TetrominoRotation _rotation;

        public Tetromino(NewTileDelegate del, 
                         float panelWidth, float panelHeight,
                         float tileWidth, float tileHeight)
        {
            PanelWidth = panelWidth;
            PanelHeight = panelHeight;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            Rotation = TetrominoRotation.Initial;
            Tiles = new GameObject[4];
            Tiles[0] = del(Color);
            Tiles[1] = del(Color);
            Tiles[2] = del(Color);
            Tiles[3] = del(Color);
        }

        /// <summary>
        /// Panel width for drawing positions
        /// </summary>
        public float PanelWidth { get; set; }
        /// <summary>
        /// Panel height for drawing positions
        /// </summary>
        public float PanelHeight { get; set; }
        /// <summary>
        /// Tile width for drawing positions
        /// </summary>
        public float TileWidth { get; set; }
        /// <summary>
        /// Tile height for drawing positions
        /// </summary>
        public float TileHeight { get; set; }
        /// <summary>
        /// Grid X coordinate
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Grid Y coordinate
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Grid size
        /// </summary>
        public abstract int GridSize { get; }
        /// <summary>
        /// Tetromino rotation
        /// </summary>
        public TetrominoRotation Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                UpdateGrid(value);
            }
        }

        /// <summary>
        /// GameObject array
        /// </summary>
        public GameObject[] Tiles { get; set; }
        /// <summary>
        /// Grid array
        /// </summary>
        public int[,] Grid { get; set; }
        /// <summary>
        /// Tetromino color
        /// </summary>
        public abstract Color Color { get; }
        /// <summary>
        /// Wall kick data (counter-clockwise)
        /// </summary>
        public abstract Dictionary<TetrominoRotation, int[,]> WKDLeft { get; }
        /// <summary>
        /// Wall kick data (clockwise)
        /// </summary>
        public abstract Dictionary<TetrominoRotation, int[,]> WKDRight { get; }

        /// <summary>
        /// Rotate tetromino counter-clockwise
        /// </summary>
        /// <param name="gameBoard"></param>
        public void RotateLeft(TetrisGameBoard gameBoard)
        {
            int x = X;
            int y = Y;

            int[,] wkd = WKDLeft[Rotation];

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

        /// <summary>
        /// Rotate tetromino clockwise
        /// </summary>
        /// <param name="gameBoard"></param>
        public void RotateRight(TetrisGameBoard gameBoard)
        {
            int x = X;
            int y = Y;

            int[,] wkd = WKDRight[Rotation];

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

        public abstract void UpdateGrid(TetrominoRotation rotation);

        /// <summary>
        /// Update game objects (tiles) positions based on the tetromino coordinates and its rotation
        /// </summary>
        public void UpdateTilesPositions()
        {
            int tilesDone = 0;

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (Grid[i, j] == 1)
                    {
                        float x = -PanelWidth / 2 + TileWidth / 2 + X * TileWidth + j * TileWidth;
                        float y = PanelHeight / 2 - TileHeight / 2 - Y * TileHeight - i * TileHeight;

                        Tiles[tilesDone++].transform.localPosition = new Vector2(x, y);

                        if (tilesDone == 4)
                            return;
                    }
                }
            }
        }

        public bool Collision(TetrisGameBoard gameBoard)
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

        /// <summary>
        /// Wall kick data for I piece (clockwise)
        /// </summary>
        protected Dictionary<TetrominoRotation, int[,]> WKDRightI = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, {-2, 0}, { 1, 0}, {-2,-1}, { 1, 2} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, {-1, 0}, { 2, 0}, {-1, 2}, { 2,-1} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, { 2, 0}, {-1, 0}, { 2, 1}, {-1,-2} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, { 1, 0}, {-2, 0}, { 1,-2}, {-2, 1} } }
        };

        /// <summary>
        /// Wall kick data for I piece (counter-clockwise)
        /// </summary>
        protected Dictionary<TetrominoRotation, int[,]> WKDLeftI = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, {-1, 0}, { 2, 0}, {-1, 2}, { 2,-1} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, { 2, 0}, {-1, 0}, { 2, 1}, {-1,-2} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, { 1, 0}, {-2, 0}, { 1,-2}, {-2, 1} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, {-2, 0}, { 1, 0}, {-2,-1}, { 1, 2} } }
        };

        /// <summary>
        /// Wall kick data for JLSTZ pieces (clockwise)
        /// </summary>
        protected Dictionary<TetrominoRotation, int[,]> WKDRightJLSTZ = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, {-1, 0}, {-1, 1}, { 0,-2}, {-1,-2} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, { 1, 0}, { 1,-1}, { 0, 2}, { 1, 2} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, { 1, 0}, { 1, 1}, { 0,-2}, { 1,-2} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, {-1, 0}, {-1,-1}, { 0, 2}, {-1, 2} } }
        };

        /// <summary>
        /// Wall kick data for JLSTZ pieces (counter-clockwise)
        /// </summary>
        protected Dictionary<TetrominoRotation, int[,]> WKDLeftJLSTZ = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, { 1, 0}, { 1, 1}, { 0,-2}, { 1,-2} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, { 1, 0}, { 1,-1}, { 0, 2}, { 1, 2} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, {-1, 0}, {-1, 1}, { 0,-2}, {-1,-2} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, {-1, 0}, {-1,-1}, { 0, 2}, {-1, 2} } }
        };
    }
}
