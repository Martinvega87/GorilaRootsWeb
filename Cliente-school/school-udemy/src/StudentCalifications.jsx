import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import * as API from "./Services/data"
import { Header } from "./Header";
import { Box, Table, TableContainer, Tbody, Th, Thead,Tr,Td, Input, Button, Center, Badge } from "@chakra-ui/react";
import { FaCheck,FaTrash } from "react-icons/fa";
import Swal from 'sweetalert2'



export function StudentCalifications(){

    let params = useParams();
    const matriculaId = params.matriculaId;

    const [calificaciones,setCalificaciones] = useState([]);

    const [calificacion, setCalificacion] = useState([]);

    useEffect(()=>{
       API.getCalificaciones(params.matriculaId).then(setCalificaciones);
    });

    let total=0;

    calificaciones?.map(calificacion => (
        total=total + calificacion.nota * (calificacion.porcentaje/100)
    ))

    function createCalificacion(){

       


        let descrField = document.getElementById("descripcion");
        let notaField = document.getElementById("nota");
        let porcentField = document.getElementById("porcentaje");

        let valid = descrField.value.trim() !=="" && notaField.value.trim() !== "" && porcentField.value.trim() !==""

        if(valid){
            API.createCalificacion(calificacion,matriculaId).then(result=>{
                if(result){
                    Swal.fire({
                        title: "Realizado!",
                        text: "Calificacion añadida con exito",
                        icon: "success"
                      });                    
                      document.getElementById("descripcion").value="";
                    document.getElementById("nota").value="";
                    document.getElementById("porcentaje").value="";
                }
                else{
                    Swal.fire({
                        title: "ERROR!",
                        text: "No se ha añadido la calificacion",
                        icon: "error"
                      }); }
            })
        }
    }

    function deleteCalificacion(id){
        API.deleteCalificacion(id).then(result=>{
            if(result){
                Swal.fire({
                    title: "Realizado!",
                    text: "Calificacion eliminada con exito",
                    icon: "success"
                  });}
            else{
                Swal.fire({
                    title: "ERROR!",
                    text: "No se ha eliminado la calificacion",
                    icon: "error"
                  });              }
        })
    }

 return(
    <>
    <Header/>
    <Box m='100px'>
        <TableContainer>
            <Table size='md'>
                <Thead>
                    <Tr>
                        <Th>Descripcion</Th>
                        <Th>Nota</Th>
                        <Th>Poderacion</Th>
                        <Th></Th>
                    </Tr>
                </Thead>
                <Tbody>
                {
                    calificaciones?.map(calificacion =>
                    (
                        <Tr>
                            <Td>{calificacion.descripcion}</Td>
                            <Td>{calificacion.nota}</Td>
                            <Td>{calificacion.porcentaje}%</Td>
                            <Td><FaTrash cursor='pointer' onClick={()=> deleteCalificacion(calificacion.id)}/></Td>

                        </Tr>
                    ))  
                    
                }
                <Tr>
                    <Td><Input type= 'text' id='descripcion' placeholder='descripcion' onChange={event =>setCalificacion({...calificacion,descripcion:event.target.value})}></Input></Td>
                    <Td><Input type= 'number' id='nota' placeholder='nota'onChange={event =>setCalificacion({...calificacion,nota:event.target.value})}></Input></Td>
                    <Td><Input type= 'number' id='porcentaje' placeholder='ponderacion'onChange={event =>setCalificacion({...calificacion,porcentaje:event.target.value})}></Input></Td>
                    <Td><FaCheck cursor='pointer' id='nueva' onClick={()=>createCalificacion()}/></Td>
                </Tr>
            </Tbody>
            </Table>
        </TableContainer>
        <Center>
            <Box mt='20px' fontSize='25px'>Nota Total: 
                <Badge ml='20px' fontSize='30px' variant='outline' colorScheme='green'>
                    {total}
                </Badge>
            </Box>
        </Center>
    </Box>
    </>

 )

}