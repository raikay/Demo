var conn = null;

setupConn = () => {
    conn = new signalR.HubConnectionBuilder()
        .withUrl("/hub")
        .build();

    conn.on("AddMsg", (obj) => {
        $('#msgPanel').append(`<p>${obj}</p>`);
    });

    conn.on("Finished", () => {
        conn.stop();
        $('#msgPanel').text('log out!');
    });

    conn.on("Self", (obj) => {
        $('#userId').text(obj);
    });

    conn.start()
        .catch(err => console.log(err));
}

setupConn();

function send() {
    $.post('./home/send', { msg: $('#msgBox').val(), id: $('#userId').text() }, (res) => {
        $('#msgBox').val('');
    });
}