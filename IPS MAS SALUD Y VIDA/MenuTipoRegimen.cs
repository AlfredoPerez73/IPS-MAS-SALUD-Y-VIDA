using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Logica;
using Entidad;
    
namespace IPS_MAS_SALUD_Y_VIDA
{
    class MenuTipoRegimen
    {
        private Liquidacion liquidacion;
        private LiquidacionService liquidacionoService = new LiquidacionService();
        public MenuTipoRegimen(Liquidacion liquidacion)
        {
            this.liquidacion = liquidacion;
        }
        public int menuPrincipal()
        {
            int OPC = 0;
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
                Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
                Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
                Console.SetCursorPosition(76, 9); Console.WriteLine("M E N U  T E R C I A R I O");
                Console.SetCursorPosition(75, 13); Console.WriteLine("1. CONTRIBUTIVO");
                Console.SetCursorPosition(75, 14); Console.WriteLine("2. SUBSIDIADO");
                Console.SetCursorPosition(75, 15); Console.WriteLine("3. SALIR DEL MENU TERCIARIO");
                do
                {
                    Console.SetCursorPosition(75, 22); Console.WriteLine("Seleccione una opcion: ");
                    Console.SetCursorPosition(98, 22); OPC = Convert.ToInt32(Console.ReadLine());
                    Console.SetCursorPosition(98, 22); Console.WriteLine("         ");
                    Console.SetCursorPosition(98, 26); Console.WriteLine("Opcion no valida");
                } while ((OPC < 1) || (OPC > 3));
                Console.SetCursorPosition(98, 22); Console.WriteLine("                                     ");
                Console.SetCursorPosition(98, 26); Console.WriteLine("                                     ");
            }
            catch (FormatException)
            {
                Console.SetCursorPosition(98, 26); Console.WriteLine("Opcion no valida");
            }
            return OPC;
        }

        public void menuPrincipal_()
        {
            int MENU_;
            char OP = 'S';
            while (OP == 'S')
            {
                MENU_ = menuPrincipal();
                switch (MENU_)
                {
                    case 1:
                        Console.Clear();
                        MostrarRegimenContributivo();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        MostrarRegimenSubsidiado();
                        Console.Clear();
                        break;
                    case 3:
                        OP = 'N';
                        break;
                }
            }
        }
        public void MostrarRegimenContributivo()
        {
            titulos1();
            try
            {
                Console.SetCursorPosition(5, 15); Console.WriteLine("ID LIQUIDACIÓN  FECHA LIQUIDACIÓN       ID PACIENTE     TIPO AFILIACIÓN    SALARIO DEVENGADO    VALOR DE HOSPITALIZACIÓN   TARIFA     CUOTA MODERADA   TOPE MÁX");
                int X = 17;
                var lista = liquidacionoService.CargarRegistros().Where(i => i.TipoAfiliacion == "C").ToList();
                if (lista != null && lista.Any())
                {
                    foreach (var i in lista)
                    {
                        Console.SetCursorPosition(5, X); Console.WriteLine(i.IdLiquidacion);
                        Console.SetCursorPosition(21, X); Console.WriteLine(i.FechaLiquidacion);
                        Console.SetCursorPosition(46, X); Console.WriteLine(i.IdPaciente);
                        Console.SetCursorPosition(68, X); Console.WriteLine(i.TipoAfiliacion);
                        Console.SetCursorPosition(81, X); Console.WriteLine($"{i.SalarioDevengado:C}");
                        Console.SetCursorPosition(102, X); Console.WriteLine($"{i.ValorHospitalizacion:C}");
                        Console.SetCursorPosition(129, X); Console.WriteLine(i.Tarifa.ToString("F2"));
                        Console.SetCursorPosition(140, X); Console.WriteLine($"{i.CuotaModeradora:C}");
                        Console.SetCursorPosition(156, X); Console.WriteLine(i.TopeMax);
                        X++;
                    }
                    Console.SetCursorPosition(70, 14 + X); Console.WriteLine("Presione cualquier tecla para continuar.");
                    Console.SetCursorPosition(110, 14 + X); Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.SetCursorPosition(75, 25); Console.WriteLine("No hay registros de régimen Contributivo para mostrar. ");
                    Console.SetCursorPosition(105, 25); Console.ReadKey();
                }
            }
            catch (IOException)
            {
                // Manejo de excepciones
            }
        }

        public void MostrarRegimenSubsidiado()
        {
            titulos2();
            try
            {
                Console.SetCursorPosition(5, 15); Console.WriteLine("ID LIQUIDACIÓN  FECHA LIQUIDACIÓN       ID PACIENTE     TIPO AFILIACIÓN    SALARIO DEVENGADO    VALOR DE HOSPITALIZACIÓN   TARIFA     CUOTA MODERADA   TOPE MÁX");
                int X = 17;
                var lista = liquidacionoService.CargarRegistros().Where(i => i.TipoAfiliacion == "S").ToList();
                if (lista != null && lista.Any())
                {
                    foreach (var i in lista)
                    {
                        Console.SetCursorPosition(5, X); Console.WriteLine(i.IdLiquidacion);
                        Console.SetCursorPosition(21, X); Console.WriteLine(i.FechaLiquidacion);
                        Console.SetCursorPosition(46, X); Console.WriteLine(i.IdPaciente);
                        Console.SetCursorPosition(68, X); Console.WriteLine(i.TipoAfiliacion);
                        Console.SetCursorPosition(81, X); Console.WriteLine($"{i.SalarioDevengado:C}");
                        Console.SetCursorPosition(102, X); Console.WriteLine($"{i.ValorHospitalizacion:C}");
                        Console.SetCursorPosition(129, X); Console.WriteLine(i.Tarifa.ToString("F2"));
                        Console.SetCursorPosition(140, X); Console.WriteLine($"{i.CuotaModeradora:C}");
                        Console.SetCursorPosition(156, X); Console.WriteLine(i.TopeMax);
                        X++;
                    }
                    Console.SetCursorPosition(70, 14 + X); Console.WriteLine("Presione cualquier tecla para continuar.");
                    Console.SetCursorPosition(110, 14 + X); Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.SetCursorPosition(75, 25); Console.WriteLine("No hay registros de régimen Subsidiado para mostrar. ");
                    Console.SetCursorPosition(105, 25); Console.ReadKey();
                }
            }
            catch (IOException)
            {
                // Manejo de excepciones
            }
        }
        public void titulos1()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(70, 9); Console.WriteLine("INFORMACION DE IPS | REGIMEN CONTRIBUTIVO");
        }
        public void titulos2()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(70, 9); Console.WriteLine("INFORMACION DE IPS | REGIMEN SUBSIDIADO");
        }
    }
}
