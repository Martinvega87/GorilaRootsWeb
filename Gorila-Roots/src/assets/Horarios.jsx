// src/assets/Horarios.jsx
import React from 'react';
import Header from './Header';  // Importar el Header
import { Box, Table, Thead, Tbody, Tr, Th, Td, Text } from '@chakra-ui/react';

const Horarios = () => {
  return (
    <>
      <Header />  {/* Incluir el Header */}
      <Box p={8} mt="100px" textAlign="center">  {/* Agregar margen superior de 50px */}
        <Text fontSize="3xl" mb={8} fontWeight="bold">
          Horarios de Entrenamiento
        </Text>
        <Box maxW="600px" mx="auto">  {/* Centrar la tabla */}
          <Table variant="striped" colorScheme="gray" size="lg">
            <Thead>
              <Tr>
                <Th>Día</Th>
                <Th>Horario</Th>
              </Tr>
            </Thead>
            <Tbody>
              <Tr>
                <Td>Lunes</Td>
                <Td>20:00 - 21:30</Td>
              </Tr>
              <Tr>
                <Td>Miercoles</Td>
                <Td>20:00 - 21:30</Td>
              </Tr>
              <Tr>
                <Td>Viernes</Td>
                <Td>19:30 - 21:00</Td>
              </Tr>
            </Tbody>
          </Table>
        </Box>
      </Box>
    </>
  );
};

export default Horarios;
