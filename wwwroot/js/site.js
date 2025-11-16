const pasos = document.querySelectorAll(".pasosPerfil");
let pasoRecorrido = 0;

function mostrarPaso(i) {
    pasos.forEach((s, idx) => {
        s.style.display = idx === i ? "block" : "none";
    });
}

mostrarPaso(pasoRecorrido);

document.querySelectorAll(".continuar").forEach(boton => {
    boton.addEventListener("click", () => {
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