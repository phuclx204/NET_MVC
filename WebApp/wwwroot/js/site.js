$.ajaxSetup({
    beforeSend: function (xhr) {
        const token = localStorage.getItem("jwtToken");
        if (token) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }
});
