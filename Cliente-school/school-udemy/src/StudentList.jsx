import { useState, useEffect } from "react";
import * as API from "./Services/data";
import { Link } from "react-router-dom";
import { Table, TableContainer, Tbody, Thead, Box, Th, Tr, Td } from "@chakra-ui/react";
import { FaEdit, FaStickyNote, FaTrashAlt } from "react-icons/fa";

export function StudentList() {
    let usuario = sessionStorage.getItem("usuario");

    const [students, setStudents] = useState([]);

    // useEffect solo se ejecutará cuando el componente se monte
    useEffect(() => {
        API.getStudents(usuario).then(setStudents);
    }, [usuario]);

    function deleteStudent(id) {
        API.deleteStudent(id).then(result => {
            if (result) {
                alert("Estudiante eliminado con éxito");
                // Actualizar la lista de estudiantes después de eliminar uno
                setStudents(prevStudents => prevStudents.filter(student => student.id !== id));
            } else {
                alert("Error al intentar eliminar un alumno");
            }
        });
    }

    return (
        <>
            <Box m="50px">
                <TableContainer>
                    <Table size="md" variant="striped" colorScheme="gray">
                        <Thead>
                            <Tr>
                                <Th>ID</Th>
                                <Th>DNI</Th>
                                <Th>Nombre</Th>
                                <Th>Dirección</Th>
                                <Th>Edad</Th>
                                <Th>Email</Th>
                                <Th>Asignatura</Th>
                                <Th></Th>
                                <Th></Th>
                                <Th></Th>
                            </Tr>
                        </Thead>
                        <Tbody>
                            {students?.map(student => (
                                <Tr key={student.id}>
                                    <Td>{student.id}</Td>
                                    <Td>{student.dni}</Td>
                                    <Td>{student.nombre}</Td>
                                    <Td>{student.direccion}</Td>
                                    <Td>{student.edad}</Td>
                                    <Td>{student.email}</Td>
                                    <Td>{student.asignatura}</Td>
                                    <Td>
                                        <Link to={'/student/' + student.id}>
                                            <FaEdit />
                                        </Link>
                                    </Td>
                                    <Td>
                                        <Link to={'/student/califications/' + student.matriculaId}>
                                            <FaStickyNote />
                                        </Link>
                                    </Td>
                                    <Td cursor={"pointer"}>
                                        <FaTrashAlt onClick={() => deleteStudent(student.id)} />
                                    </Td>
                                </Tr>
                            ))}
                        </Tbody>
                    </Table>
                </TableContainer>
            </Box>
        </>
    );
}
