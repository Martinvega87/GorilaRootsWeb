import { Header } from "./Header"
import { useState } from "react"
import * as API from './Services/data'; 

export function StudentNew(){

    const[student, setStudent] = useState({dni:'',nombre:'',direccion:'',edad:'',email:'',asignatura:'1'});

    function handleSubmit(e){
        e.preventDefault();
        API.createStudent(student).then(result =>{
            if(result){
                alert("alumno insertado");
                document.getElementById("formulario").reset();
            }
            else{
                alert("error");
            }
        })
    }

    return(
        <>
            <Header/>
            <p>nuevo estudiante</p>
            <form id='formulario' onSubmit={handleSubmit}>
                DNI<input type='text' id='dni' required onChange={event=> setStudent({...student,dni:event.target.value})}/> <br></br>
                Nombre<input type='text' id='nombre' required onChange={event=> setStudent({...student,nombre:event.target.value})}/> <br></br>
                Direccion<input type='text' id='direccion' required onChange={event=> setStudent({...student,direccion:event.target.value})}/> <br></br>
                Edad<input type='number' id='edad' required onChange={event=> setStudent({...student,edad:event.target.value})}/> <br></br>
                email<input type='text' id='email' required onChange={event=> setStudent({...student,email:event.target.value})}/> <br></br>
                asignatura<select id='asignatura'  onChange={event=> setStudent({...student,asignatura:event.target.value})}>
                            <option value='1'>Matematica</option>
                            <option value='2'>Informatica</option>
                            <option value='3'>Ingles</option>
                            <option value='4'>Lengua</option>
                          </select> <br></br>
                          <input type='submit' id='editar' value='Crear Alumno'/>
            </form>
        </>
    )
}