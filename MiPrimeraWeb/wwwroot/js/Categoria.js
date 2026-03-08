(() => {
    const Categoria = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblCategoria').DataTable({
                ajax: {
                    url: '/Categoria/ObtenerCategorias',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'id' },
                    { data: 'nombre' },
                    { data: 'descripcion' },
                    {
                        data: 'activo',
                        render: function (data) {
                            return data === 1 ? 'Sí' : 'No';
                        }
                    },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.id}">
                                    <i class="bi bi-pencil"></i> Editar
                                </button>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.id}">
                                    <i class="bi bi-trash"></i> Eliminar
                                </button>`;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },

        registrarEventos() {
            $('#tblCategoria').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Categoria.cargarDatosCategoria(id);
            });

            $('#tblCategoria').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Categoria.eliminarCategoria(id);
            });

            $('#btnGuardarCategoria').on('click', function () {
                Categoria.guardarCategoria();
            });

            $('#btnEditarCategoria').on('click', function () {
                Categoria.editarCategoria();
            });
        },

        guardarCategoria() {
            const form = $('#formCrearCategoria');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearCategoria').modal('hide');
                        form[0].reset();
                        Categoria.tabla.ajax.reload();
                        Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                    }
                },
                error: function (error) {
                    Swal.fire({
                        title: 'Error',
                        text: error.responseJSON?.description || 'Error en la solicitud',
                        icon: 'error'
                    });
                }
            });
        },

        cargarDatosCategoria(id) {
            $.get(`/Categoria/ObtenerCategoriaPorId?id=${id}`, function (result) {
                if (result.esCorrecto) {
                    const data = result.data;
                    $('#CategoriaId').val(data.id);
                    $('#NombreEdit').val(data.nombre);
                    $('#DescripcionEdit').val(data.descripcion || '');
                    $('#ActivoEdit').val(data.activo);
                    $('#modalEditarCategoria').modal('show');
                }
            });
        },

        editarCategoria() {
            const form = $('#formEditarCategoria');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalEditarCategoria').modal('hide');
                        form[0].reset();
                        Categoria.tabla.ajax.reload();
                        Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                    }
                },
                error: function (error) {
                    Swal.fire({
                        title: 'Error',
                        text: error.responseJSON?.description || 'Error en la solicitud',
                        icon: 'error'
                    });
                }
            });
        },

        eliminarCategoria(id) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: '¡No podrás revertir esta operación!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Categoria/EliminarCategoria',
                        type: 'POST',
                        data: { id: id },
                        success: function (response) {
                            if (response.esCorrecto) {
                                Categoria.tabla.ajax.reload();
                                Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                            } else {
                                Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                            }
                        },
                        error: function (error) {
                            Swal.fire({
                                title: 'Error',
                                text: error.responseJSON?.description || 'Error en la solicitud',
                                icon: 'error'
                            });
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Categoria.init());
})();
