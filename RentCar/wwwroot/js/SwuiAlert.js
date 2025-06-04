document.getElementById('Renta').addEventListener('submit', function (e) {
    e.preventDefault(); 

    Swal.fire({
        title: "Reservacion!",
        icon: "Completada",
        draggable: true
    }).then(() => {
        
        e.target.submit();
    });
});