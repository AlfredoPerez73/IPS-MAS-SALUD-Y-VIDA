using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Logica;
using System.IO;

namespace IPS_MAS_SALUD_Y_VIDA
{
    public class MenuPrincipal
    {
        private Liquidacion liquidacion;
        private LiquidacionService liquidacionoService = new LiquidacionService();
        public MenuPrincipal(Liquidacion liquidacion)
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
                Console.SetCursorPosition(76, 9); Console.WriteLine("M E N U  P R I N C I P A L");
                Console.SetCursorPosition(75, 13); Console.WriteLine("1. REGISTRO DE PACIENTES");
                Console.SetCursorPosition(75, 14); Console.WriteLine("2. CONSULTA TOTAL DE PACIENTES");
                Console.SetCursorPosition(75, 15); Console.WriteLine("3. CONSULTA POR FILTRO");
                Console.SetCursorPosition(75, 16); Console.WriteLine("4. ACTUALIZACION DE DATOS");
                Console.SetCursorPosition(75, 17); Console.WriteLine("5. ELIMINAR PACIENTE");
                Console.SetCursorPosition(75, 20); Console.WriteLine("6. SALIR");
                do
                {
                    Console.SetCursorPosition(75, 25); Console.WriteLine("Seleccione una opcion: ");
                    Console.SetCursorPosition(98, 25); OPC = Convert.ToInt32(Console.ReadLine());
                    Console.SetCursorPosition(98, 25); Console.WriteLine("         ");
                    Console.SetCursorPosition(98, 29); Console.WriteLine("Opcion no valida");
                } while ((OPC < 1) || (OPC > 6));
                Console.SetCursorPosition(98, 25); Console.WriteLine("                                     ");
                Console.SetCursorPosition(98, 29); Console.WriteLine("                                     ");

            }
            catch (FormatException)
            {
                Console.SetCursorPosition(98, 29); Console.WriteLine("Opcion no valida");
            }
            return OPC;
        }

        public void menuPrincipal_()
        {
            MenuSecundario secundario = new MenuSecundario(liquidacion);
            int MENU_;
            char OP = 'S';
            while (OP == 'S')
            {
                MENU_ = menuPrincipal();
                switch (MENU_)
                {
                    case 1:
                        Console.Clear();
                        registro();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        MostrarRegistro();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        secundario.menuPrincipal_();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        ModificarRegistro();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        EliminarRegistro();
                        Console.Clear();
                        break;
                    case 6:
                        OP = 'N';
                        break;
                }
            }
        }

        public void registro()
        {
            string IdLiquidacion;
            DateTime Fecha;
            string IdPaciente;
            string TipoAfiliacion;
            double SalarioDevengado;
            double ValorHospitalizacion;

            char OP = 'S';
            while (OP == 'S')
            {
                try
                {
                    titulos1();
                    Console.SetCursorPosition(35, 11); Console.WriteLine("ID DE LIQUIDACIÓN        : ");
                    Console.SetCursorPosition(35, 12); Console.WriteLine("FECHA                    : ");
                    Console.SetCursorPosition(35, 13); Console.WriteLine("ID DE PACIENTE           : ");
                    Console.SetCursorPosition(35, 14); Console.WriteLine("TIPO DE AFILIACIÓN       : ");
                    Console.SetCursorPosition(35, 15); Console.WriteLine("SALARIO DEVENGADO        : ");
                    Console.SetCursorPosition(35, 16); Console.WriteLine("VALOR DE HOSPITALIZACIÓN : ");
                    Console.SetCursorPosition(63, 11); IdLiquidacion = Console.ReadLine();
                    if (!liquidacionoService.ExisteIdLiquidacion(IdLiquidacion))
                    {
                        Console.SetCursorPosition(35, 25); Console.WriteLine("Formato correcto de fecha y hora: DD/MM/YY 00:00:00");
                        Console.SetCursorPosition(63, 12); Fecha = Convert.ToDateTime(Console.ReadLine());
                        Console.SetCursorPosition(35, 25); Console.WriteLine("                                                           ");
                        Console.SetCursorPosition(63, 13); IdPaciente = Console.ReadLine().ToUpper();
                        do
                        {
                            Console.SetCursorPosition(35, 25); Console.WriteLine("Digite S: Subsidiado o Digite C: Contributivo");
                            Console.SetCursorPosition(63, 14); TipoAfiliacion = Console.ReadLine().ToUpper();

                        } while ((TipoAfiliacion != "S") && (TipoAfiliacion != "C"));
                        Console.SetCursorPosition(35, 25); Console.WriteLine("                                                         ");
                        do
                        {
                            Console.SetCursorPosition(35, 25); Console.WriteLine("Recuerde que si el tipo de regimen es subsidiado el sal Devengado es $ 0");
                            Console.SetCursorPosition(63, 15); SalarioDevengado = Convert.ToDouble(Console.ReadLine());
                        } while (SalarioDevengado < 0);
                        Console.SetCursorPosition(35, 25); Console.WriteLine("                                                                             ");
                        do
                        {
                            Console.SetCursorPosition(63, 16); ValorHospitalizacion = Convert.ToDouble(Console.ReadLine());
                        } while (ValorHospitalizacion < 0);
                        Liquidacion liquidacion = new Liquidacion(IdLiquidacion, Fecha, IdPaciente, TipoAfiliacion, SalarioDevengado, ValorHospitalizacion, 0, 0, "");
                        liquidacion.tarifa();
                        liquidacion.CalculoCuotaModeradora();
                        liquidacion.tope();
                        Console.SetCursorPosition(34, 25); Console.WriteLine(liquidacionoService.GuardarRegistros(liquidacion));
                    }
                    else
                    {
                        Console.SetCursorPosition(35, 25); Console.WriteLine("Se encontró un registro con el ID de la liquidación proporcionado.");
                    }
                    do
                    {
                        Console.SetCursorPosition(34, 18); Console.WriteLine("¿Desea continuar? S/N : ");
                        Console.SetCursorPosition(58, 18); OP = Convert.ToChar(Console.ReadLine());
                        OP = char.ToUpper(OP);
                        Console.Clear();
                    } while ((OP != 'S') && (OP != 'N'));
                }
                catch (FormatException)
                {
                    Console.SetCursorPosition(35, 25); Console.Write("Por favor no deje campos incorrectos");
                }
            }
        }

        public void MostrarRegistro()
        {
            titulos4();
            try
            {
                Console.SetCursorPosition(5, 15); Console.WriteLine("ID LIQUIDACIÓN  FECHA LIQUIDACIÓN       ID PACIENTE     TIPO AFILIACIÓN    SALARIO DEVENGADO    VALOR DE HOSPITALIZACIÓN   TARIFA     CUOTA MODERADA   TOPE MÁX");
                int X = 17;
                var lista = liquidacionoService.CargarRegistros();
                if(lista != null){
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
                else{
                    
                    Console.SetCursorPosition(75, 25); Console.WriteLine("No hay registros para mostrar. ");
                    Console.SetCursorPosition(105, 25); Console.ReadKey();
                }
            }catch (IOException)
            {
            }
        }

        public void ModificarRegistro()
        {
            char OP = 'S';
            while (OP == 'S')
            {
                try
                {
                    Console.Clear();
                    titulos2();
                    Console.SetCursorPosition(51, 11); Console.Write("Ingrese el ID de la liquidación que desea modificar: ");
                    Console.SetCursorPosition(104, 11); string idAModificar = Console.ReadLine();
                    if (liquidacionoService.ExisteIdLiquidacion(idAModificar))
                    {
                        var liquidacionAModificar = liquidacionoService.CargarRegistros().FirstOrDefault(p => p.IdLiquidacion == idAModificar);

                        Console.SetCursorPosition(51, 13); Console.Write("NUEVO VALOR DE HOSPITALIZACION : ");
                        Console.SetCursorPosition(84, 13); double nuevoValorHospitalizacion = Convert.ToDouble(Console.ReadLine());

                        liquidacionAModificar.ValorHospitalizacion = nuevoValorHospitalizacion;
                        liquidacionAModificar.CalculoCuotaModeradora();

                        string resultado = liquidacionoService.ModificarRegistro(idAModificar, liquidacionAModificar);

                        Console.SetCursorPosition(57, 18); Console.WriteLine(resultado);
                        do
                        {
                            Console.SetCursorPosition(68, 20); Console.WriteLine("¿Desea continuar? S/N : ");
                            Console.SetCursorPosition(92, 20); OP = Convert.ToChar(Console.ReadLine());
                            OP = char.ToUpper(OP);
                            Console.Clear();
                        } while ((OP != 'S') && (OP != 'N'));
                    }
                    else
                    {
                        Console.SetCursorPosition(60, 15); Console.WriteLine("No se encontró un registro con el ID de la liquidación proporcionado.");
                        Console.SetCursorPosition(130, 15); Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    Console.SetCursorPosition(35, 25); Console.Write("Por favor no deje campos incorrectos");
                    Console.ReadKey();
                }
            }
        }
        public void EliminarRegistro()
        {
            Console.Clear();
            titulos2();
            Console.SetCursorPosition(51, 11); Console.Write("Ingrese el ID de liquidación que desea eliminar: ");
            Console.SetCursorPosition(99, 11); string idAEliminar = Console.ReadLine();

            string mensaje = liquidacionoService.EliminarRegistro(idAEliminar);

            Console.SetCursorPosition(57, 18); Console.WriteLine(mensaje);
            Console.SetCursorPosition(68, 20); Console.WriteLine("Presione Enter para continuar.");
            Console.SetCursorPosition(99, 20); Console.ReadLine();
        }

        public void titulos1()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(80, 9); Console.WriteLine("R E G I S T R O");
        }
        public void titulos2()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(80, 9); Console.WriteLine("M O D I F I C A R");
        }
        public void titulos4()
        {
            Console.SetCursorPosition(75, 6); Console.WriteLine("UNIVERSIDAD POPULAR DEL CESAR");
            Console.SetCursorPosition(77, 7); Console.WriteLine("TALLER DE PROGRAMACION III");
            Console.SetCursorPosition(68, 8); Console.WriteLine("SOFTWARE DE LIQUIDACIÓN IPS MAS SALUD Y VIDA");
            Console.SetCursorPosition(80, 9); Console.WriteLine("INFORMACION DE IPS");
        }
    }
}
