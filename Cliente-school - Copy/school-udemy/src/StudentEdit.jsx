import { useParams } from "react-router-dom";
import { Header } from "./Header";
import { useEffect, useState } from "react";
import * as API from "./Services/data";

export function StudentEdit() {
    let params = useParams();

    // Inicializar como objeto vacío
    const [student, setStudent] = useState({
        dni: '',
        nombre: '',
        direccion: '',
        edad: '',
        email: ''
    });

    useEffect(() => {
        API.getStudentDetails(params.studentId).then(setStudent);
    }, [params.studentId]); // Añadido params.studentId como dependencia

    function handleSubmit(e) {
        e.preventDefault();
        API.editStudent(student).then(result => {
            if (result === "true" || result === true) { // Considerar tanto string como boolean
                alert("Alumno modificado");
            } else {
                alert("Error al modificar alumno");
            }
        });
    }

    return (
        <>
            <Header />
            <form id='formulario' onSubmit={handleSubmit}>
                DNI<input 
                    type="text" 
                    id='dni' 
                    required 
                    disabled 
                    value={student.dni || ''} 
                /><br></br>
                Nombre<input 
                    type="text" 
                    id='nombre' 
                    required  
                    value={student.nombre || ''} 
                    onChange={event => setStudent({...student, nombre: event.target.value})} 
                /><br></br>
                Direccion<input 
                    type="text" 
                    id='direccion' 
                    required  
                    value={student.direccion || ''} 
                    onChange={event => setStudent({...student, direccion: event.target.value})} 
                /><br></br>
                Edad<input 
                    type="number" 
                    id='edad' 
                    required  
                    value={student.edad || ''} 
                    onChange={event => setStudent({...student, edad: event.target.value})} 
                /><br></br>
                email<input 
                    type="text" 
                    id='email' 
                    required  
                    value={student.email || ''} 
                    onChange={event => setStudent({...student, email: event.target.value})} 
                /><br></br>
                <input type='submit' id='editar' value='Editar' />
            </form>
        </>
    );
}
