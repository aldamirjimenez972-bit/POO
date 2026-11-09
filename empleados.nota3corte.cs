using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace nota_de_electiva_aldamir
{
    internal class Program
    {
      
        
         


  

              public class Empleado
            {
                private string nombre;
                private int edad;
                protected decimal salarioBase;

                public Empleado(string nombre, int edad, decimal salarioBase)
                {
                    this.nombre = nombre;
                    this.edad = edad;
                    this.salarioBase = salarioBase;
                }

                public string Nombre
                {
                    get { return nombre; }
                    set
                    {
                        if (string.IsNullOrWhiteSpace(value))
                            throw new ArgumentException("El nombre no debe estar vacío.");
                        nombre = value;
                    }
                }

                public int Edad
                {
                    get { return edad; }
                    set
                    {
                        if (value < 18 || value > 65)
                            throw new ArgumentException("Edad no válida para trabajar.");
                        edad = value;
                    }
                }

                public virtual decimal CalcularSalario()
                {
                    return salarioBase;
                }

                public virtual string MostrarInfo()
                {
                    return $"Nombre: {nombre}, Edad: {edad}, Salario total: {CalcularSalario():N0}";
                }
            }

            public class Gerente : Empleado
            {
                private const decimal bonoGestion = 800000;

                public Gerente(string nombre, int edad, decimal salarioBase)
                    : base(nombre, edad, salarioBase) { }

                public override decimal CalcularSalario()
                {
                    return salarioBase + bonoGestion;
                }

                public override string MostrarInfo()
                {
                    return base.MostrarInfo() + " | Rol: gerente";
                }
            }

            public static void Main(string[] args)
            {
                List<Empleado> empleados = new List<Empleado> {
            new Gerente("Aldamir jimenez orozco", 18, 4000000),
            new Gerente("David jimenez orozco", 23, 7000000)
        };

                foreach (Empleado e in empleados)
                {
                    Console.WriteLine(e.MostrarInfo());
                }
            }
        }
    }
    
