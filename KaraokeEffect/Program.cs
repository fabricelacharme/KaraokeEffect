﻿using System;
using System.Windows.Forms;

namespace KaraokeEffect
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false); // FAB
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
        }
    }
}
