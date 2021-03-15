using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tetrominoes
{
    public class TetrominoT : Tetromino
    {
        private static Color _color = new Color(0.502f, 0, 1, 1);

        public TetrominoT(NewTileDelegate del,
                          float panelWidth, float panelHeight,
                          float tileWidth, float tileHeight)
            : base(del, panelWidth, panelHeight, tileWidth, tileHeight)
        {
            X = 3;
            Y = -1;
        }

        public override int GridSize
        {
            get { return 3; }
        }

        public override Color Color
        {
            get { return _color; }
        }

        public override Dictionary<TetrominoRotation, int[,]> WKDLeft
        {
            get { return WKDLeftI; }
        }

        public override Dictionary<TetrominoRotation, int[,]> WKDRight
        {
            get { return WKDRightI; }
        }

        public override void UpdateGrid(TetrominoRotation rotation)
        {
            switch (rotation)
            {
                case TetrominoRotation.Initial:
                    Grid = new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    };
                    break;
                case TetrominoRotation.Right:
                    Grid = new int[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 1, 0 },
                    };
                    break;
                case TetrominoRotation.Twice:
                    Grid = new int[,]
                    {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 1, 0 },
                    };
                    break;
                case TetrominoRotation.Left:
                    Grid = new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 0 },
                    };
                    break;
            }
        }
    }
}
