import { json, UNSAFE_FetchersContext } from "react-router-dom";

const URL ='https://localhost:7034/api/'

export function login(usuario, pass){
    let datos = {usuario:usuario,pass:pass};

    return fetch(URL+'autenticacion',{
        method:'POST',
        body: JSON.stringify(datos),
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(data => data.text())
}

export function getStudents(usuario){
    
    return fetch(URL +'AlumnosProfesor?usuario='+usuario,{
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(data=>data.json());
}
export function createStudent(student){
    let data = {dni:student.dni, nombre:student.nombre, direccion: student.direccion, edad:student.edad, email:student.email};

    return fetch(URL+'Alumno?idAsig='+student.asignatura,{
        method:'POST',
        body: JSON.stringify(data),
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(data =>data.text());
}

export function deleteStudent(id){
    return fetch(URL+'alumno?id='+id,{
        method:'DELETE',
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(data =>data.text());

}

export function getStudentDetails(id) {
    return fetch(URL + 'Alumno?id=' + id, {
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(data => data.json()); // Cambiado JSON() por json()
}

export function editStudent(student){

    return fetch(URL + 'Alumno',{

        method:'PUT',
        body: JSON.stringify(student),
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(data => data.text());
}

export function getCalificaciones(id){

    return fetch(URL+'calificacion?idMatricula='+id,{
        headers:{
            'Content-Type':'application/json',
        }
    })
    .then(data => data.json());
}

export function createCalificacion(calificacion,id){

    let data ={descripcion:calificacion.descripcion,nota:calificacion.nota,porcentaje:calificacion.porcentaje,matriculaId:id};

    return fetch(URL+'calificacion',{
        method:'POST',
        body: JSON.stringify(data),
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(data => data.text());
}

export function deleteCalificacion(id){

    return fetch(URL+'calificacion?id='+id,{
        method:'DELETE',
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(data=>data.text())
}