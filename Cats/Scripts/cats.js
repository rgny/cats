var grid, dialog, dialogDelete;

function Edit(e) {

    $('#Id').val(e.data.id);
    $('#Name').val(e.data.record.Name);
    $('#Breed').val(e.data.record.Breed);
    $('#Age').val(e.data.record.Age);
    $('#message').html('');

    dialog.open('Edit Cat');
    $('#btnUpdate').show();
    $('#btnSave').hide();
}

function ConfirmDelete(e) {

    $('#IdDelete').val(e.data.id);
    $('#NameDelete').html(e.data.record.Name);
    $('#messageDelete').html('');
    dialogDelete.open('Confirmation');
}

function validate() {
    if ($('#Name').val() == "" || $('#Breed').val() == "" || $('#Age').val() == "") {
        $('#message').html("Please enter all the information");
        return false;
    }
    return true;
}

function Save() {

    if (!validate()) return;

    var record = {
        Name: $('#Name').val(),
        Breed: $('#Breed').val(),
        Age: $('#Age').val()
    };
    $.ajax({ url: 'api/Cats', data: record, type: 'POST' })
        .done(function () {
            dialog.close();
            grid.reload();
        })
        .fail(function (error) {
            $('#message').html(error.responseJSON.Message);
        });
}

function Update() {

    if (!validate()) return;

    var record = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        Breed: $('#Breed').val(),
        Age: $('#Age').val()
    };
    $.ajax({ url: 'api/Cats', data: record, type: 'PUT' })
        .done(function () {
            dialog.close();
            grid.reload();
        })
        .fail(function (error) {
            $('#message').html(error.responseJSON.Message);
        });
}

function Delete() {

    var Id = $('#IdDelete').val();

    $.ajax({ url: 'api/Cats/' + Id, type: 'DELETE' })
        .done(function () {
            dialogDelete.close();
            grid.reload();
        })
        .fail(function (error) {
            $('#messageDelete').html(error.responseJSON.Message);
        });

}
$(document).ready(function () {
    grid = $('#grid').grid({
        primaryKey: 'Id',
        dataSource: 'api/Cats', method: 'GET',
        columns: [
            { field: 'Id', width: 56 },
            { field: 'Name', width: 100 },
            { field: 'Breed', width: 50 },
            { field: 'Age', width: 20 },
            { tmpl: '<span class="gj-button-md" title="Click to Modify">Edit</span>', align: 'center', events: { 'click': Edit }, width: 20 },
            { tmpl: '<span class="gj-button-md" title="Click to Delete">Delete</span>', align: 'center', events: { 'click': ConfirmDelete }, width: 20 }
        ]
    });

    dialog = $('#dialog').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        width: 400
    });

    dialogDelete = $('#dialogDelete').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        width: 400
    });

    $('#btnAdd').on('click', function () {
        $('#Id').val('');
        $('#Name').val('');
        $('#Breed').val('');
        $('#Age').val('');
        $('#message').html('');
        dialog.open('Add Cat');
        $('#btnUpdate').hide();
        $('#btnSave').show();

    });

    $('#btnSave').on('click', Save);
    $('#btnUpdate').on('click', Update);
    $('#btnDelete').on('click', Delete);

    $('#btnCancel').on('click', function () {
        dialog.close();
    });
    $('#btnCancelDelete').on('click', function () {
        dialogDelete.close();
    });

});