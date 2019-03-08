var wasmHelper = {};

wasmHelper.ACCESS_TOKEN_KEY = "__access_token__";

wasmHelper.saveAccessToken = function (tokenStr) {
    localStorage.setItem(wasmHelper.ACCESS_TOKEN_KEY, tokenStr);
};

wasmHelper.getAccessToken = function () {
    return localStorage.getItem(wasmHelper.ACCESS_TOKEN_KEY);
};

wasmHelper.removeAccessToken = function () {
    return localStorage.removeItem(wasmHelper.ACCESS_TOKEN_KEY);
};