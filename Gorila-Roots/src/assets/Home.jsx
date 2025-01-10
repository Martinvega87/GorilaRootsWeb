import { Box, Grid, Image, Text, Link } from '@chakra-ui/react';
import Header from './Header'; // Importamos el Header
import Horarios from './Horarios'; // Importamos el componente Horarios
import Ubicacion from './Ubicacion'; // Importamos el componente Ubicacion
import Multimedia from './Multimedia'; // Importamos el componente Multimedia

function Home() {
  return (
    <>
      <Header />

      {/* Sección de imágenes con texto centrado */}
      <Box p={8} bg="gray.100" minH="100vh" pt="120px"> {/* pt="120px" para evitar solapado */}
        <Grid templateColumns="repeat(3, 1fr)" gap={6}>
          
          {/* Primera imagen con "Horarios" */}
          <Link href="#horarios"> {/* Enlace al ancla de Horarios */}
            <Box
              overflow="hidden"
              position="relative"
              height="500px"
              width="100%"
              _hover={{ img: { transform: 'scale(1.2)' } }}
              transition="all 0.5s ease"
            >
              <Image
                src="/src/assets/bjj1eraImagenHome.jpg"
                alt="Primera Imagen"
                objectFit="cover"
                width="100%"
                height="100%"
                transition="transform 0.5s ease"
              />
              <Text
                position="absolute"
                top="50%"
                left="50%"
                transform="translate(-50%, -50%)"
                color="white"
                fontWeight="bold"
                fontSize="2xl"
              >
                Horarios
              </Text>
            </Box>
          </Link>

          {/* Segunda imagen con "Ubicación" */}
          <Link href="#ubicacion"> {/* Enlace al ancla de Ubicación */}
            <Box
              overflow="hidden"
              position="relative"
              height="500px"
              width="100%"
              _hover={{ img: { transform: 'scale(1.2)' } }}
              transition="all 0.5s ease"
            >
              <Image
                src="/src/assets/bjj2daImagenHome.jpg"
                alt="Segunda Imagen"
                objectFit="cover"
                width="100%"
                height="100%"
                transition="transform 0.5s ease"
              />
              <Text
                position="absolute"
                top="50%"
                left="50%"
                transform="translate(-50%, -50%)"
                color="white"
                fontWeight="bold"
                fontSize="2xl"
              >
                Ubicación
              </Text>
            </Box>
          </Link>

          {/* Tercera imagen con "Multimedia" */}
          <Link href="#multimedia"> {/* Enlace al ancla de Multimedia */}
            <Box
              overflow="hidden"
              position="relative"
              height="500px"
              width="100%"
              _hover={{ img: { transform: 'scale(1.2)' } }}
              transition="all 0.5s ease"
            >
              <Image
                src="/src/assets/bjj3eraImagenHome.jpg"
                alt="Tercera Imagen"
                objectFit="cover"
                width="100%"
                height="100%"
                transition="transform 0.5s ease"
              />
              <Text
                position="absolute"
                top="50%"
                left="50%"
                transform="translate(-50%, -50%)"
                color="white"
                fontWeight="bold"
                fontSize="2xl"
              >
                Multimedia
              </Text>
            </Box>
          </Link>
        </Grid>
      </Box>

      {/* Sección de Horarios */}
      <Box id="horarios" py={12} bg="gray.200">
        <Horarios />
      </Box>

      {/* Sección de Ubicación */}
      <Box id="ubicacion" py={12} bg="gray.300">
        <Ubicacion />
      </Box>

      {/* Sección de Multimedia */}
      <Box id="multimedia" py={12} bg="gray.400">
        <Multimedia />
      </Box>
    </>
  );
}

export default Home;
