using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Logica;

namespace IPS_MAS_SALUD_Y_VIDA
{
    internal class Principal
    {
        // Funcion para maximizar ventana
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        // Funcion para eliminar los botenes de la ventana
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);
        [DllImport("user32.dll")]
        public static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        public const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        const int SW_MAXIMIZE = 3;
        static void Main(string[] args)
        {

            Console.Title = "IPS MAS SALUD Y VIDA - LIQUIDES";
            // Obtén el identificador de la ventana de la consola actual
            IntPtr hwnd = GetConsoleWindow();
            IntPtr consoleWindowHandle = FindWindow(null, Console.Title);

            if (consoleWindowHandle != IntPtr.Zero)
            {
                IntPtr sysMenuHandle = GetSystemMenu(consoleWindowHandle, 0);

                if (sysMenuHandle != IntPtr.Zero)
                {
                    // Deshabilitar la opción "Cerrar" (botón X)
                    RemoveMenu(sysMenuHandle, SC_CLOSE, MF_BYCOMMAND);
                }
            }
            if (hwnd != IntPtr.Zero)
            {
                // Maximiza la ventana de la consola
                ShowWindow(hwnd, SW_MAXIMIZE);
            }

            Liquidacion producto = new Liquidacion();
            MenuPrincipal menu = new MenuPrincipal(producto);
            menu.menuPrincipal_();


        }

    }
}
