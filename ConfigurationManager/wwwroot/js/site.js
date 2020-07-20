$(function () {

    SetValueInputType();

    $('a.delete').on('click', function () {

        SetDeleteModal($(this));

    });

    $('select#Type').on('change', function () {

        $('#Value').val('');
        SetValueInputType();

    });

    $('[name=BoolValue]').on('change', function () {

        $('#Value').val($(this).val());

    });

    if ($('.alert').length) {
        $('.alert').delay(3000).fadeOut();
    }

});

function SetDeleteModal(element) {

    var entityId = element.data('entity-id');

    $('#id').val(entityId);

}

function SetValueInputType() {

    var dataType = $('#Type').val();
    var valueInput = $('#Value');

    valueInput.datepicker('destroy');
    valueInput.unmask();
    valueInput.prop('readonly', false);
    valueInput.removeClass('hidden');

    $('.bool-radios').addClass('hidden');

    switch (dataType) {
        case 'Integer':
            valueInput.mask('0000000000');
            break;
        case 'Bool':
            valueInput.addClass('hidden');
            $('.bool-radios').removeClass('hidden');
            $('.bool-radio input[value=' + valueInput.val() + ']').prop('checked', 'checked');
            break;
        case 'DateTime':
            valueInput.datepicker({dateFormat: 'dd.mm.yy'});
            valueInput.prop('readonly', 'readonly');
            break;
        case 'Decimal':
            valueInput.mask('0ZZZZZZ.00', { placeholder: 'eg. 123.45', translation: { 'Z': { pattern: /[0-9]/, optional: true } } });
            break;
    }

}

