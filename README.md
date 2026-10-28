<div align="center">

# 🎓 AcademiaTrack

### Plataforma de Registro y Seguimiento Académico Estudiantil

**Proyecto Final Integrador 5 — Estructura de Datos**  
**C# · .NET 10 · Aplicación de consola**

*Registro, búsqueda, inscripciones y seguimiento académico en un mismo sistema.*

</div>

---

## Índice

1. [Presentación del proyecto](#1-presentación-del-proyecto)
2. [Problemática](#2-problemática)
3. [Objetivos](#3-objetivos)
4. [Funcionalidades](#4-funcionalidades)
5. [Estructuras de datos](#5-estructuras-de-datos)
6. [Funcionamiento de los algoritmos](#6-funcionamiento-de-los-algoritmos)
7. [Ejemplo de uso y pruebas](#7-ejemplo-de-uso-y-pruebas)
8. [Organización del código](#8-organización-del-código)
9. [ODS 4 y valor emprendedor](#9-ods-4-y-valor-emprendedor)
10. [Conclusiones](#10-conclusiones)

## 1. Presentación del proyecto

**AcademiaTrack** es un sistema desarrollado en C# para organizar los registros académicos de los estudiantes. Permite registrar alumnos, gestionar materias e inscripciones, consultar calificaciones y revisar el historial por semestres.

El proyecto corresponde a la asignatura **Estructura de Datos**, de quinto semestre, y aplica los conocimientos de arreglos, memoria dinámica, pila, cola, listas enlazadas y árbol AVL a una situación relacionada con la gestión educativa.

La interfaz está organizada como un menú de consola con colores para diferenciar títulos, secciones y resultados.

## 2. Problemática

Las instituciones educativas manejan información de estudiantes, materias y calificaciones. Cuando los registros no están organizados adecuadamente pueden surgir dificultades para:

- **Encontrar estudiantes rápidamente**, especialmente al buscar entre numerosos registros.
- **Hacer seguimiento académico**, consultando materias y calificaciones de diferentes semestres.
- **Gestionar inscripciones ordenadamente**, atendiendo las solicitudes en el orden en que fueron recibidas.

Estos procesos son relevantes para estudiantes, docentes y personal administrativo. AcademiaTrack integra las operaciones principales en un sistema de seguimiento académico.

## 3. Objetivos

**Objetivo general:** Desarrollar un sistema que gestione el registro de estudiantes y materias, permita búsquedas eficientes mediante un árbol AVL y facilite el seguimiento del progreso académico.

**Objetivos específicos:**

- Registrar estudiantes con código, nombre y carrera.
- Organizar y consultar el catálogo de materias y su carga horaria.
- Implementar búsquedas por código mediante un árbol AVL balanceado.
- Gestionar solicitudes de inscripción mediante una cola y permitir deshacer la última matrícula mediante una pila.
- Utilizar listas enlazadas para las materias inscritas y el historial académico.
- Registrar calificaciones y calcular el promedio ponderado de cada semestre.

## 4. Funcionalidades

| Módulo | Opciones | Función |
|:--|:--|:--|
| Estudiantes | 1, 2, 3 | Registrar, buscar por código y mostrar estudiantes. |
| Materias | 4, 5 | Consultar el catálogo y registrar nuevas materias. |
| Inscripciones | 6, 7, 8 | Solicitar inscripción, atender la cola y deshacer la última inscripción. |
| Calificaciones | 9, 10 | Registrar o actualizar una nota y consultar el promedio ponderado. |
| Historial | 11, 12 | Agregar un semestre y recorrer el historial académico. |
| Sistema | 0 | Salir del programa. |

**Colores del menú:** cian para encabezados, amarillo para secciones y avisos, verde para confirmaciones y rojo para mensajes de error.

## 5. Estructuras de datos

| Estructura | Uso dentro de AcademiaTrack | Criterio |
|:--|:--|:--:|
| **Arreglo** | Catálogo de materias con sus códigos, nombres y horas. | CV1 |
| **Memoria dinámica** | Creación de objetos y nodos cuando se registran estudiantes, materias o inscripciones. | CV1 |
| **Pila (LIFO)** | Historial de inscripciones procesadas para deshacer la última. | CV1 |
| **Cola (FIFO)** | Solicitudes de inscripción procesadas por orden de llegada. | CV1 |
| **Lista simplemente enlazada** | Materias que un estudiante tiene inscritas en un semestre. | CV2 |
| **Lista doblemente enlazada** | Semestres y navegación hacia el anterior o el siguiente. | CV2 |
| **Árbol AVL** | Registro balanceado de estudiantes y búsqueda por código. | CV3 |

### 5.1 Árbol AVL

El **árbol AVL** es un árbol binario de búsqueda que controla la altura de sus ramas para mantenerse balanceado. Cada nodo guarda un estudiante, referencias a sus nodos izquierdo, derecho y padre, y la altura del nodo.

Los estudiantes se ordenan por su **código**. Cuando se registra uno nuevo, el algoritmo busca iterativamente su posición y actualiza las alturas de los nodos. Si encuentra un desequilibrio, realiza rotaciones para recuperar el balance. El código contempla rotación a la izquierda, a la derecha y los casos dobles izquierda-derecha y derecha-izquierda.

Para buscar un estudiante, el algoritmo compara el código con el del nodo actual: si es menor, continúa por la izquierda; si es mayor, por la derecha. Esto permite búsquedas eficientes, normalmente **O(log n)** en un árbol AVL.

También se utiliza un recorrido **en orden** para mostrar los estudiantes ordenados por código. Se implementa con una pila auxiliar, sin recursividad.

### 5.2 Lista simplemente enlazada

Cada nodo de materias contiene la materia, su nota (si ya fue registrada) y una referencia al siguiente nodo. La lista permite agregar materias inscritas, buscar por código, registrar notas, quitar una inscripción y recorrer todas las materias de un semestre.

La lista crece mediante nodos enlazados. La búsqueda de una materia puede requerir recorrer varios nodos, por lo que su complejidad es **O(n)**.

### 5.3 Lista doblemente enlazada

El historial académico se compone de nodos de semestre. Cada semestre guarda su número, una lista de materias y referencias al semestre **anterior** y al **siguiente**. El sistema permite crear nuevos semestres y navegar entre los periodos académicos con las opciones correspondientes.

### 5.4 Pila y cola

- **Pila — LIFO:** al procesar una inscripción, se registra en la pila. Cuando el usuario elige deshacer, se quita primero la última inscripción procesada. Insertar y sacar de la cima requiere **O(1)**.
- **Cola — FIFO:** una solicitud se agrega al final y se atiende desde el inicio. De esta forma se respeta el orden de llegada. Encolar y sacar requieren **O(1)**, porque se mantienen referencias al primer y al último nodo.

### 5.5 Arreglo y memoria dinámica

El catálogo se almacena en un arreglo de hasta **30 materias**. Cada posición conserva una materia con su código y carga horaria. Para localizar una materia se recorre el catálogo; la búsqueda es **O(n)** respecto al número de materias almacenadas.

Los estudiantes, nodos del árbol, nodos de listas y solicitudes se crean como objetos de C# cuando son necesarios.

## 6. Funcionamiento de los algoritmos

### 6.1 Registro y búsqueda de estudiantes

1. Se ingresa código, nombre y carrera.
2. El sistema verifica que el código no esté registrado.
3. Se crea el estudiante y su primer semestre.
4. El estudiante se inserta en el árbol AVL, que ajusta el balance cuando corresponde.
5. Para encontrarlo, se ingresa su código y se recorre el árbol de búsqueda.

### 6.2 Inscripción de materias

1. El usuario elige un estudiante y una materia del catálogo.
2. La solicitud entra en la cola de inscripciones.
3. Al procesar la cola, la materia se añade a la lista del semestre correspondiente.
4. La inscripción procesada queda registrada en una pila para permitir deshacerla.

### 6.3 Promedio ponderado **sin recursividad**

El promedio tiene en cuenta la **nota** y las **horas** de cada materia que ya tenga calificación registrada:

**Promedio ponderado = Σ(nota × horas) ÷ Σ(horas de materias calificadas)**

Ejemplo:

| Materia | Nota | Horas | Nota × horas |
|:--|--:|--:|--:|
| Programación I | 80 | 6 | 480 |
| Cálculo I | 90 | 5 | 450 |
| **Total** | | **11** | **930** |

**Resultado: 930 ÷ 11 = 84,55.**

El método `CalcularPromedio()` de `ListaMaterias.cs` recorre los nodos con un ciclo `while`. Utiliza las variables `sumaPonderada`, `sumaHoras` y `actual`, y divide las sumas al finalizar. Su complejidad temporal es **O(n)**, donde `n` es la cantidad de materias inscritas.

> **Criterio de implementación:** el enunciado del proyecto menciona un promedio recursivo; esta versión utiliza el cálculo iterativo con `while`, según la adaptación indicada para el desarrollo del sistema.

## 7. Ejemplo de uso y pruebas

Un recorrido de demostración del sistema puede realizarse con estos datos:

| Paso | Acción | Datos y resultado esperado |
|:--|:--|:--|
| 1 | Registrar estudiante | Código **2001**, nombre **Ana Flores**, carrera **Sistemas**. |
| 2 | Buscar estudiante | Consultar el código **2001** y mostrar su registro. |
| 3 | Consultar catálogo | Ver **Programación I (101, 6 h)** y **Cálculo I (102, 5 h)**. |
| 4 | Solicitar inscripciones | Crear una solicitud para cada materia. |
| 5 | Procesar la cola | Atender ambas solicitudes y completar las inscripciones. |
| 6 | Registrar notas | Programación I: **80**; Cálculo I: **90**. |
| 7 | Consultar promedio | Obtener **84,55** (mostrado como `84.55` según el formato de consola). |
| 8 | Consultar historial | Crear el semestre 2 y regresar al semestre 1 con la navegación. |

También se consideran casos como intentar registrar códigos repetidos, buscar estudiantes inexistentes, inscribir dos veces la misma materia y consultar el promedio antes de registrar notas.

## 8. Organización del código

| Archivo | Responsabilidad |
|:--|:--|
| [`Program.cs`](Program.cs) | Punto de inicio de la aplicación. |
| [`SistemaAcademico.cs`](SistemaAcademico.cs) | Menú y coordinación de las operaciones. |
| [`Modelos.cs`](Modelos.cs) | Clases de estudiante, materia y solicitudes. |
| [`ArbolAVL.cs`](ArbolAVL.cs) | Inserción, balanceo, búsqueda y recorrido del árbol. |
| [`ListaMaterias.cs`](ListaMaterias.cs) | Materias inscritas, notas y promedio. |
| [`HistorialAcademico.cs`](HistorialAcademico.cs) | Semestres conectados mediante lista doble. |
| [`PilaYCola.cs`](PilaYCola.cs) | Estructuras para inscripciones y deshacer operaciones. |
| [`AcademiaTrack.csproj`](AcademiaTrack.csproj) | Configuración del proyecto C#/.NET 10. |

## 9. ODS 4 y valor emprendedor

AcademiaTrack se relaciona con el **Objetivo de Desarrollo Sostenible 4: Educación de Calidad**, porque busca facilitar la organización y el seguimiento de la información académica.

Desde el enfoque emprendedor, sus funciones pueden servir como punto de partida para soluciones de gestión destinadas a instituciones educativas: centralizar el registro, organizar las solicitudes de inscripción y facilitar la consulta del avance de cada estudiante.

## 10. Conclusiones

AcademiaTrack integra estructuras de datos en operaciones de gestión académica concretas. El árbol AVL permite organizar y buscar estudiantes; las listas enlazadas representan materias y semestres; la cola y la pila ayudan a controlar las inscripciones; y el promedio ponderado relaciona notas con carga horaria.

El desarrollo permite demostrar cómo se aplican estructuras de datos en un sistema de consola con una interfaz organizada y un flujo de trabajo académico.

---

**Proyecto Final Integrador 5 — Estructura de Datos**  
**Docente:** Msc. Lic. Andrés Albino  
**Universidad Privada Domingo Savio (UPDS) — La Paz**  
**Gestión 2026**
En el siguiente video se presenta la problemática, los objetivos y el funcionamiento de AcademiaTrack, incluyendo una demostración de las principales funcionalidades desarrolladas en C#.

▶️ [Ver presentación de AcademiaTrack en YouTube](https://youtu.be/n2eG9Y9tKio)
