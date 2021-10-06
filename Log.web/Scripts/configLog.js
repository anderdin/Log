var host_api = 'https://localhost:44376/api/';

function verificaSessao() {
    if (localStorage.getItem("token") == null) {
        window.location.replace('/Login');
    } else {
        $.post(host_api + 'Login/VerificaSessao?prToken=' + localStorage.getItem("token"), {}, function (res) {
            if (res.Sucesso) {
                if (!res.Dados.sessaoValida) {
                    window.location.replace('/Login');
                } else {
                    window.location.replace('/Dashboard');
                }
            }
        });
    }
}