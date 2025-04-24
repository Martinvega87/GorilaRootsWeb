import { Header } from "./Header"
import { useState } from "react"
import * as API from './Services/data'; 
import { Center,Box, Heading, FormControl, FormLabel, Input, Select } from "@chakra-ui/react";

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
            <Center>
            <Box m='40px' boxShadow='xl' borderRadius='md' width='40%' id='caja'>
                <Box textAlign="center">
                    <Heading>Nuevo Estudiante</Heading>
                </Box>
                    <Box p='20px'>
                    <form id='formulario' onSubmit={handleSubmit}>
                    <FormControl mt='3px'>
                        <FormLabel>DNI</FormLabel>
                        <Input type='text' id='dni' required onChange={event=> setStudent({...student,dni:event.target.value})}/>
                    </FormControl>
                    <FormControl mt='3px'>
                        <FormLabel>Nombre</FormLabel>
                        <Input type='text' id='Nombre' required onChange={event=> setStudent({...student,nombre:event.target.value})}/>
                    </FormControl>
                    <FormControl mt='3px'>
                        <FormLabel>Direccion</FormLabel>
                        <Input type='text' id='Direccion' required onChange={event=> setStudent({...student,direccion:event.target.value})}/>
                    </FormControl>
                    <FormControl mt='3px'>
                        <FormLabel>Edad</FormLabel>
                        <Input type='number' id='Edad' required onChange={event=> setStudent({...student,Edad:event.target.value})}/>
                    </FormControl>
                    <FormControl mt='3px'>
                        <FormLabel>email</FormLabel>
                        <Input type='text' id='email' required onChange={event=> setStudent({...student,email:event.target.value})}/>
                    </FormControl>
                    <FormControl mt='3px'>
                        <FormLabel>asignatura</FormLabel>
                        <Select  id='asignatura' required onChange={event=> setStudent({...student,asignatura:event.target.value})}>
                            <option value='1'>Matematica</option>
                            <option value='2'>Informatica</option>
                            <option value='3'>Ingles</option>
                            <option value='4'>Lengua</option>
                            <option value='5'>Fisica</option>
                        </Select>
                    </FormControl>
                    <FormControl>
                        <Input type="submit" mt='20px' id='editar' borderColor='teal' value='Nuevo Alumno'/>   
                    </FormControl>       
                    </form>
                </Box>                
            </Box>
            </Center> 
        </>
    )
}