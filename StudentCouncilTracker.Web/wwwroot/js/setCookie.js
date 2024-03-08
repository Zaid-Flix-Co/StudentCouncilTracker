window.setCookie = (name, value, minutes) => {
    const date = new Date();
    date.setTime(date.getTime() + (minutes * 60 * 1000));
    const expires = "expires=" + date.toUTCString();
    document.cookie = name + "=" + value + ";" + expires + ";path=/";
}

window.getCookie = function (key) {
    var value = "";
    document.cookie.split(';').forEach(function (cookie) {
        var parts = cookie.split('=');
        if (parts.length === 2 && parts[0].trim() === key) {
            value = parts[1].trim();
        }
    });
    return value;
};

window.deleteCookie = function (key) {
    document.cookie = key + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
};