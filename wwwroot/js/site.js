const pasos = document.querySelectorAll(".pasosPerfil");
let pasoRecorrido = 0;

const respuestas = {
    alergias: "",
    intolerancias: "",
    enfermedades: "",
    cultura: "",
    estiloDeVida: "",
    dieta: ""
};

function mostrarPaso(i) {
    pasos.forEach((s, idx) => {
        s.style.display = idx === i ? "block" : "none";
    });
}

mostrarPaso(pasoRecorrido);

document.querySelectorAll(".continuar").forEach((boton) => {
    boton.addEventListener("click", () => {

        const pasoActual = pasos[pasoRecorrido];
        const checkboxes = pasoActual.querySelectorAll('input[type="checkbox"]');
        const mensajeError = pasoActual.querySelector(".error");
        const clave = pasoActual.getAttribute("name");

        let seleccionados = [];

        checkboxes.forEach(c => {
            if (c.checked) {
                seleccionados.push(c.value);
            }
        });

        if (seleccionados.length === 0) {
            mensajeError.innerHTML = "ERROR - Tenés que seleccionar al menos una opción para continuar";
            mensajeError.style.display = "block";
            return;
        }

        mensajeError.style.display = "none";

        respuestas[clave] = seleccionados.join(", ");

        if (pasoRecorrido === pasos.length - 1) {
            enviarFormulario();
            return;
        }

        pasoRecorrido++;
        mostrarPaso(pasoRecorrido);
    });
});

document.querySelectorAll(".atras").forEach(boton => {
    boton.addEventListener("click", () => {
        pasoRecorrido--;
        mostrarPaso(pasoRecorrido);
    });
});

function enviarFormulario() {
    document.getElementById("alergiasInput").value = respuestas.alergias;
    document.getElementById("intoleranciasInput").value = respuestas.intolerancias;
    document.getElementById("enfermedadesInput").value = respuestas.enfermedades;
    document.getElementById("culturaInput").value = respuestas.cultura;
    document.getElementById("estiloDeVidaInput").value = respuestas.estiloDeVida;
    document.getElementById("dietaInput").value = respuestas.dieta;

    document.getElementById("formPerfil").submit();
}