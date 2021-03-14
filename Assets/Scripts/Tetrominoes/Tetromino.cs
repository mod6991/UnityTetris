using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tetrominoes
{
    /// <summary>
    /// Tetromino base abstract class
    /// </summary>
    public abstract class Tetromino
    {
        public Tetromino(float panelWidth, float panelHeight,
                         float tileWidth, float tileHeight)
        {
            PanelWidth = panelWidth;
            PanelHeight = panelHeight;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
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
        public TetrominoRotation Rotation { get; set; }
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
        /// Rotate tetromino counter-clockwise
        /// </summary>
        /// <param name="gameBoard"></param>
        public abstract void RotateLeft(TetrisGameBoard gameBoard);

        /// <summary>
        /// Rotate tetromino clockwise
        /// </summary>
        /// <param name="gameBoard"></param>
        public abstract void RotateRight(TetrisGameBoard gameBoard);

        /// <summary>
        /// Move tetromino to the left
        /// </summary>
        public void MoveLeft()
        {
            X -= 1;
            UpdateTilesPositions();
        }

        /// <summary>
        /// Move tetromino to the right
        /// </summary>
        public void MoveRight()
        {
            X += 1;
            UpdateTilesPositions();
        }

        /// <summary>
        /// Update game objects (tiles) positions based on the tetromino coordinates and its rotation
        /// </summary>
        public abstract void UpdateTilesPositions();

        public abstract bool Collision(TetrisGameBoard gameBoard);

        /// <summary>
        /// Wall kick data for I piece (clockwise)
        /// </summary>
        public Dictionary<TetrominoRotation, int[,]> WKDRightI = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, {-2, 0}, { 1, 0}, {-2,-1}, { 1, 2} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, {-1, 0}, { 2, 0}, {-1, 2}, { 2,-1} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, { 2, 0}, {-1, 0}, { 2, 1}, {-1,-2} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, { 1, 0}, {-2, 0}, { 1,-2}, {-2, 1} } }
        };

        /// <summary>
        /// Wall kick data for I piece (counter-clockwise)
        /// </summary>
        public Dictionary<TetrominoRotation, int[,]> WKDLeftI = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, {-1, 0}, { 2, 0}, {-1, 2}, { 2,-1} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, { 2, 0}, {-1, 0}, { 2, 1}, {-1,-2} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, { 1, 0}, {-2, 0}, { 1,-2}, {-2, 1} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, {-2, 0}, { 1, 0}, {-2,-1}, { 1, 2} } }
        };

        /// <summary>
        /// Wall kick data for JLSTZ pieces (clockwise)
        /// </summary>
        public Dictionary<TetrominoRotation, int[,]> WKDRightJLSTZ = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, {-1, 0}, {-1, 1}, { 0,-2}, {-1,-2} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, { 1, 0}, { 1,-1}, { 0, 2}, { 1, 2} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, { 1, 0}, { 1, 1}, { 0,-2}, { 1,-2} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, {-1, 0}, {-1,-1}, { 0, 2}, {-1, 2} } }
        };

        /// <summary>
        /// Wall kick data for JLSTZ pieces (counter-clockwise)
        /// </summary>
        public Dictionary<TetrominoRotation, int[,]> WKDLeftJLSTZ = new Dictionary<TetrominoRotation, int[,]>()
        {
            { TetrominoRotation.Initial, new int[5, 2] { { 0, 0}, { 1, 0}, { 1, 1}, { 0,-2}, { 1,-2} } },
            { TetrominoRotation.Right, new int[5, 2] { { 0, 0}, { 1, 0}, { 1,-1}, { 0, 2}, { 1, 2} } },
            { TetrominoRotation.Twice, new int[5, 2] { { 0, 0}, {-1, 0}, {-1, 1}, { 0,-2}, {-1,-2} } },
            { TetrominoRotation.Left, new int[5, 2] { { 0, 0}, {-1, 0}, {-1,-1}, { 0, 2}, {-1, 2} } }
        };
    }
}
