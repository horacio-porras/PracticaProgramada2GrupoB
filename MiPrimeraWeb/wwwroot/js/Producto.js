(() => {
    const Producto = {
        tabla: null,
        categorias: [],

        init() {
            this.cargarCategorias().then(() => {
                this.llenarCombosCategoria();
                this.inicializarTabla();
                this.registrarEventos();
            });
        },

        cargarCategorias() {
            return $.get('/Producto/ObtenerCategorias').then((result) => {
                if (result.esCorrecto && result.data) {
                    Producto.categorias = result.data;
                }
            });
        },

        llenarCombosCategoria() {
            const opciones = Producto.categorias.map(c => `<option value="${c.id}">${c.nombre}</option>`).join('');
            $('#CategoriaId').html('<option value="">-- Seleccione categoría --</option>' + opciones);
            $('#CategoriaIdEdit').html('<option value="">-- Seleccione categoría --</option>' + opciones);
        },

        inicializarTabla() {
            this.tabla = $('#tblProducto').DataTable({
                ajax: {
                    url: '/Producto/ObtenerProductos',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'id' },
                    { data: 'nombre' },
                    { data: 'descripcion' },
                    {
                        data: 'precio',
                        render: function (data) {
                            return data != null ? parseFloat(data).toFixed(2) : '-';
                        }
                    },
                    { data: 'stock' },
                    { data: 'nombreCategoria' },
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
            $('#tblProducto').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Producto.cargarDatosProducto(id);
            });

            $('#tblProducto').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Producto.eliminarProducto(id);
            });

            $('#btnGuardarProducto').on('click', function () {
                Producto.guardarProducto();
            });

            $('#btnEditarProducto').on('click', function () {
                Producto.editarProducto();
            });
        },

        guardarProducto() {
            const form = $('#formCrearProducto');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearProducto').modal('hide');
                        form[0].reset();
                        Producto.tabla.ajax.reload();
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

        cargarDatosProducto(id) {
            $.get(`/Producto/ObtenerProductoPorId?id=${id}`, function (result) {
                if (result.esCorrecto) {
                    const data = result.data;
                    $('#ProductoId').val(data.id);
                    $('#NombreEdit').val(data.nombre);
                    $('#DescripcionEdit').val(data.descripcion || '');
                    $('#PrecioEdit').val(data.precio);
                    $('#StockEdit').val(data.stock);
                    $('#CategoriaIdEdit').val(data.categoriaId);
                    $('#modalEditarProducto').modal('show');
                }
            });
        },

        editarProducto() {
            const form = $('#formEditarProducto');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalEditarProducto').modal('hide');
                        form[0].reset();
                        Producto.tabla.ajax.reload();
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

        eliminarProducto(id) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: '¡No podrás revertir esta operación!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Producto/EliminarProducto',
                        type: 'POST',
                        data: { id: id },
                        success: function (response) {
                            if (response.esCorrecto) {
                                Producto.tabla.ajax.reload();
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

    $(document).ready(() => Producto.init());
})();
