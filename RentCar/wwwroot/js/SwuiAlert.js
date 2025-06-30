function confirmarBorradoCliente(button) {
    Swal.fire({
        title: '¿Estás seguro de borrar este cliente?',
        text: "Esta acción no se puede deshacer.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, borrar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            
            const form = button.closest('form');
            if (form) {
                form.submit();
            } else {
                console.error('No se encontró el formulario para enviar.');
            }
        }
    });
}

function confirmarBorradoVehiculo(button) {
    Swal.fire({
        title: '¿Estás seguro de borrar este Vehiculo?',
        text: "Esta acción no se puede deshacer.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, borrar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            const form = button.closest('form');
            if (form) {
                form.submit();
            } else {
                console.error('No se encontró el formulario para enviar.');
            }
        }
    });
}

