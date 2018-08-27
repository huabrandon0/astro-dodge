using UnityEngine;

namespace AsteroidRage
{
    [System.Serializable]
    public class VerticalGradient
    {
        public Color _topColor = Color.white;
        public Color _bottomColor = Color.black;

        public VerticalGradient()
        {
            
        }

        public VerticalGradient(Color topColor, Color bottomColor)
        {
            _topColor = topColor;
            _bottomColor = bottomColor;
        }

        public Color BlendedColour(float time)
        {
            return Color.Lerp(_bottomColor, _topColor, time);
        }
    }
}
