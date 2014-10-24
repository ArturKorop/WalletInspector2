function onSuccess() {
    $(".InputNameClass").val('')
    $(".InputValueClass").val('')
    $(".InputTagClass").val('')

    $(".updateButton").button({
        icons: {
            primary: "ui-icon-refresh"
        },
        text: false
    });

    $(".removeButton").button({
        icons: {
            primary: "ui-icon-minusthick"
        },
        text: false
    });
}

function UpdateButtons() {
    $(".addButton").button({
        icons: {
            primary: "ui-icon-plusthick"
        },
        text: false
    });
    $(".updateButton").button({
        icons: {
            primary: "ui-icon-refresh"
        },
        text: false
    });
    $(".removeButton").button({
        icons: {
            primary: "ui-icon-minusthick"
        },
        text: false
    });
}

function UpdatePrevNextButtons(){
    $("#ButtonNext").button({
        icons: {
            primary: "ui-icon-circle-triangle-e"
        },
        text: false
    });
    $("#ButtonPrev").button({
        icons: {
            primary: "ui-icon-circle-triangle-w"
        },
        text: false
    });
}