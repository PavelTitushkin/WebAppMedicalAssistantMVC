let divEmailUser = document.getElementById('account-email');

const getUserEmailUrl = `${window.location.origin}/Account/GetUserEmail`;

fetch(getUserEmailUrl)
    .then(function (response) {
        return response.text();
    }).then(function (result) {
        divEmailUser.innerHTML = result;
    }).catch(function () {
        console.error('smth goes wrong');
    });
