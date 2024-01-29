document.querySelector("#formularioRegistro").addEventListener('submit', validarDatos);

function validarDatos(evt) {
    evt.preventDefault();

    let nombre = document.querySelector("#nombre").value;
    let apellido = document.querySelector("#nombre").value;
    let fechanac = document.querySelector("#fechanac").value;
    let email = document.querySelector("#email").value;
    let pass = document.querySelector("#pass").value;


    if (nombre != "" && apellido !== "" fechanac != "" && email !== "" && pass != "") {
        this.submit();
    } else {
        document.querySelector("#pRes").innerHTML = "Verifique los datos, no pueden estar vacios";
    }
}

