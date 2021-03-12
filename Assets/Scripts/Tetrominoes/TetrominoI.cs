using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tetrominoes
{
    public class TetrominoI : Tetromino
    {
        private static Color _color = new Color(1, 1, 1, 1);

        public TetrominoI()
        {

        }

        public float X { get; set; }
        public float Y { get; set; }
        public TetrominoRotation Rotation { get; set; }
        public GameObject[] Objects { get; set; }
        public GameObject[,] Grid { get; set; }

        public Color Color
        {
            get { return _color; }
        }

        public void RotateLeft()
        {
            throw new NotImplementedException();
        }

        public void RotateRight()
        {
            throw new NotImplementedException();
        }
    }
}
