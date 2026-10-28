# Informe técnico breve - AcademiaTrack

## 1. Problema

Se necesita registrar estudiantes y materias, encontrar estudiantes rápidamente, gestionar inscripciones y consultar su avance por semestre.

## 2. Objetivo

Construir un sistema de consola en C# que use varias estructuras de datos para organizar la información académica y calcular promedios según la carga horaria.

## 3. Estructuras de datos usadas

| Estructura | Para qué la usé | Archivo |
|---|---|---|
| Arreglo | Guardar un catálogo de materias y sus horas | `SistemaAcademico.cs` |
| Memoria dinámica | Crear objetos y nodos mientras funciona el sistema | Varios archivos |
| Pila | Guardar inscripciones exitosas para deshacer la más reciente (LIFO) | `PilaYCola.cs` |
| Cola | Atender solicitudes de inscripción por orden de llegada (FIFO) | `PilaYCola.cs` |
| Lista simplemente enlazada | Guardar las materias inscritas de cada semestre | `ListaMaterias.cs` |
| Lista doblemente enlazada | Conectar semestres y moverse al anterior o siguiente | `HistorialAcademico.cs` |
| Árbol AVL | Guardar y buscar alumnos por código manteniendo el árbol balanceado | `ArbolAVL.cs` |

## 4. Explicación del árbol AVL

El AVL es un árbol binario de búsqueda: los códigos menores van a la izquierda y los mayores van a la derecha. A diferencia de un árbol binario simple, el AVL revisa la **altura** de sus ramas cuando insertamos un estudiante. Si la diferencia de alturas es mayor que 1, realiza giros para volver a balancear.

- Giro a la izquierda: corrige un peso excesivo a la derecha.
- Giro a la derecha: corrige un peso excesivo a la izquierda.
- Giro doble: combina ambos cuando el desbalance está en forma de zigzag.

En este proyecto los nodos guardan un enlace `Padre`. Eso permite subir por el árbol con `while` después de insertar, sin hacer llamadas recursivas. Buscar estudiantes también usa `while`. Para listarlos en orden se utiliza una pila temporal de nodos.

La búsqueda y la inserción en un AVL balanceado tardan **O(log n)** en función de la cantidad de estudiantes.

## 5. Promedio ponderado (sin recursividad)

En una lista de materias se recorre cada nodo usando `while` y se acumulan dos valores:

- `sumaPonderada`: suma de `nota × horas` para las materias que tienen nota.
- `sumaHoras`: horas de esas mismas materias.

Al terminar, `promedio = sumaPonderada / sumaHoras`. Si no hay notas, el programa informa que el promedio todavía no se puede calcular.

Ejemplo: Programacion I, 80 y 6 horas; Calculo I, 90 y 5 horas:

`(80 × 6 + 90 × 5) / 11 = 84.55`

Este cálculo tarda **O(m)**, donde `m` es la cantidad de materias del semestre.

## 6. Qué se puede mejorar

Por ahora, los datos se pierden al cerrar el programa y se usan como máximo 30 materias en el catálogo. Una mejora futura sería guardar los registros en archivos o una base de datos. También se podría controlar que cada semestre corresponda a fechas y gestionar usuarios.

## 7. Guía de defensa (máximo 5 minutos)

- **0:00 a 0:30:** cuál es el problema académico que resuelve.
- **0:30 a 1:00:** qué registra el sistema y por qué se hizo en consola.
- **1:00 a 3:30:** mostrar un registro, búsqueda, cola de inscripciones, notas y promedio.
- **3:30 a 4:30:** señalar el AVL, la lista simple, la lista doble, la pila y la cola en el código.
- **4:30 a 5:00:** explicar que una institución podría adaptarlo para seguir el avance de estudiantes.

**Importante:** el PDF original menciona la recursividad como criterio. El programa no usa recursividad; revisar con el docente si esta adaptación está aceptada.
