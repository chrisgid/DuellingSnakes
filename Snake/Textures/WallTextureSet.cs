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

            
        }

        public Texture2D Top => _top;
        public Texture2D Bottom => _bottom;
        public Texture2D Left => _left;
        public Texture2D Right => _right;
        public Texture2D TopLeft => _topLeft;
        public Texture2D TopRight => _topRight;
        public Texture2D BottomLeft => _bottomLeft;
        public Texture2D BottomRight => _bottomRight;
    }
}
