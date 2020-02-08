using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeVersusSnake.Textures
{
    public class WallTextureSet
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly int _sizeInPixels;
        private readonly Color _mainColor;
        private readonly Color _borderColor;

        public WallTextureSet(GraphicsDevice graphicsDevice, int sizeInPixels, Color color)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;
            _mainColor = color;
            _borderColor = new Color(_mainColor.R * 3 / 8, _mainColor.G / 2, _mainColor.B / 2);

            Top = GetTop();
            Bottom = GetBottom();
            Left = GetLeft();
            Right = GetRight();
            TopLeft = GetTopLeft();
            TopRight = GetTopRight();
            BottomLeft = GetBottomLeft();
            BottomRight = GetBottomRight();
        }

        public Texture2D Top { get; }

        public Texture2D Bottom { get; }

        public Texture2D Left { get; }

        public Texture2D Right { get; }

        public Texture2D TopLeft { get; }

        public Texture2D TopRight { get; }

        public Texture2D BottomLeft { get; }

        public Texture2D BottomRight { get; }

        private Texture2D EmptyTexture => new Texture2D(_graphicsDevice, _sizeInPixels, _sizeInPixels);

        private Texture2D GetTop()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderBottom: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetBottom()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderTop: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetLeft()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderRight: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetRight()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, borderLeft: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetTopLeft()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, bottomRight: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetTopRight()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, bottomLeft: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetBottomLeft()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, topRight: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

        private Texture2D GetBottomRight()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels);
            colorData.AddCornerBorder(_borderColor, _sizeInPixels, topLeft: true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }

    }
}
