﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UIBlocks.MaterialDesignTheme
{
    internal static class DpiEditor
    {
        private static readonly int DpiX;
        private static readonly int DpiY;

        private const double StandardDpiX = 96.0;
        private const double StandardDpiY = 96.0;

        static DpiEditor()
        {
            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

            DpiX = (int)dpiXProperty.GetValue(null, null);
            DpiY = (int)dpiYProperty.GetValue(null, null);
        }

        public static double TransformToDeviceY(Visual visual, double y)
        {
            var source = PresentationSource.FromVisual(visual);
            if (source?.CompositionTarget != null) return y * source.CompositionTarget.TransformToDevice.M22;

            return TransformToDeviceY(y);
        }

        public static double TransformToDeviceX(Visual visual, double x)
        {
            var source = PresentationSource.FromVisual(visual);
            if (source?.CompositionTarget != null) return x * source.CompositionTarget.TransformToDevice.M11;

            return TransformToDeviceX(x);
        }

        public static double TransformToDeviceY(double y)
        {
            return y * DpiY / StandardDpiY;
        }

        public static double TransformToDeviceX(double x)
        {
            return x * DpiX / StandardDpiX;
        }
    }

}
