using System;
using System.Collections.Generic;
using System.Linq;

namespace nota_de_electiva_aldamir
{
    public class Empleado
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        protected decimal salarioBase;

        public Empleado(string nombre, int edad, decimal salarioBase)
        {
            Nombre = nombre;
            Edad = edad;
            this.salarioBase = salarioBase;
        }

        public virtual decimal CalcularSalario() => salarioBase;

        public virtual string MostrarInfo() =>
            $"Nombre: {Nombre}, Edad: {Edad}, Salario: {CalcularSalario():N0}";
    }

    public class Gerente : Empleado
    {
        public Gerente(string nombre, int edad, decimal salarioBase)
            : base(nombre, edad, salarioBase) { }

        public override decimal CalcularSalario() => salarioBase + 800000m;

        public override string MostrarInfo() => base.MostrarInfo() + " | Rol: Gerente";
    }

    public class HabitacionHotel
    {
        public int Numero { get; }
        public string Tipo { get; }
        public decimal Precio { get; private set; }
        public bool Ocupada { get; private set; }
        public string Huesped { get; private set; } = "Ninguno";

        public HabitacionHotel(int numero, string tipo, decimal precio)
        {
            Numero = numero;
            Tipo = tipo;
            Precio = precio;
        }

        public void AplicarDescuento(int porcentaje)
        {
            decimal nuevo = Precio - Precio * porcentaje / 100m;
            if (porcentaje <= 0 || nuevo <= 0)
                Console.WriteLine("Descuento no válido: el precio no puede quedar en 0 o negativo.");
            else
                Precio = nuevo;
        }

        public string ObtenerDatosResumen() =>
            $"[{Numero}] {Tipo} - ${Precio:N0} - {(Ocupada ? "Ocupada por: " + Huesped : "Libre")}";

        public void Reservar(string nombre) { Ocupada = true; Huesped = nombre; }
        public void Liberar() { Ocupada = false; Huesped = "Ninguno"; }
    }

    internal class Programs
    {
        // Pide un número de habitación y devuelve su posición en la lista (-1 si no existe).
        static int BuscarHabitacion(List<HabitacionHotel> lista)
        {
            Console.Write("Número de habitación: ");
            int.TryParse(Console.ReadLine(), out int num);
            int pos = lista.FindIndex(h => h.Numero == num);
            if (pos == -1) Console.WriteLine("Esa habitación no existe.");
            return pos;
        }

        static void Main(string[] args)
        {
            List<HabitacionHotel> habitaciones = new List<HabitacionHotel>
            {
                new HabitacionHotel(101, "Individual", 50000),
                new HabitacionHotel(102, "Doble", 90000),
                new HabitacionHotel(103, "Suite", 150000)
            };

            int opcion;
            do
            {
                Console.WriteLine("\n1. Ver habitaciones\n2. Reservar\n3. Liberar\n4. Aplicar descuento\n5. Salir");
                Console.Write("Elige una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                if (opcion == 1)
                {
                    foreach (var h in habitaciones) Console.WriteLine(h.ObtenerDatosResumen());
                }
                else if (opcion >= 2 && opcion <= 4)
                {
                    int pos = BuscarHabitacion(habitaciones);
                    if (pos == -1) continue;

                    if (opcion == 2)
                    {
                        Console.Write("Nombre del huésped: ");
                        habitaciones[pos].Reservar(Console.ReadLine() ?? "Sin nombre");
                    }
                    else if (opcion == 3)
                    {
                        habitaciones[pos].Liberar();
                    }
                    else
                    {
                        Console.Write("Porcentaje de descuento: ");
                        int.TryParse(Console.ReadLine(), out int desc);
                        habitaciones[pos].AplicarDescuento(desc);
                    }
                    Console.WriteLine(habitaciones[pos].ObtenerDatosResumen());
                }
            } while (opcion != 5);

            // Métricas finales con foreach
            int libres = 0, ocupadas = 0;
            decimal suma = 0;

            foreach (var h in habitaciones)
            {
                if (h.Ocupada) ocupadas++;
                else libres++;
                suma += h.Precio;
            }

            Console.WriteLine($"\nLibres: {libres} | Ocupadas: {ocupadas}");
            Console.WriteLine($"Precio promedio: ${suma / habitaciones.Count:N0}");

            // Ejemplo de empleados
            Console.WriteLine();
            Empleado e = new Gerente("Aldamir Jiménez", 21, 4000000);
            Console.WriteLine(e.MostrarInfo());

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}
