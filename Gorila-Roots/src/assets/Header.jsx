import { Box, Flex, Link, Image } from '@chakra-ui/react';
import { Link as RouterLink } from 'react-router-dom';  // Importamos Link de React Router
import gorilaLogo from './gorila-roots-logo.png';  // Asegúrate de que la ruta sea correcta
import { FaInstagram } from 'react-icons/fa';  // Importamos el ícono de Instagram

const Header = () => {
  const scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  return (
    <Box bg="gray.200" py={4} px={8} width="100%" position="fixed" top="0" zIndex="1000">
      <Flex justify="space-between" align="center">
        {/* Enlace a Home */}
        <Link as={RouterLink} to="/" onClick={scrollToTop} fontSize="xl" fontWeight="bold" cursor="pointer" ml={4}>
          Home
        </Link>

        {/* Logo Gorila Roots */}
        <Image src={gorilaLogo} alt="Gorila Roots Logo" boxSize="80px" />

        {/* Enlace a Instagram */}
        <Link href="https://www.instagram.com/gorilarootsbjj/" isExternal>
          <FaInstagram size={28} color="gray" _hover={{ color: "black" }} />
        </Link>
      </Flex>
    </Box>
  );
};

export default Header;
