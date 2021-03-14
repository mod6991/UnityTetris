using Assets.Scripts.Tetrominoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TetrisGameBoard
    {
        public TetrisGameBoard(float panelWidth, float panelHeight,
                               float tileWidth, float tileHeight)
        {
            Board = new GameObject[20, 10]
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            };

            PanelWidth = panelWidth;
            PanelHeight = panelHeight;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public float PanelWidth { get; }
        public float PanelHeight { get; }
        public float TileWidth { get; }
        public float TileHeight { get; }

        public GameObject[,] Board { get; }

        public void UpdateTilesPositions()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Board[i, j] != null)
                    {
                        float x = -PanelWidth / 2 + TileWidth / 2 + j * TileWidth;
                        float y = PanelHeight / 2 - TileHeight / 2 - i * TileHeight;

                        Board[i, j].transform.localPosition = new Vector2(x, y);
                    }
                }
            }
        }

        public Tetromino GetRandomTetromino()
        {
            throw new NotImplementedException();
        }
    }
}
