window.clipboardCopy = {
    copyText: function (id) {
        var text = document.getElementById(id).innerText;
        navigator.clipboard.writeText(text).then(function () {
            console.log("Copied to clipboard!");

        })
            .catch(function (error) {
                alert(error);
            });
    }
};