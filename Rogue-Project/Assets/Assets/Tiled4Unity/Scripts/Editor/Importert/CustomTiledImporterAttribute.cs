﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEditor;
using UnityEngine;

namespace Tiled4Unity
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    class CustomTiledImporterAttribute : System.Attribute
    {
        public int Order { get; set; }
    }
}
