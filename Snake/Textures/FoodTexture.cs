using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Textures
{
    class FoodTexture
    {
        private GraphicsDevice _graphicsDevice;
        private int _sizeInPixels;
        private Color _mainColor;
        private Color _borderColor;
        private Texture2D _square;

        public FoodTexture(GraphicsDevice graphicsDevice, int sizeInPixels, Color mainColor, Color borderColor)
        {
            _graphicsDevice = graphicsDevice;
            _sizeInPixels = sizeInPixels;
            _mainColor = mainColor;
            _borderColor = borderColor;
            _square = GetSquare();
        }

        public Texture2D Square => _square;

        private Texture2D EmptyTexture => new Texture2D(_graphicsDevice, _sizeInPixels, _sizeInPixels);

        private Texture2D GetSquare()
        {
            Color[] colorData = ColorDataCreator.CreateColorData(_mainColor, _borderColor, _sizeInPixels, true, true, true, true);
            Texture2D texture = EmptyTexture;
            texture.SetData(colorData);
            return texture;
        }
    }
}
