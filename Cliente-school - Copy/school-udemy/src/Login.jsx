import { useState } from 'react';
import * as API from './Services/data';
import imagen from './assets/Logo.png';
import { useNavigate } from 'react-router-dom';

export function Login() {
    const [teacher, setTeacher] = useState({ usuario: '', password: '' });

    const navigate = useNavigate();

    async function handleSubmit(e) {
        e.preventDefault();
        const response = await API.login(teacher.usuario, teacher.password);
        if (response.length !== 0) {
            sessionStorage.setItem("usuario",response)
           navigate('/dashboard');
        } else {
            alert('ERROR');
        }
    }

    return (
        <>
            <img src={imagen} alt="Descripción de la imagen" width="100" height="100" />
            <h3>Iniciar Sesión</h3>
            <form id='formulario' onSubmit={handleSubmit}>
                Usuario <input type='text' id='usuario' onChange={event => setTeacher({ ...teacher, usuario: event.target.value })} /><br />
                Contraseña <input type='password' id='pass' onChange={event => setTeacher({ ...teacher, password: event.target.value })} /><br />
                <input type='submit' id='enviar' />
            </form>
        </>
    );
}
