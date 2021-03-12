using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tetrominoes
{
    public interface Tetromino
    {
        float X { get; set; }
        float Y { get; set; }
        TetrominoRotation Rotation { get; set; }
        GameObject[] Objects { get; set; }
        GameObject[,] Grid { get; set; }
        Color Color { get; }
        void RotateLeft();
        void RotateRight();
    }

    public enum TetrominoRotation
    {
        Initial,
        Right,
        Twice,
        Left
    }
}
