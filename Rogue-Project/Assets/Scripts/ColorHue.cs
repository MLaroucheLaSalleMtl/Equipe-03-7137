using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHue : MonoBehaviour
{
    public enum ColorHues
    {
        Red,
        Blue,
        Black,
        Golden,
        BaseColor
    }

    private static Hashtable ColourValues = new Hashtable
    {
         { ColorHues.Red,       new Color32( 166 , 254 , 0, 1 ) },
         { ColorHues.Blue,      new Color32( 0 , 122 , 254, 1 ) },
         { ColorHues.Black,     new Color32( 0 , 254 , 111, 1 ) },
         { ColorHues.Golden,    new Color32( 0 , 201 , 254, 1 ) },
         { ColorHues.BaseColor, new Color32( 255 , 255 , 255, 255 ) },

     };

    public static Color ColourValue(ColorHues colourValues)
    {
        return (Color)ColourValues[colourValues];
    }

}
