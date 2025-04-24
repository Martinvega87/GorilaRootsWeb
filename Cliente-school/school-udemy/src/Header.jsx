import { Box, Flex, HStack,Image } from "@chakra-ui/react";
import { Link, Navigate } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import logo from './assets/escuela.png';



export function Header(){

    const navigate = useNavigate();
    function cerrarSesion(){
        sessionStorage.removeItem("usuario");
        navigate("/");
    }
    return(
        <>
            <Flex w='100%' h='70px' p='6px' align='center' justify='space-between'   bgGradient="linear(to-r, #85C890, #69B99F, #4DB0A9, #34A7B3, #20B4BE)">
                <HStack as='nav' spacing='10px'>
                    <Image src={logo} h='50px' ml='6px'/>
                    <Link to={'/dashboard'}><Box _hover={{color:'gray'}}>Listado</Box></Link>
                    <Link to={'/student'}><Box _hover={{color:'gray'}}>Nuevo</Box></Link>
                </HStack>
                <HStack>
                   <Box mr='20px' cursor='pointer' _hover={{color:'gray'}} onClick={()=>cerrarSesion()}>Cerrar Sesion</Box>
                </HStack>
            </Flex>
        </>
    )
}