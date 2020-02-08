using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame.Textures
{
    public class SnakeTextureSet
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly int _sizeInPixels;
        private readonly Color _mainColor;
        private readonly Color _headColor;
        private readonly Color _borderColor;

        public SnakeTextureSet(GraphicsDevice graphicsDevice, int sizeInPixels, Color color)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;
            _mainColor = color;
            _headColor = new Color(r: _mainColor.R * 3/4, g: _mainColor.G * 3/4, b: _mainColor.B * 3/4);
            _borderColor = new Color(_headColor.R / 2, _headColor.G / 2, _headColor.B / 2);
            Head = GetSnakeHeadTail(true);
            Tail = GetSnakeHeadTail(false);
            MiddleStraight = GetMiddleStraight();
            MiddleCorner = GetMiddleCorner();
        }

        public Texture2D Head { get; }

        public Texture2D Tail { get; }

        public Texture2D MiddleStraight { get; }

        public Texture2D MiddleCorner { get; }

        private Texture2D EmptyTexture => new Texture2D(_graphicsDevice, _sizeInPixels, _sizeInPixels);

        private Texture2D GetSnakeHeadTail(bool isHead)
        {
            var color = isHead ? _headColor : _mainColor;
            var colorData = ColorDataCreator.CreateColorData(color, _borderColor, _sizeInPixels, borderTop: true, borderLeft: true, borderRight: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetMiddleStraight()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderLeft: true, borderRight: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetMiddleCorner()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderTop: true, borderLeft: true);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, bottomRight: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }
    }
}
