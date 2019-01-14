using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeTextureSet
    {
        private int _borderWidth = 10;
        private GraphicsDevice _graphicsDevice;
        private int _sizeInPixels;
        private Color _mainColor;
        private Color _headColor;
        private Color _borderColor;
        private List<int> _leftBorderMods;
        private List<int> _rightBorderMods;

        private Texture2D _head;
        private Texture2D _tail;
        private Texture2D _middleStraight;
        private Texture2D _middleCorner;
        private Texture2D _square;


        public SnakeTextureSet(GraphicsDevice graphicsDevice, int sizeInPixels, Color mainColor)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;

            _mainColor = mainColor;
            _headColor = new Color(r: _mainColor.R * 3/4, g: _mainColor.G * 3/4, b: _mainColor.B * 3/4);
            _borderColor = new Color(_headColor.R / 2, _headColor.G / 2, _headColor.B / 2);

            _leftBorderMods = new List<int>();
            _rightBorderMods = new List<int>();

            for (int i = 0; i < _borderWidth; i++)
            {
                _leftBorderMods.Add(i);
                _rightBorderMods.Add((_sizeInPixels - 1) - i);
            }

            _head = GetSnakeHeadTail(true);
            _tail = GetSnakeHeadTail(false);
            _middleStraight = GetMiddleStraight();
            _middleCorner = GetMiddleCorner();
            _square = GetSquare();
        }

        public Texture2D Head => _head;
        public Texture2D Tail => _tail;
        public Texture2D MiddleStraight => _middleStraight;
        public Texture2D MiddleCorner => _middleCorner;
        public Texture2D Square => _square;

        private int TopBorderEnd { get => _sizeInPixels * _borderWidth; }
        private int BottomBorderStart { get => TotalPixels - (_sizeInPixels * _borderWidth); }
        private int TotalPixels { get => _sizeInPixels * _sizeInPixels; }

        private Color[] GetDefaultColorData(Color color)
        {
            Color[] colorData = new Color[TotalPixels];

            for (int i = 0; i < TotalPixels; i++)
            {
                colorData[i] = color;
            }

            return colorData;
        }

        private Texture2D DefaultTexture => new Texture2D(_graphicsDevice, _sizeInPixels, _sizeInPixels);

        private Texture2D GetSnakeHeadTail(bool isHead)
        {
            Color[] colorData;

            if (isHead)
                colorData = GetDefaultColorData(_headColor);
            else
                colorData = GetDefaultColorData(_mainColor);

            for (int i = 0; i < TotalPixels; i++)
            {
                if (_leftBorderMods.Contains(i % _sizeInPixels) ||
                    _rightBorderMods.Contains(i % _sizeInPixels) ||
                    i < TopBorderEnd)
                {
                    colorData[i] = _borderColor;
                }
            }

            Texture2D texture = DefaultTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetMiddleStraight()
        {
            Color[] colorData = GetDefaultColorData(_mainColor);

            for (int i = 0; i < TotalPixels; i++)
            {
                if (_leftBorderMods.Contains(i % _sizeInPixels) ||
                    _rightBorderMods.Contains(i % _sizeInPixels))
                {
                    colorData[i] = _borderColor;
                }
            }

            Texture2D texture = DefaultTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetMiddleCorner()
        {
            Color[] colorData = GetDefaultColorData(_mainColor);

            for (int i = 0; i < TotalPixels; i++)
            {
                if (_leftBorderMods.Contains(i % _sizeInPixels) ||
                    i < TopBorderEnd ||
                    (i >= BottomBorderStart
                        && _rightBorderMods.Contains(i % _sizeInPixels)))
                {
                    colorData[i] = _borderColor;
                }
            }

            Texture2D texture = DefaultTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetSquare()
        {
            Color[] colorData = GetDefaultColorData(_mainColor);

            for (int i = 0; i < TotalPixels; i++)
            {
                if (_leftBorderMods.Contains(i % _sizeInPixels) ||
                    _rightBorderMods.Contains(i % _sizeInPixels) ||
                    i < TopBorderEnd ||
                    i >= BottomBorderStart)
                {
                    colorData[i] = _borderColor;
                }
            }

            Texture2D texture = DefaultTexture;
            texture.SetData(colorData);
            return texture;
        }
    }
}
