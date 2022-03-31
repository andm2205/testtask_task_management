jQueryAjaxGetList = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#task_list_container').html(res.html)
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxOpenForm = (formUrl) => {
    try {
        $.ajax({
            type: 'GET',
            url: formUrl,
            success: function (res) {
                $('#task_desc_container').html(res.html)
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}