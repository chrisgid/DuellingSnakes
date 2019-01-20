using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Textures
{
    class WallTextureSet
    {
        private GraphicsDevice _graphicsDevice;
        private int _sizeInPixels;
        private Color _mainColor;
        private Color _borderColor;
        private Texture2D _top;
        private Texture2D _bottom;
        private Texture2D _left;
        private Texture2D _right;
        private Texture2D _topLeft;
        private Texture2D _topRight;
        private Texture2D _bottomLeft;
        private Texture2D _bottomRight;


        public WallTextureSet(GraphicsDevice graphicsDevice, int sizeInPixels, Color color)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;
            _mainColor = color;
            _borderColor = new Color(_mainColor.R * 3 / 8, _mainColor.G / 2, _mainColor.B / 2);

            _top = GetTop();
            _bottom = GetBottom();
            _left = GetLeft();
            _right = GetRight();
            _topLeft = GetTopLeft();
            _topRight = GetTopRight();
            _bottomLeft = GetBottomLeft();
            _bottomRight = GetBottomRight();
        }


        public Texture2D Top => _top;
        public Texture2D Bottom => _bottom;
        public Texture2D Left => _left;
        public Texture2D Right => _right;
        public Texture2D TopLeft => _topLeft;
        public Texture2D TopRight => _topRight;
        public Texture2D BottomLeft => _bottomLeft;
        public Texture2D BottomRight => _bottomRight;

        private Texture2D EmptyTexture => new Texture2D(_graphicsDevice, _sizeInPixels, _sizeInPixels);

        private Texture2D GetTop()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderBottom: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetBottom()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderTop: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetLeft()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderRight: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetRight()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderLeft: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetTopLeft()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, bottomRight: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetTopRight()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, bottomLeft: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetBottomLeft()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, topRight: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetBottomRight()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, topLeft: true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

    }
}
