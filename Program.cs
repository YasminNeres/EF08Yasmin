using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static System.Console;


public class InstitutoContext : DbContext
{
     public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Modulo> Modulos { get; set; }
       
     public DbSet<Matricula> Matriculas { get; set; }
    public string connString { get; private set; }

    public InstitutoContext()
    {
        var database = "EF08Yasmin"; // "EF{XX}Nombre" => EF00Santi
        connString = $"Server=185.60.40.210\\SQLEXPRESS,58015;Database={database};User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
        //connString = $"Server=(localdb)\\mssqllocaldb;Database=EFE8Yasmin;Trusted_Connection=True;MultipleActiveResultSets=true";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(connString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matricula>().HasKey(be => new
            {
                be.AlumnoId,
                be.ModuloId
            });
        }
}
[Table("Alumno")]
public class Alumno
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AlumnoId { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public decimal Efectivo { get; set; }

    public string Pelo {get;set;}


    public List<Matricula> Matriculacion { get; } = new List<Matricula>();
    /*
    */
}

[Table("Modulo")]
public class Modulo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ModuloId { get; set; }
    public string Nombre { get; set; }

    public List<Matricula> Matriculado { get; } = new List<Matricula>();
    /*
    */

}

[Table("Matricula")]
public class Matricula
{
    public int MatriculaId { get; set; }  

    public int AlumnoId { get; set; }
    public int ModuloId { get; set; }
}

class Program
{
    static void GenerarDatos()
    {
        using (var db = new InstitutoContext())
        {
            // Borrar todo
            db.Alumnos.RemoveRange(db.Alumnos);
            db.Modulos.RemoveRange(db.Modulos);
            db.Matriculas.RemoveRange(db.Matriculas);
            db.SaveChanges();

            // Añadir Alumnos
            // Id de 1 a 7
             db.Alumnos.Add(new Alumno { AlumnoId = 1 , Nombre = "Pepe" ,Edad = 18, Efectivo= 100 , Pelo="Rubio"});
             db.Alumnos.Add(new Alumno { AlumnoId = 2 , Nombre = "Luis" ,Edad = 20, Efectivo= 100 , Pelo="Moreno"});
             db.Alumnos.Add(new Alumno { AlumnoId = 3 , Nombre = "Marta" ,Edad = 18, Efectivo= 100 , Pelo="Castaño"});
             db.Alumnos.Add(new Alumno { AlumnoId = 4 , Nombre = "Laura" ,Edad = 20, Efectivo= 100 , Pelo="Rubio"});
             db.Alumnos.Add(new Alumno { AlumnoId = 5 , Nombre = "Paula" ,Edad = 18, Efectivo= 100 , Pelo="Moreno"});
             db.Alumnos.Add(new Alumno { AlumnoId = 6 , Nombre = "Unai" ,Edad = 20, Efectivo= 100 , Pelo="Castaño"});
             db.Alumnos.Add(new Alumno { AlumnoId = 7 , Nombre = "Maria" ,Edad = 18, Efectivo= 100 , Pelo="Rubio"});
             db.SaveChanges();

            // Añadir Módulos
            // Id de 1 a 10
            db.Modulos.Add(new Modulo { ModuloId = 1 , Nombre = "Lengua" });
            db.Modulos.Add(new Modulo { ModuloId = 2 , Nombre = "Euskera" });
            db.Modulos.Add(new Modulo { ModuloId = 3 , Nombre = "Ingles" });
            db.Modulos.Add(new Modulo { ModuloId = 4 , Nombre = "Frances" });
            db.Modulos.Add(new Modulo { ModuloId = 5 , Nombre = "Matematicas" });
            db.Modulos.Add(new Modulo { ModuloId = 6 , Nombre = "Informatica" });
            db.Modulos.Add(new Modulo { ModuloId = 7 , Nombre = "Economia" });
            db.Modulos.Add(new Modulo { ModuloId = 8 , Nombre = "Fisica" });
            db.Modulos.Add(new Modulo { ModuloId = 9 , Nombre = "Tecnologia" });
            db.Modulos.Add(new Modulo { ModuloId = 10 , Nombre = "Biologia" });
            db.SaveChanges();

            // Matricular Alumnos en Módulos

             var alumno = db.Alumnos.OrderBy(a => a.AlumnoId);
             var modulo = db.Modulos.OrderBy(m => m.ModuloId);
            

            db.Matriculas.Add(new Matricula{AlumnoId=1, ModuloId=4 ,MatriculaId=1});
            db.Matriculas.Add(new Matricula{AlumnoId=2, ModuloId=3,MatriculaId=2});
            db.Matriculas.Add(new Matricula{AlumnoId=3, ModuloId=2,MatriculaId=3});
            db.Matriculas.Add(new Matricula{AlumnoId=4, ModuloId=5,MatriculaId=4});
            db.Matriculas.Add(new Matricula{AlumnoId=5, ModuloId=8,MatriculaId=5});
            db.Matriculas.Add(new Matricula{AlumnoId=6, ModuloId=9,MatriculaId=6});
            db.Matriculas.Add(new Matricula{AlumnoId=7, ModuloId=1,MatriculaId=7});
            db.Matriculas.Add(new Matricula{AlumnoId=1, ModuloId=1,MatriculaId=8});
            db.Matriculas.Add(new Matricula{AlumnoId=5, ModuloId=1,MatriculaId=9});
            db.Matriculas.Add(new Matricula{AlumnoId=6, ModuloId=3,MatriculaId=10});
              db.SaveChanges();

        }
    }

  static void BorrarMatriculaciones()

    { 
         using (var db = new InstitutoContext())
        {
           var matricula = db.Matriculas.OrderBy(ma => ma.MatriculaId);
        
               db.Remove(matricula);
               db.SaveChanges();

        }
    }
    static void RealizarQuery()
    {
        using (var db = new InstitutoContext())
        {
           //Filtering
           //Seleccionar los registros de edad igual a 18
           var qry1 = db.Alumnos.Where( a => a.Edad == 18);
           WriteLine("Query 1 ");
          foreach (var item in qry1)
           {
              WriteLine( item );
           }

            // Alunos con pelo Rubio 
            var col = db.Alumnos.Where(o => o.Pelo == "Rubio");
             foreach (var lista in col)
            {
                Console.WriteLine(lista.Nombre);
            }
          
           //Return Anonymous Type
           //Seleccionar todos los alumnos 
           WriteLine("Query 2 ");

            var col2 = db.Alumnos.Select(a => new
            {
            Nombre = a.Nombre,
           
            } );
               foreach (var lista in col2)
            {
                WriteLine(lista.Nombre);
            }
            

            //Ordering
             //Ordena los modulos
              WriteLine("Query3");
              var col3 = db.Matriculas.OrderBy(m => m.ModuloId);
               foreach (var lista2 in col3)
            {
                WriteLine(lista2.ModuloId);
            }

             WriteLine("Query4");
              //Joining
              var query4 = from c in db.Matriculas
                         join o in db.Alumnos on   
                         c.AlumnoId equals o.AlumnoId
                         select new {
                             o.Nombre,
                             o.Edad,
                             o.Efectivo,
                             o.Pelo
                         };

                    foreach (var lista3 in query4)
                    {
                        WriteLine(lista3);
                    }

                       WriteLine("Query5");
                       //Grouping  
                         var query5 = from o in db.Alumnos 
                         group o by o.AlumnoId into g
                         select new{
                             alumnoId = g.Key,
                             Total = g.Count()
                         };
                 foreach (var lista4 in query5)
                    {
                        WriteLine(lista4);
                    }
   
    WriteLine("Query6");
     //Paging 
     //Coger los 3 primeros registros de Edad = 20
     var query6 = (from o in db.Alumnos
     where o.Edad == 20
     select o).Take(3);

      foreach (var lista5 in query6)
                    {
                     WriteLine(lista5.Nombre);
                    }

           

 /* WriteLine("Query7");
  //Element Operators
  //Da fallo en el foreach

   var query7 = (from o in db.Modulos where o.ModuloId > 3
   select o).Last();
  
    foreach (var lista6 in query7)
         {
         WriteLine(lista6);
        }
*/
 // ToArray
 WriteLine("Query8");
 
    string[] nombres = (from c in db.Alumnos select c.Nombre).ToArray();
    foreach (var lista6 in nombres)
         {
         WriteLine(lista6);
        }
           //Da fallo
           /*WriteLine("Query9");
            // ToDictionary
            Dictionary<int, Alumno> coll = db.Alumnos.ToDictionary(c => c.AlumnoId);
            Dictionary<string, decimal> customerOrdersWithMaxCost = (from oc in
            (from o in db.Alumnos  
            join c in db.Alumnos on o.AlumnoId equals c.AlumnoId
            select new { c.Nombre, o.Efectivo })
            group oc by oc.Nombre into g
            select g).ToDictionary(g => g.Key, g => g.Max(oc => oc.Efectivo));

               foreach (var lista7 in customerOrdersWithMaxCost)
         {
         WriteLine(lista7);
        }*/

            

        


        }
    }

    static void Main(string[] args)
    {
        //GenerarDatos();
        //BorrarMatriculaciones();
        RealizarQuery();
    }

}

// dotnet ef migrations add InitialCreate
// dotnet ef database update