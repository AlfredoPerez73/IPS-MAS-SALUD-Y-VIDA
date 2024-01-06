using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Liquidacion
    {
        public string IdLiquidacion { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public string IdPaciente { get; set; }
        public string TipoAfiliacion { get; set; }
        public double SalarioDevengado { get; set; }
        public double ValorHospitalizacion { get; set; }
        public double Tarifa { get; set; }
        public double CuotaModeradora { get; set; }
        public string TopeMax { get; set; }

        public Liquidacion(string idLiquidacion, DateTime fechaLiquidacion, string idPaciente, string tipoAfiliacion, double salarioDevengado,
            double valorHospitalizacion, double tarifa, double cuotaModeradora, string topeMax)
        {
            this.IdLiquidacion = idLiquidacion;
            this.FechaLiquidacion = fechaLiquidacion;
            this.IdPaciente = idPaciente;
            this.TipoAfiliacion = tipoAfiliacion;
            this.SalarioDevengado = salarioDevengado;
            this.ValorHospitalizacion = valorHospitalizacion;
            this.Tarifa = tarifa;
            this.CuotaModeradora = cuotaModeradora;
            this.TopeMax = topeMax;
        }

        public Liquidacion() { }

        public double tarifa()
        {
            if (TipoAfiliacion == "C")
            {
                if (SalarioDevengado < 2320000)
                {
                    Tarifa = 0.15;
                }
                else if ((SalarioDevengado >= 2320000) && (SalarioDevengado < 5800000))
                {
                    Tarifa = 0.20;
                }
                else if (SalarioDevengado >= 5800000)
                {
                    Tarifa = 0.25;
                }
            }else if (TipoAfiliacion == "S" || CuotaModeradora >= 200000)
            {
                    Tarifa = 0.05;
            }
            return Tarifa;
        }

        public string tope()
        {
            if (TipoAfiliacion == "S" || CuotaModeradora >= 200000)
            {
                
                 TopeMax = "Si Aplico";
                
            } else 
            {
                TopeMax = "No Aplico";
            }
            
            return TopeMax;
        }
        public double CalculoCuotaModeradora()
        {
            if (TipoAfiliacion == "C")
            {
                if (SalarioDevengado <= 2320000){
                    Tarifa = tarifa();
                    CuotaModeradora = ValorHospitalizacion * Tarifa;
                }
                else if ((SalarioDevengado >= 2320000)&&(SalarioDevengado < 5800000)){
                    Tarifa = tarifa();
                    CuotaModeradora = ValorHospitalizacion * Tarifa;
                }else if (SalarioDevengado >= 5800000){
                    Tarifa = tarifa();
                    CuotaModeradora = ValorHospitalizacion * Tarifa;
                }

                if (CuotaModeradora >= 250000 && CuotaModeradora < 900000)
                {
                    CuotaModeradora = 250000;
                }
                else if (CuotaModeradora >= 900000 && CuotaModeradora <= 1500000 )
                {
                    CuotaModeradora = 900000;
                }
                else if (CuotaModeradora >= 1500000)
                {
                    CuotaModeradora = 1500000;
                }
            }
            else if (TipoAfiliacion == "S")
            {
                CuotaModeradora = ValorHospitalizacion * 0.05;
                if (CuotaModeradora >= 200000)
                {
                    TopeMax = tope();
                    CuotaModeradora = 200000;
                }else if (CuotaModeradora < 200000)
                {
                    CuotaModeradora = ValorHospitalizacion * 0.05;
                    TopeMax = tope();
                }
                
            }
            return CuotaModeradora;
        }
        public override string ToString()
        {
            return $"{IdLiquidacion};{FechaLiquidacion};{IdPaciente};{TipoAfiliacion};{SalarioDevengado};" +
                $"{ValorHospitalizacion};{Tarifa};{CuotaModeradora};{TopeMax}";
        }
    }
}
