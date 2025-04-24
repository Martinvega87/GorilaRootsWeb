import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import * as API from "./Services/data"
import { Header } from "./Header";


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
                    alert("calificacion añadida con exito")
                    document.getElementById("descripcion").value="";
                    document.getElementById("nota").value="";
                    document.getElementById("porcentaje").value="";
                }
                else{
                    alert("ERROR");
                    }
            })
        }


    }

 return(
    <>
    <Header/>
       <table>
            <thead>
                <tr>
                    <th>Descripcion</th>
                    <th>Nota</th>
                    <th>Poderacion</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {
                    calificaciones?.map(calificacion =>
                    (
                        <tr>
                            <td>{calificacion.descripcion}</td>
                            <td>{calificacion.nota}</td>
                            <td>{calificacion.porcentaje}%</td>
                            <td>Eliminar</td>

                        </tr>
                    ))  
                    
                }
                <tr>
                    <td><input type= 'text' id='descripcion' placeholder='descripcion' onChange={event =>setCalificacion({...calificacion,descripcion:event.target.value})}></input></td>
                    <td><input type= 'number' id='nota' placeholder='nota'onChange={event =>setCalificacion({...calificacion,nota:event.target.value})}></input></td>
                    <td><input type= 'number' id='porcentaje' placeholder='ponderacion'onChange={event =>setCalificacion({...calificacion,porcentaje:event.target.value})}></input></td>
                    <td><button id='nueva' onClick={()=>createCalificacion()}>Nueva</button></td>
                </tr>
            </tbody>
       </table>
       <p>Nota Final: {total}</p>
    </>

 )

}