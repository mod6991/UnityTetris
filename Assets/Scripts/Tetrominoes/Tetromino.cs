using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tetrominoes
{
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

        public float PanelWidth { get; set; }
        public float PanelHeight { get; set; }
        public float TileWidth { get; set; }
        public float TileHeight { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public TetrominoRotation Rotation { get; set; }
        public GameObject[] Tiles { get; set; }
        public int[,] Grid { get; set; }
        public abstract Color Color { get; }
        public abstract void RotateLeft(TetrisGameBoard gameBoard);

        public abstract void RotateRight(TetrisGameBoard gameBoard);

        public void MoveLeft()
        {
            X -= 1;
            UpdateTilesPositions();
        }

        public void MoveRight()
        {
            X += 1;
            UpdateTilesPositions();
        }

        public abstract void UpdateTilesPositions();

        public abstract bool CollisionX(TetrisGameBoard gameBoard);
        public abstract bool CollisionY(TetrisGameBoard gameBoard);

        public Dictionary<TetrominoRotation, RotationWKCoords[]> WKDRightI = new Dictionary<TetrominoRotation, RotationWKCoords[]>()
        {
            {
                TetrominoRotation.Initial, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-2, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords(-2,-1),
                    new RotationWKCoords( 1, 2)
                }
            },
            {
                TetrominoRotation.Right, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords( 2, 0),
                    new RotationWKCoords(-1, 2),
                    new RotationWKCoords( 2,-1)
                }
            },
            {
                TetrominoRotation.Twice, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 2, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords( 2, 1),
                    new RotationWKCoords(-1,-2)
                }
            },
            {
                TetrominoRotation.Left, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords(-2, 0),
                    new RotationWKCoords( 1,-2),
                    new RotationWKCoords(-2, 1)
                }
            }
        };

        public Dictionary<TetrominoRotation, RotationWKCoords[]> WKDLeftI = new Dictionary<TetrominoRotation, RotationWKCoords[]>()
        {
            {
                TetrominoRotation.Initial, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords( 2, 0),
                    new RotationWKCoords(-1, 2),
                    new RotationWKCoords( 2,-1)
                }
            },
            {
                TetrominoRotation.Right, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 2, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords( 2, 1),
                    new RotationWKCoords(-1,-2)
                }
            },
            {
                TetrominoRotation.Twice, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords(-2, 0),
                    new RotationWKCoords( 1,-2),
                    new RotationWKCoords(-2, 1)
                }
            },
            {
                TetrominoRotation.Left, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-2, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords(-2,-1),
                    new RotationWKCoords( 1, 2)
                }
            }
        };

        public Dictionary<TetrominoRotation, RotationWKCoords[]> WKDRightJLSTZ = new Dictionary<TetrominoRotation, RotationWKCoords[]>()
        {
            {
                TetrominoRotation.Initial, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords(-1, 1),
                    new RotationWKCoords( 0,-2),
                    new RotationWKCoords(-1,-2)
                }
            },
            {
                TetrominoRotation.Right, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords( 1,-1),
                    new RotationWKCoords( 0, 2),
                    new RotationWKCoords( 1, 2)
                }
            },
            {
                TetrominoRotation.Twice, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords( 1, 1),
                    new RotationWKCoords( 0,-2),
                    new RotationWKCoords( 1,-2)
                }
            },
            {
                TetrominoRotation.Left, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords(-1,-1),
                    new RotationWKCoords( 0, 2),
                    new RotationWKCoords(-1, 2)
                }
            }
        };

        public Dictionary<TetrominoRotation, RotationWKCoords[]> WKDLeftJLSTZ = new Dictionary<TetrominoRotation, RotationWKCoords[]>()
        {
            {
                TetrominoRotation.Initial, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords( 1, 1),
                    new RotationWKCoords( 0,-2),
                    new RotationWKCoords( 1,-2)
                }
            },
            {
                TetrominoRotation.Right, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords( 1, 0),
                    new RotationWKCoords( 1,-1),
                    new RotationWKCoords( 0, 2),
                    new RotationWKCoords( 1, 2)
                }
            },
            {
                TetrominoRotation.Twice, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords(-1, 1),
                    new RotationWKCoords( 0,-2),
                    new RotationWKCoords(-1,-2)
                }
            },
            {
                TetrominoRotation.Left, new RotationWKCoords[5]
                {
                    new RotationWKCoords( 0, 0),
                    new RotationWKCoords(-1, 0),
                    new RotationWKCoords(-1,-1),
                    new RotationWKCoords( 0, 2),
                    new RotationWKCoords(-1, 2)
                }
            }
        };
    }

    public enum TetrominoRotation
    {
        Initial = 0,
        Right = 1,
        Twice = 2,
        Left = 3
    }

    public delegate GameObject NewTileDelegate(Color color);

    public class RotationWKCoords
    {
        public RotationWKCoords()
        { }

        public RotationWKCoords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
