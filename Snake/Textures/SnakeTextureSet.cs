using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Textures
{
    public class SnakeTextureSet
    {
        private GraphicsDevice _graphicsDevice;
        private int _sizeInPixels;
        private Color _mainColor;
        private Color _headColor;
        private Color _borderColor;
        private Texture2D _head;
        private Texture2D _tail;
        private Texture2D _middleStraight;
        private Texture2D _middleCorner;

        public SnakeTextureSet(GraphicsDevice graphicsDevice, int sizeInPixels, Color color)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;
            _mainColor = color;
            _headColor = new Color(r: _mainColor.R * 3/4, g: _mainColor.G * 3/4, b: _mainColor.B * 3/4);
            _borderColor = new Color(_headColor.R / 2, _headColor.G / 2, _headColor.B / 2);
            _head = GetSnakeHeadTail(true);
            _tail = GetSnakeHeadTail(false);
            _middleStraight = GetMiddleStraight();
            _middleCorner = GetMiddleCorner();
        }

        public Texture2D Head => _head;
        public Texture2D Tail => _tail;
        public Texture2D MiddleStraight => _middleStraight;
        public Texture2D MiddleCorner => _middleCorner;

        private Texture2D EmptyTexture => new Texture2D(_graphicsDevice, _sizeInPixels, _sizeInPixels);

        private Texture2D GetSnakeHeadTail(bool isHead)
        {
            Color color = isHead ? _headColor : _mainColor;
            Color[] colorData = ColorDataCreator.CreateColorData(color, _borderColor, _sizeInPixels, borderTop: true, borderLeft: true, borderRight: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetMiddleStraight()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderLeft: true, borderRight: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetMiddleCorner()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderTop: true, borderLeft: true);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, bottomRight: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }
    }
}
