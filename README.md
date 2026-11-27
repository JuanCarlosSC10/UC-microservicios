# UC-microservicios
Plataforma academica con arquitectura de microservicios en .NET 10, Sql Server y un API Gateway.

Pasos para levantar el proyecto

## Levantar con Docker 

- Docker Desktop abierto.

- clonar el proyecto 

- en la terminal ejecutar docker compose up --build

Tarda un poco al descarga y construye las imagenes, luego pegar en el navegador el siguiente enlace

**   http://localhost:5120/swagger  **

Arriba a la derecha elegir **Estudiantes API** o **Cursos API**.

Las bases de datos y tablas se crean solas al iniciar.


## Opcional Si la primera falla - sin Docker para las APIs

Usar esta opción si el primero falla.

Requisitos: .NET 10 SDK y Docker Desktop abierto.

**1. Levantar solo la base de datos**

docker compose up -d --wait sqlserver

**2. Levantar los 3 servicios en una terminal para cada uno

- dotnet run --project src/Estudiantes.API
- dotnet run --project src/Cursos.API
- dotnet run --project src/ApiGateway


Abrir **    http://localhost:5120/swagger  **

** Pruebas  automatizadas 

dotnet test
