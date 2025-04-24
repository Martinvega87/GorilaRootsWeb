import { useParams } from "react-router-dom";
import { Header } from "./Header";
import { useEffect, useState } from "react";
import * as API from "./Services/data";
import { Center,Box, Heading, FormControl, FormLabel, Input, Select } from "@chakra-ui/react";

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
            <Center>
                <Box m='40px' boxShadow='xl' borderRadius='md' width='40%' id='caja'>
                    <Box textAlign="center">
                        <Heading>Editar Estudiante</Heading>
                    </Box>
                    <Box p='20px'>
                <form id='formulario' onSubmit={handleSubmit}>
                <FormControl mt='3px'>
                    <FormLabel>DNI</FormLabel>
                    <Input type="text" 
                    id='dni' 
                    required 
                    disabled 
                    value={student.dni || ''} />
                </FormControl>
                <FormControl mt='3px'>
                    <FormLabel>Nombre</FormLabel>
                    <Input type="text" 
                    id='nombre' 
                    required  
                    value={student.nombre || ''} 
                    onChange={event => setStudent({...student, nombre: event.target.value})} />
                </FormControl>
                <FormControl mt='3px'>
                    <FormLabel>Direccion</FormLabel>
                    <Input  type="text" 
                    id='direccion' 
                    required  
                    value={student.direccion || ''} 
                    onChange={event => setStudent({...student, direccion: event.target.value})} />
                </FormControl>
                <FormControl mt='3px'>
                    <FormLabel>Edad</FormLabel>
                    <Input  type="number" 
                    id='edad' 
                    required  
                    value={student.edad || ''} 
                    onChange={event => setStudent({...student, edad: event.target.value})} />
                </FormControl>
                <FormControl mt='3px'>
                    <FormLabel>email</FormLabel>
                    <Input  type="text" 
                    id='email' 
                    required  
                    value={student.email || ''} 
                    onChange={event => setStudent({...student, email: event.target.value})} />
                </FormControl>
                  <Input type="submit" mt='20px' id='editar' borderColor='teal' value='Editar Alumno'/>
                </form>
                </Box>
            </Box>
                
            </Center>
        </>
    );
}
