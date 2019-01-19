using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Textures
{
    static class ColorDataCreator
    {
        private static int _borderWidth = 10;

        public static Color[] CreateColorData(Color mainColor, Color borderColor, int sizeInPixels, bool borderTop = false, bool borderBottom = false, bool borderLeft = false, bool borderRight = false)
        {
            Color[] colorData = GetDefaultSquareColorData(mainColor, sizeInPixels);

            if (borderTop)
                colorData.AddBorderTop(borderColor, sizeInPixels);

            if (borderBottom)
                colorData.AddBorderBottom(borderColor, sizeInPixels);

            if (borderLeft)
                colorData.AddBorderLeft(borderColor, sizeInPixels);

            if (borderRight)
                colorData.AddBorderRight(borderColor, sizeInPixels);

            return colorData;
        }
        
        public static Color[] AddCornerBorder(this Color[] colorData, Color borderColor, int sizeInPixels, bool topLeft = false, bool topRight = false, bool bottomLeft = false, bool bottomRight = false)
        {

            int totalPixels = sizeInPixels * sizeInPixels;
            List<int> rightBorderMods = new List<int>();
            List<int> leftBorderMods = new List<int>();
            int topBorderEnd = sizeInPixels * _borderWidth;
            int bottomBorderStart = totalPixels - (sizeInPixels * _borderWidth);

            for (int i = 0; i < _borderWidth; i++)
                rightBorderMods.Add((sizeInPixels - 1) - i);

            for (int i = 0; i < _borderWidth; i++)
                leftBorderMods.Add(i);

            for (int i = 0; i < totalPixels; i++)
            {
                if (topLeft && ((i < topBorderEnd) && (leftBorderMods.Contains(i % sizeInPixels))))
                    colorData[i] = borderColor;

                if (topRight && ((i < topBorderEnd) && (rightBorderMods.Contains(i % sizeInPixels))))
                    colorData[i] = borderColor;

                if (bottomLeft && ((i >= bottomBorderStart) && (leftBorderMods.Contains(i % sizeInPixels))))
                    colorData[i] = borderColor;

                if (bottomRight && ((i >= bottomBorderStart) && (rightBorderMods.Contains(i % sizeInPixels))))
                    colorData[i] = borderColor;
            }

            return colorData;
        }
        
        private static Color[] GetDefaultSquareColorData(Color color, int sizeInPixels)
        {
            int totalPixels = sizeInPixels * sizeInPixels;

            Color[] colorData = new Color[totalPixels];

            for (int i = 0; i < totalPixels; i++)
                colorData[i] = color;

            return colorData;
        }

        private static Color[] AddBorderTop(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            int totalPixels = sizeInPixels * sizeInPixels;
            int topBorderEnd = sizeInPixels * _borderWidth;

            for (int i = 0; i < totalPixels; i++)
                if (i < topBorderEnd)
                    colorData[i] = borderColor;

            return colorData;
        }

        private static Color[] AddBorderBottom(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            int totalPixels = sizeInPixels * sizeInPixels;
            int bottomBorderStart = totalPixels - (sizeInPixels * _borderWidth);

            for (int i = 0; i < totalPixels; i++)
                if (i >= bottomBorderStart)
                    colorData[i] = borderColor;

            return colorData;
        }

        private static Color[] AddBorderLeft(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            List<int> leftBorderMods = new List<int>();

            for (int i = 0; i < _borderWidth; i++)
                leftBorderMods.Add(i);

            int totalPixels = sizeInPixels * sizeInPixels;

            for (int i = 0; i < totalPixels; i++)
                if (leftBorderMods.Contains(i % sizeInPixels))
                    colorData[i] = borderColor;

            return colorData;
        }

        private static Color[] AddBorderRight(this Color[] colorData, Color borderColor, int sizeInPixels)
        {
            List<int> rightBorderMods = new List<int>();

            for (int i = 0; i < _borderWidth; i++)
                rightBorderMods.Add((sizeInPixels - 1) - i);

            int totalPixels = sizeInPixels * sizeInPixels;

            for (int i = 0; i < totalPixels; i++)
                if (rightBorderMods.Contains(i % sizeInPixels))
                    colorData[i] = borderColor;

            return colorData;
        }
    }
}
