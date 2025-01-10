import React, { useState } from 'react';
import { Box, SimpleGrid, Image, Modal, ModalOverlay, ModalContent, ModalBody, useDisclosure } from '@chakra-ui/react';
import Header from './Header';  // Importar el Header

const Multimedia = () => {
  const { isOpen, onOpen, onClose } = useDisclosure(); // Control del Modal
  const [selectedImage, setSelectedImage] = useState(null);  // Imagen seleccionada para agrandar

  // Lista de imágenes
  const images = [
    '/src/assets/gallery1.jpg',
    '/src/assets/gallery2.jpg',
    '/src/assets/gallery3.jpg',
    '/src/assets/gallery4.jpg',
    '/src/assets/gallery5.jpg',
    '/src/assets/gallery6.jpg',    
    '/src/assets/gallery7.jpg',
    '/src/assets/gallery8.jpg',
    '/src/assets/gallery9.jpg',
    '/src/assets/gallery10.jpg',
    '/src/assets/gallery11.jpg',
    '/src/assets/gallery12.jpg',
    '/src/assets/gallery13.jpg',    
    '/src/assets/gallery14.jpg',
    '/src/assets/gallery15.jpg',
    '/src/assets/gallery16.jpg',
    '/src/assets/gallery17.jpg',
    '/src/assets/gallery18.jpg',
  ];

  // Función para manejar clic en la imagen
  const handleImageClick = (src) => {
    setSelectedImage(src);  // Guardar imagen seleccionada
    onOpen();  // Abrir modal
  };

  return (
    <>
      <Header />  {/* Incluir el Header */}
      <Box p={8}>
        <h1>Galería Multimedia</h1>
        <SimpleGrid columns={[1, 2, 3]} spacing={10} mt={8}>
          {images.map((src, index) => (
            <Image
              key={index}
              src={src}
              alt={`Gallery image ${index + 1}`}
              objectFit="cover"
              boxShadow="lg"
              borderRadius="md"
              cursor="pointer"
              _hover={{ transform: 'scale(1.05)', transition: '1.3s' }}
              onClick={() => handleImageClick(src)}  // Al hacer clic, agrandar imagen
            />
          ))}
        </SimpleGrid>
        
        {/* Modal para mostrar imagen seleccionada */}
        <Modal isOpen={isOpen} onClose={onClose} isCentered>
          <ModalOverlay />
          <ModalContent maxW="80%">  {/* Tamaño máximo del modal */}
            <ModalBody p={0}>
              <Image src={selectedImage} alt="Selected" width="100%" />
            </ModalBody>
          </ModalContent>
        </Modal>
      </Box>
    </>
  );
};

export default Multimedia;
