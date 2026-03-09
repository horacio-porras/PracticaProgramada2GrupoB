**1. Grupo B:**
* Adrian Morales Robles
* Cheryl Robles Quesada
* Horacio Porras Marin
* Marypaz Vargas Arce


<br>**2. Repositorio:**
https://github.com/horacio-porras/PracticaProgramada2GrupoB

<br>**3. Especificación básica del proyecto:**

a) Arquitectura
* 3 capas / 3 proyectos:
  * MiPrimeraWeb: ASP.NET Core MVC (capa de presentación: controladores, vistas, wwwroot, configuración en Program.cs).
  * MiPrimeWebBLL: Lógica de negocio (servicios, DTOs, mapeos con AutoMapper, respuestas estandarizadas).
  * MiPrimeraWebDAL: Acceso a datos (entidades, DbContext de EF Core y repositorio genérico).

b) NuGet usados
* AutoMapper (16.0.0)
* Microsoft.EntityFrameworkCore (10.0.3)
* Microsoft.EntityFrameworkCore.Sqlite (10.0.3)
* Microsoft.EntityFrameworkCore.Design (10.0.3)
* Microsoft.EntityFrameworkCore.Tools (10.0.3)

c) SOLID + Patrones
* SOLID aplicado (evidente en el código):
  * SRP: separación clara por capas (UI, negocio, datos).
  * DIP: uso de interfaces (ICategoriaServicio, IProductoServicio, IRepositorioGenerico<T>) con inyección de dependencias.
  * ISP/OCP (parcial): contratos de servicios específicos y repositorio genérico reutilizable por entidad.
* Patrones utilizados:
  * Repository (Genérico): IRepositorioGenerico<T> / RepositorioGenerico<T>.
  * Service Layer: CategoriaServicio, ProductoServicio.
  * DTO: CategoriaDto, ProductoDto.
  * Dependency Injection: registro en Program.cs con AddScoped.
  * Mapper: AutoMapper (MapeoClases).
  * Response Wrapper: CustomResponse<T>.
  * Middleware de excepciones globales: MiddlewareGlobalExceptionHandler.
  * MVC: controladores y vistas en la capa web.
