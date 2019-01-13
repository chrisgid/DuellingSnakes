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
        private GraphicsDevice _graphicsDevice;
        private int _sizeInPixels;
        private Color _mainColor;
        private Color _headColor;
        private Color _borderColor;
        private int _borderWidth;
        private List<int> _leftBorderMods;
        private List<int> _rightBorderMods;

        public SnakeTextureSet(GraphicsDevice graphicsDevice, int sizeInPixels, Color mainColor, Color headColor, Color borderColor, int borderWidth)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;
            _mainColor = mainColor;
            _headColor = headColor;
            _borderColor = borderColor;
            _borderWidth = borderWidth;

            _leftBorderMods = new List<int>();
            _rightBorderMods = new List<int>();

            for (int i = 0; i < _borderWidth; i++)
            {
                _leftBorderMods.Add(i);
                _rightBorderMods.Add((_sizeInPixels - 1) - i);
            }
        }

        public Texture2D SnakeSquare
        {
            get
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

        public Texture2D SnakeHead => GetSnakeHeadTail(isHead: true);

        public Texture2D SnakeTail => GetSnakeHeadTail(isHead: false);

        public Texture2D GetSnakeHeadTail(bool isHead)
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

        public Texture2D SnakeMiddleStraight
        {
            get
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
        }

        public Texture2D SnakeMiddleCorner
        {
            get
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
        }

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
    }
}
