using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace Datos
{
    public class LiquidacionRepository
    {
        string filePath = "LIQUIDACIONES.txt";
        public string GuardarRegistros(Liquidacion liquidacion)
        {
            var write = new StreamWriter(filePath, true);
            write.WriteLine(liquidacion.ToString());
            write.Close();
            return $"El registro se ha guardado correctamente";
        }

        public string Guardar(List<Liquidacion> liquidacionList)
        {
            var write = new StreamWriter(filePath);
            foreach (var i in liquidacionList)
            {
                write.WriteLine(i.ToString());
            }
            write.Close();
            return $"Registros cargados";
        }

        public List<Liquidacion> CargarRegistros()
        {
            var liquidacionList = new List<Liquidacion>();
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            liquidacionList.Add(Map(reader.ReadLine()));
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                return null;
            }
            return liquidacionList;
        }
        private Liquidacion Map(string line)
        {
            var liquidacion = new Liquidacion();
            var datos = line.Split(';');
            liquidacion.IdLiquidacion = datos[0];
            liquidacion.FechaLiquidacion = Convert.ToDateTime(datos[1]);
            liquidacion.IdPaciente = datos[2];
            liquidacion.TipoAfiliacion = datos[3];
            liquidacion.SalarioDevengado = Convert.ToDouble(datos[4]);
            liquidacion.ValorHospitalizacion = Convert.ToDouble(datos[5]);
            liquidacion.Tarifa = Convert.ToDouble(datos[6]);
            liquidacion.CuotaModeradora = Convert.ToDouble(datos[7]);
            liquidacion.TopeMax = datos[8];
            return liquidacion;

        }
    }
}
