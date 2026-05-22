#Rotomdex - Catálogo Interactivo en .NET (N-Tier Architecture)

![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![Architecture](https://img.shields.io/badge/Architecture-N--Tier-success)
![Status](https://img.shields.io/badge/Status-Entregable_Finalizado-brightgreen)

## 📖 Descripción del Proyecto
**Rotomdex** es una aplicación web interactiva desarrollada en **ASP.NET Core MVC**. El proyecto implementa un catálogo completo de Pokémon, gestionando operaciones de lectura, escritura y autenticación mediante un sistema de almacenamiento basado enteramente en archivos JSON locales. 

El sistema ha sido estructurado siguiendo el patrón de **Arquitectura en Capas (N-Tier)** para garantizar la separación de responsabilidades, la escalabilidad y el mantenimiento del código.

---

##Características Principales

* **Arquitectura N-Tier:** Separación estricta entre `Presentation`, `Application`, `Domain` e `Infrastructure`.
* **Almacenamiento Local (JSON):** Persistencia de datos ligera y portable para entidades (`items.json`), usuarios (`usuarios.json`) y reseñas (`comentarios.json`) sin dependencia de servidores SQL.
* **Autenticación por Cookies:** Sistema de Login y Registro desarrollado a medida mediante validación de `Claims` y Cookies nativas de ASP.NET, operando independientemente de Microsoft Identity.
* **Sistema de Reseñas:** Los usuarios autenticados pueden documentar sus observaciones sobre cada Pokémon.
* **UI/UX Inmersiva:** Interfaz de usuario moderna impulsada por **Bootstrap** y animaciones de fondo renderizadas en WebGL mediante **Vanta.js**.

---

##Tecnologías y Herramientas

**Backend & Arquitectura:**
* C# / ASP.NET Core MVC
* Inyección de Dependencias (DI)
* `System.Text.Json` para serialización de datos.

**Frontend:**
* HTML5, CSS3, JavaScript
* Bootstrap 5 (Responsive Design)
* Vanta.js & Three.js (Efectos de partículas WebGL)

---

## ⚙️ Estructura del Proyecto

```text
📁 Solucion_CatalogoApp
 ├── 📂 Catalogo.Domain         # Modelos de negocio (Item, ComentarioModel, etc.)
 ├── 📂 Catalogo.Application    # Lógica de negocio y Servicios (ItemService)
 ├── 📂 Catalogo.Infrastructure # Acceso a datos (JsonItemRepository)
 └── 📂 Catalogo.Presentation   # Controladores, Vistas Razor, CSS/JS y Autenticación
      └── 📂 data               # Base de datos local (.json)
```
## Cláusula de Asistencia de Inteligencia Artificial (IA)

Este proyecto ha sido desarrollado bajo principios de integridad académica y transparencia. Se declara el uso de herramientas de Inteligencia Artificial (LLMs) como asistentes de desarrollo en las siguientes áreas específicas:

Ideación de UX/UI: Sugerencias de paletas de colores y refactorización de estilos en las vistas Razor mediante Bootstrap y la integración de la librería Vanta.js.

Pair-Programming y Debugging: Apoyo en el diagnóstico de errores de inyección de dependencias (InvalidOperationException) derivados de la reestructuración del proyecto monolítico a una Arquitectura en Capas.

Refactorización de Autenticación: Transición guiada del esquema de bases de datos relacionales (Microsoft Identity / Entity Framework) hacia un modelo personalizado de autenticación por Cookies basado en persistencia JSON.
