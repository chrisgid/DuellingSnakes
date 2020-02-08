using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeVersusSnake.Textures
{
    public class FoodTexture
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly int _sizeInPixels;
        private readonly Color _mainColor;
        private readonly Color _borderColor;

        public FoodTexture(GraphicsDevice graphicsDevice, int sizeInPixels, Color mainColor, Color borderColor)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;
            _mainColor = mainColor;
            _borderColor = borderColor;
            Square = GetSquare();
        }

        public Texture2D Square { get; }

        private Texture2D EmptyTexture => new Texture2D(_graphicsDevice, _sizeInPixels, _sizeInPixels);

        private Texture2D GetSquare()
        {
            var colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, true, true, true, true);
            var texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }
    }
}
