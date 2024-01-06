using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Entidad;
using Logica;

namespace IPS_MAS_SALUD_Y_VIDA
{
    public class MenuSecundario
    {
        private Liquidacion liquidacion;
        private LiquidacionService liquidacionoService = new LiquidacionService();
        public MenuSecundario(Liquidacion liquidacion)
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
                Console.SetCursorPosition(76, 9); Console.WriteLine("M E N U  S E C U N D A R I O");
                Console.SetCursorPosition(75, 13); Console.WriteLine("1. FILTRO POR TIPO DE REGIMEN");
                Console.SetCursorPosition(75, 14); Console.WriteLine("2. VALOR TOTAL DE LIQUIDACIONES");
                Console.SetCursorPosition(75, 15); Console.WriteLine("3. FILTRO DE LIQUIDACION POR TIEMPO");
                Console.SetCursorPosition(75, 16); Console.WriteLine("4. CONSULTA SELECTIVA");
                Console.SetCursorPosition(75, 20); Console.WriteLine("5. SALIR DEL MENU SECUNDARIO");
                do
                {
                    Console.SetCursorPosition(75, 22); Console.WriteLine("Seleccione una opcion: ");
                    Console.SetCursorPosition(98, 22); OPC = Convert.ToInt32(Console.ReadLine());
                    Console.SetCursorPosition(98, 22); Console.WriteLine("         ");
                    Console.SetCursorPosition(98, 26); Console.WriteLine("Opcion no valida");
                } while ((OPC < 1) || (OPC > 5));
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
            MenuTipoRegimen regimen = new MenuTipoRegimen(liquidacion);
            int MENU_;
            char OP = 'S';
            while (OP == 'S')
            {
                MENU_ = menuPrincipal();
                switch (MENU_)
                {
                    case 1:
                        Console.Clear();
                        regimen.menuPrincipal_();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        MostrarTotalesPorTipoAfiliacion();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        ConsultarLiquidacionesPorMesYAnio();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        ConsultarLiquidacionPorId();
                        Console.Clear();
                        break;
                    case 5:
                        OP = 'N';
                        break;
                }
            }
        }
        public void MostrarTotalesPorTipoAfiliacion()
        {
            titulos1();
            try
            {
                Console.SetCursorPosition(5, 15); Console.WriteLine("TIPO AFILIACIÓN       VALOR TOTAL CUOTAS MODERADORAS   VALOR TOTAL LIQUIDADO");
                var registrosContributivo = liquidacionoService.CargarRegistros().Where(i => i.TipoAfiliacion == "C").ToList();
                double totalCuotasContributivo = registrosContributivo.Sum(i => i.CuotaModeradora);
                double totalLiquidadoContributivo = registrosContributivo.Sum(i => i.ValorHospitalizacion);

                Console.SetCursorPosition(5, 17); Console.WriteLine("CONTRIBUTIVO          {0:C}                     {1:C}", totalCuotasContributivo, totalLiquidadoContributivo);
                var registrosSubsidiado = liquidacionoService.CargarRegistros().Where(i => i.TipoAfiliacion == "S").ToList();
                double totalCuotasSubsidiado = registrosSubsidiado.Sum(i => i.CuotaModeradora);
                double totalLiquidadoSubsidiado = registrosSubsidiado.Sum(i => i.ValorHospitalizacion);

                Console.SetCursorPosition(5, 18); Console.WriteLine("SUBSIDIADO            {0:C}                      {1:C}", totalCuotasSubsidiado, totalLiquidadoSubsidiado);

                Console.SetCursorPosition(70, 20); Console.WriteLine("Presione cualquier tecla para continuar.");
                Console.SetCursorPosition(110, 20);
                Console.ReadKey();
                Console.Clear();
            }
            catch (IOException)
            {
                // Manejo de excepciones
            }
        }

        public void ConsultarLiquidacionesPorMesYAnio()
        {
            char OP = 'S';
            while (OP == 'S')
            {
                try
                {
                    titulos2();
                    Console.SetCursorPosition(5, 17); Console.Write("INGRESE EL MES : (1-12)");
                    Console.SetCursorPosition(30, 17); int mes = int.Parse(Console.ReadLine());

                    Console.SetCursorPosition(5, 18); Console.Write("INGRESE EL AÑO : (EJEMPLO 2023)");
                    Console.SetCursorPosition(37, 18); int anio = int.Parse(Console.ReadLine());

                    var liquidacionesFiltradas = liquidacionoService.CargarRegistros()
                        .Where(i => i.FechaLiquidacion.Month == mes && i.FechaLiquidacion.Year == anio)
                        .ToList();

                    if (liquidacionesFiltradas.Any())
                    {
                        double totalCuotasModeradoras = liquidacionesFiltradas.Sum(i => i.CuotaModeradora);
                        double totalValoresLiquidados = liquidacionesFiltradas.Sum(i => i.ValorHospitalizacion);

                        Console.SetCursorPosition(5, 20); Console.WriteLine("LIQUIDACIONES ENCONTRADAS PARA EL MES {0} DEL AÑO {1}:", mes, anio);

                        Console.SetCursorPosition(5, 22); Console.WriteLine("ID LIQUIDACIÓN  FECHA LIQUIDACIÓN       ID PACIENTE     TIPO AFILIACIÓN    SALARIO DEVENGADO    VALOR DE HOSPITALIZACIÓN");

                        int X = 24;
                        foreach (var liquidacion in liquidacionesFiltradas)
                        {
                            Console.SetCursorPosition(5, X); Console.WriteLine($"{liquidacion.IdLiquidacion}");
                            Console.SetCursorPosition(21, X); Console.WriteLine($"{liquidacion.FechaLiquidacion}");
                            Console.SetCursorPosition(46, X); Console.WriteLine($"{liquidacion.IdPaciente}");
                            Console.SetCursorPosition(67, X); Console.WriteLine($"{liquidacion.TipoAfiliacion}");
                            Console.SetCursorPosition(81, X); Console.WriteLine($"{liquidacion.SalarioDevengado:C}");
                            Console.SetCursorPosition(102, X); Console.WriteLine($"{liquidacion.ValorHospitalizacion:C}");
                            X++;
                        }

                        Console.SetCursorPosition(5, X + 1); Console.WriteLine("TOTALES:");
                        Console.SetCursorPosition(5, X + 2); Console.WriteLine("Total Cuotas Moderadoras: {0:C}", totalCuotasModeradoras);
                        Console.SetCursorPosition(5, X + 3); Console.WriteLine("Total Valores Liquidados: {0:C}", totalValoresLiquidados);
                        do
                        {
                            Console.SetCursorPosition(70, X + 5); Console.WriteLine("¿Desea continuar? S/N : ");
                            Console.SetCursorPosition(94, X + 5); OP = Convert.ToChar(Console.ReadLine());
                            OP = char.ToUpper(OP);
                            Console.Clear();
                        } while ((OP != 'S') && (OP != 'N'));
                    }
                    else
                    {
                        int X = 24;
                        Console.SetCursorPosition(5, 20); Console.WriteLine("No se encontraron liquidaciones para el mes {0} del año {1}.", mes, anio);
                        do
                        {
                            Console.SetCursorPosition(70, X + 5); Console.WriteLine("¿Desea continuar? S/N : ");
                            Console.SetCursorPosition(94, X + 5); OP = Convert.ToChar(Console.ReadLine());
                            OP = char.ToUpper(OP);
                            Console.Clear();
                        } while ((OP != 'S') && (OP != 'N'));
                    }
                }
                catch (FormatException)
                {
                    Console.SetCursorPosition(35, 25); Console.Write("Por favor no deje campos incorrectos");
                    Console.ReadKey();
                }
            }
        }

        public void ConsultarLiquidacionPorId()
        {
            char OP = 'S';
            while (OP == 'S')
            {
                try
                {
                    titulos3();
                    Console.SetCursorPosition(5, 17); Console.Write("Ingrese el ID de liquidación que desea buscar: ");
                    Console.SetCursorPosition(52, 17); string idLiquidacionBuscada = Console.ReadLine();

                    var liquidacionesFiltradas = liquidacionoService.CargarRegistros()
                        .Where(i => i.IdLiquidacion == idLiquidacionBuscada)
                        .ToList();

                    if (liquidacionesFiltradas.Any())
                    {
                        Console.SetCursorPosition(5, 19); Console.WriteLine("LIQUIDACIONES ENCONTRADAS CON ID {0}:", idLiquidacionBuscada);

                        Console.SetCursorPosition(5, 21); Console.WriteLine("ID LIQUIDACIÓN  FECHA LIQUIDACIÓN       ID PACIENTE     TIPO AFILIACIÓN    SALARIO DEVENGADO    VALOR DE HOSPITALIZACIÓN");

                        int X = 23;
                        foreach (var liquidacion in liquidacionesFiltradas)
                        {
                            Console.SetCursorPosition(5, X);
                            Console.WriteLine($"{liquidacion.IdLiquidacion}              {liquidacion.FechaLiquidacion}   {liquidacion.IdPaciente}            {liquidacion.TipoAfiliacion}         {liquidacion.SalarioDevengado:C}                    {liquidacion.ValorHospitalizacion:C}");
                            X++;
                        }
                        do
                        {
                            Console.SetCursorPosition(70, X + 5); Console.WriteLine("¿Desea continuar? S/N : ");
                            Console.SetCursorPosition(94, X + 5); OP = Convert.ToChar(Console.ReadLine());
                            OP = char.ToUpper(OP);
                            Console.Clear();
                        } while ((OP != 'S') && (OP != 'N'));
                    }
                    else
                    {
                        int X = 24;
                        Console.SetCursorPosition(5, 19); Console.WriteLine("No se encontraron liquidaciones con el ID {0}.", idLiquidacionBuscada);
                        do
                        {
                            Console.SetCursorPosition(70, X + 5); Console.WriteLine("¿Desea continuar? S/N : ");
                            Console.SetCursorPosition(94, X + 5); OP = Convert.ToChar(Console.ReadLine());
                            OP = char.ToUpper(OP);
                            Console.Clear();
                        } while ((OP != 'S') && (OP != 'N'));
                    }
                }
                catch (FormatException)
                {
                    Console.SetCursorPosition(35, 25); Console.Write("Por favor no deje campos incorrectos");
                    Console.ReadKey();
                }
            }
        }
        public void titulos1()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(70, 9); Console.WriteLine("INFORMACION DE IPS | LIQUIDACIONES TOTALES");
        }
        public void titulos2()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(67, 9); Console.WriteLine("INFORMACION DE IPS | LIQUIDACIONES POR TIEMPO");
        }
        public void titulos3()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(71, 9); Console.WriteLine("INFORMACION DE IPS | CONSULTA SELECTIVA");
        }

    }
}
