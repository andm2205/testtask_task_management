jQueryAjaxGetList = (actionUrl) => {
    try {
        $.ajax({
            type: 'GET',
            url: actionUrl,
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

jQueryAjaxActionOnTask = (form, actionUrl) => {
    try {
        $.ajax({
            type: 'POST',
            url: actionUrl,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function () {
                alert("Успешно")
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

jQueryAjaxOpenFormWithForm = form => {
    try {
        $.ajax({
            type: 'POST',
            data: new FormData(form),
            url: form.action,
            contentType: false,
            processData: false,
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

jQueryAjaxPrintTaskTree = form => {
    try {
        $.ajax({
            type: 'POST',
            data: new FormData(form),
            url: '/Home/TaskTree',
            contentType: false,
            processData: false,
            success: function (res) {
                $('#task-tree').html(res.html)
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