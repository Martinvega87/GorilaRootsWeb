import { useState, useEffect } from "react";
import * as API from "./Services/data"
import { Link } from "react-router-dom";

export function StudentList(){

    let usuario = sessionStorage.getItem("usuario");

    const [students, setStudent]= useState([]);

    useEffect(()=>{
        API.getStudents(usuario).then(setStudent)
    })

function deleteStudent(id){
    
    API.deleteStudent(id).then(result =>{
        if(result){
            alert("estudiante eliminado con exito");
        }
        else
        {
            alert("error al intentar eliminar un alumno");
        }
        
    })
}

    return(
        <>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>dni</th>
                        <th>nombre</th>
                        <th>Direccion</th>
                        <th>Edad</th>
                        <th>email</th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        students?.map(student =>(
                            <tr key={student.id}>
                                <td>{student.id}</td>
                                <td>{student.dni}</td>
                                <td>{student.nombre}</td>
                                <td>{student.direccion}</td>
                                <td>{student.edad}</td>
                                <td>{student.email}</td>
                                <td>{student.asignatura}</td>
                                <td><Link to= {'/student/'+student.id}>Editar</Link> </td>
                                <td><Link to= {'/student/califications/'+student.matriculaId}>Calificar</Link></td>
                                <td onClick={() => deleteStudent(student.id)}>Eliminar</td>
                            </tr>
                        ))
                    }
                </tbody>
            </table>
        </>
    )
}