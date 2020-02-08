using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeVersusSnake.Textures
{
    public static class ColorDataCreator
    {
        private static readonly int _borderWidth = 10;

        public static Color[] CreateColorData(Color mainColor, Color borderColor, int sizeInPixels, bool borderTop = false, bool borderBottom = false, bool borderLeft = false, bool borderRight = false)
        {
            var colorData = GetDefaultSquareColorData(mainColor, sizeInPixels);

            if (borderTop)
            {
                colorData.AddBorderTop(borderColor, sizeInPixels);
            }

            if (borderBottom)
            {
                colorData.AddBorderBottom(borderColor, sizeInPixels);
            }

            if (borderLeft)
            {
                colorData.AddBorderLeft(borderColor, sizeInPixels);
            }

            if (borderRight)
            {
                colorData.AddBorderRight(borderColor, sizeInPixels);
            }

            return colorData;
        }
        
        public static Color[] AddCornerBorder(this Color[] colorData, Color borderColor, int sizeInPixels, bool topLeft = false, bool topRight = false, bool bottomLeft = false, bool bottomRight = false)
        {

            var totalPixels = sizeInPixels * sizeInPixels;
            var rightBorderMods = new List<int>();
            var leftBorderMods = new List<int>();
            var topBorderEnd = sizeInPixels * _borderWidth;
            var bottomBorderStart = totalPixels - (sizeInPixels * _borderWidth);

            for (var i = 0; i < _borderWidth; i++)
            {
                rightBorderMods.Add((sizeInPixels - 1) - i);
            }

            for (var i = 0; i < _borderWidth; i++)
            {
                leftBorderMods.Add(i);
            }

            for (var i = 0; i < totalPixels; i++)
            {
                if (topLeft && ((i < topBorderEnd) && (leftBorderMods.Contains(i % sizeInPixels))))
                {
                    colorData[i] = borderColor;
                }

                if (topRight && ((i < topBorderEnd) && (rightBorderMods.Contains(i % sizeInPixels))))
                {
                    colorData[i] = borderColor;
                }

                if (bottomLeft && ((i >= bottomBorderStart) && (leftBorderMods.Contains(i % sizeInPixels))))
                {
                    colorData[i] = borderColor;
                }

                if (bottomRight && ((i >= bottomBorderStart) && (rightBorderMods.Contains(i % sizeInPixels))))
                {
                    colorData[i] = borderColor;
                }
            }

            return colorData;
        }
        
        private static Color[] GetDefaultSquareColorData(Color color, int sizeInPixels)
        {
            var totalPixels = sizeInPixels * sizeInPixels;

            var colorData = new Color[totalPixels];

            for (var i = 0; i < totalPixels; i++)
            {
                colorData[i] = color;
            }

            return colorData;
        }

        private static Color[] AddBorderTop(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            var totalPixels = sizeInPixels * sizeInPixels;
            var topBorderEnd = sizeInPixels * _borderWidth;

            for (var i = 0; i < totalPixels; i++)
            {
                if (i < topBorderEnd)
                {
                    colorData[i] = borderColor;
                }
            }

            return colorData;
        }

        private static Color[] AddBorderBottom(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            var totalPixels = sizeInPixels * sizeInPixels;
            var bottomBorderStart = totalPixels - (sizeInPixels * _borderWidth);

            for (var i = 0; i < totalPixels; i++)
            {
                if (i >= bottomBorderStart)
                {
                    colorData[i] = borderColor;
                }
            }

            return colorData;
        }

        private static Color[] AddBorderLeft(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            var leftBorderMods = new List<int>();

            for (var i = 0; i < _borderWidth; i++)
            {
                leftBorderMods.Add(i);
            }

            var totalPixels = sizeInPixels * sizeInPixels;

            for (var i = 0; i < totalPixels; i++)
            {
                if (leftBorderMods.Contains(i % sizeInPixels))
                {
                    colorData[i] = borderColor;
                }
            }

            return colorData;
        }

        private static Color[] AddBorderRight(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            var rightBorderMods = new List<int>();

            for (var i = 0; i < _borderWidth; i++)
            {
                rightBorderMods.Add((sizeInPixels - 1) - i);
            }

            var totalPixels = sizeInPixels * sizeInPixels;

            for (var i = 0; i < totalPixels; i++)
            {
                if (rightBorderMods.Contains(i % sizeInPixels))
                {
                    colorData[i] = borderColor;
                }
            }

            return colorData;
        }
    }
}
