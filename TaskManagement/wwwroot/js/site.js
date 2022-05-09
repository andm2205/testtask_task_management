GetList = (actionUrl) => {
    try {
        $.ajax({
            type: 'GET',
            url: actionUrl,
            success: function (res) {
                if (res.error === true) {
                    console.log(res.message)
                    alert(res.message)
                }
                else
                    $('#task_list_container').html(res.html)
            },
            error: function (err) {
                console.log(err)
                alert(err.message)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

PrintTaskTree = dataa => {
    try {
        $.ajax({
            type: 'POST',
            data: dataa,
            url: '/Home/TaskTree',
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.error === true) {
                    console.log(res.message)
                    alert(res.message)
                }
                else
                    $('#task-tree').html(res.html)
            },
            error: function (err) {
                console.log(err)
                alert(err.message)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

ActionOnTask = (form, actionUrl, type) => {
    try {
        $.ajax({
            type: type,
            url: actionUrl,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.error === true) {
                    console.log(res.message)
                    alert(res.message)
                }
                else
                    alert(res.message)
            },
            error: function (err) {
                console.log(err)
                alert(err.message)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

OpenForm = (formUrl) => {
    try {
        $.ajax({
            type: 'GET',
            url: formUrl,
            success: function (res) {
                if (res.error === true) {
                    console.log(res.message)
                    alert(res.message)
                }
                else
                    $('#task_desc_container').html(res.html)
            },
            error: function (err) {
                console.log(err)
                alert(err.message)
            }
        })
        PrintTaskTree(null)
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

OpenFormWithData = form => {
    try {
        $.ajax({
            type: 'POST',
            data: new FormData(form),
            url: form.action,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.error === true) {
                    console.log(res.message)
                    alert(res.message)
                }
                else
                    $('#task_desc_container').html(res.html)
            },
            error: function (err) {
                console.log(err)
                alert(err.message)
            }
        })
        PrintTaskTree(new FormData(form))
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

