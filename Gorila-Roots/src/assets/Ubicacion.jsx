// src/assets/Ubicacion.jsx
import React from 'react';
import Header from './Header';  // Importar el Header
import { Box, Text } from '@chakra-ui/react';

const Ubicacion = () => {
  return (
    <>
      <Header />  {/* Incluir el Header */}
      <Box p={8} mt={5} textAlign="center">
        <Text fontSize="3xl" mb={4} fontWeight="bold">
          Nuestra Ubicación
        </Text>
        <Text fontSize="xl" mb={4} color="gray.600">
          Dirección: Belgrano 1783, Banfield, Club Country de Banfield
        </Text>
        <Box
          as="iframe"
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d330.39190497816046!2d-58.39090780843779!3d-34.740165182852756!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x95bcd2a09616bcad%3A0xbdb63ac27a566484!2sCountry%20Club%20infantil%20de%20Banfield!5e0!3m2!1ses-419!2sar!4v1697111145369!5m2!1ses-419!2sar"
          width="60%"  // Ajusta el ancho del mapa al 90% para centrarlo
          height="250"
          allowFullScreen=""
          loading="lazy"
          referrerPolicy="no-referrer-when-downgrade"
          borderRadius="md"
          boxShadow="lg"
          mx="auto"  // Centra el mapa horizontalmente
        />
      </Box>
    </>
  );
};

export default Ubicacion;
