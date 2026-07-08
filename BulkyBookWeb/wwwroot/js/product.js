$(document).ready(function () {
    $('#tblData').DataTable({
        ajax: 'product/getall',
        columns: [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'price' },
            { data: 'author' },
            { data: null, defaultContent: '' },
            { data: null, defaultContent: '' }
        ]
    });
});