using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Datos;
using System.IO;

namespace Logica
{
    public class LiquidacionService
    {
        private LiquidacionRepository liquidacionRepository = null;
        private List<Liquidacion> liquidacionList = null;
        public LiquidacionService()
        {
            liquidacionRepository = new LiquidacionRepository();
            liquidacionList = liquidacionRepository.CargarRegistros();

        }

        public String GuardarRegistros(Liquidacion liquidacion)
        {
            if (liquidacion.IdLiquidacion == null || liquidacion.IdPaciente == null
                || liquidacion.ValorHospitalizacion == 0)
            {
                return $"Campos nulos";
            }
            var message = (liquidacionRepository.GuardarRegistros(liquidacion));
            liquidacionList = liquidacionRepository.CargarRegistros();
            return message;
        }

        public bool ExisteIdLiquidacion(string idLiquidacion)
        {
            return CargarRegistros().Any(liquidacion => liquidacion.IdLiquidacion == idLiquidacion);
        }

        public List<Liquidacion> CargarRegistros()
        {
            var msg = liquidacionRepository.CargarRegistros();
            return msg;
        }
        public string EliminarRegistro(string idAEliminar)
        {
            try
            {
                var pacienteAeliminar = liquidacionList.FirstOrDefault(p => p.IdLiquidacion == idAEliminar);

                if (pacienteAeliminar != null)
                {
                    liquidacionList.Remove(pacienteAeliminar);
                    liquidacionRepository.Guardar(liquidacionList);
                    return "Registro eliminado con éxito.";
                }
                else
                {
                    return "No se encontró un registro con el ID de la liquidacion proporcionado.";
                }
            }
            catch (IOException)
            {
                return "Ocurrió un error al intentar eliminar el registro.";
            }
        }

        public string ModificarRegistro(string idAModificar, Liquidacion nuevaLiquidacion)
        {
            try
            {
                var liquidacionAModificar = liquidacionList.FirstOrDefault(p => p.IdLiquidacion == idAModificar);

                if (liquidacionAModificar != null)
                {
                    // Copiar todas las propiedades de nuevaLiquidacion en liquidacionAModificar
                    liquidacionAModificar.ValorHospitalizacion = nuevaLiquidacion.ValorHospitalizacion;
                    // ...

                    liquidacionRepository.Guardar(liquidacionList);
                    return "Registro modificado con éxito.";
                }
                else
                {
                    return "No se encontró un registro con el ID de la liquidación proporcionado.";
                }
            }
            catch (IOException)
            {
                return "Ocurrió un error al intentar modificar el registro.";
            }
        }

        
    }
}
