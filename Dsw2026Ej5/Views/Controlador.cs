using Dsw2026Ej5.Data;
using Dsw2026Ej5.Domain;

namespace Dsw2026Ej5.Views;

public class Controlador
{
    public static List<VehiculoViewModel> GetVehiculos()
    {
        Persistencia.InicializarDatos();

        List<VehiculoViewModel> vehiculos = new List<VehiculoViewModel>();
        foreach (Vehiculo vehiculo in Persistencia.GetVehiculos())
        {
            vehiculos.Add(new VehiculoViewModel(vehiculo));
        }
        return vehiculos;


    }

    public static (double, double) CalcularConsumos(Dictionary<string, double> vehiculos)
    {

        Persistencia.InicializarDatos();
        double consumoElectricos = 0;
        double consumoCombustible = 0;
        foreach (KeyValuePair<string, double> entry in vehiculos)
        {
            double consumo = 0;
            Vehiculo? vehiculo = Persistencia.GetVehiculo(entry.Key);
            if (vehiculo != null)
            {
                consumo = vehiculo.CalcularConsumo(entry.Value);



                consumoElectricos += vehiculo.EsDe(VehiculoTipo.Electrico) ? consumo : 0;
                consumoCombustible += vehiculo.EsDe(VehiculoTipo.Combustible) ? consumo : 0;
            }
        }
        return (consumoElectricos, consumoCombustible);
    }
    
}
