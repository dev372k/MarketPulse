// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function html(strings, ...values) {
    const htmlString = strings.reduce((result, string, i) => {
        const value = values[i] || '';
        return result + string + value;
    }, '');
    return new DOMParser().parseFromString(htmlString, 'text/html').body.firstChild;
}


async function request(url, method, data = null) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    if (data) {
        options.body = JSON.stringify(data);
    }

    try {
        const response = await fetch(url, options);
        const responseData = await response.json();

        if (!response.ok) {
            throw new Error(responseData.message || 'Something went wrong');
        }

        return responseData;
    } catch (error) {
        throw new Error(error.message || 'Something went wrong');
    }
}

$.LoadingOverlaySetup({
    background: "rgba(0, 0, 0, 0.5)",
    image: '<svg width="120" height="30" xmlns="http://www.w3.org/2000/svg" fill="#000" viewBox="0 0 120 30"><circle cx="15" cy="15" r="15"><animate attributeName="r" from="15" to="15" begin="0s" dur="0.8s" values="15;9;15" calcMode="linear" repeatCount="indefinite"/><animate attributeName="fill-opacity" from="1" to="1" begin="0s" dur="0.8s" values="1;.5;1" calcMode="linear" repeatCount="indefinite"/></circle><circle cx="60" cy="15" r="9" attributeName="fill-opacity" from="1" to="0.3"><animate attributeName="r" from="9" to="9" begin="0s" dur="0.8s" values="9;15;9" calcMode="linear" repeatCount="indefinite"/><animate attributeName="fill-opacity" from="0.5" to="1" begin="0s" dur="0.8s" values=".5;1;.5" calcMode="linear" repeatCount="indefinite"/></circle><circle cx="105" cy="15" r="15"><animate attributeName="r" from="15" to="15" begin="0s" dur="0.8s" values="15;9;15" calcMode="linear" repeatCount="indefinite"/><animate attributeName="fill-opacity" from="1" to="1" begin="0s" dur="0.8s" values="1;.5;1" calcMode="linear" repeatCount="indefinite"/></circle></svg>',
    imageAnimation: "1.5s fadein",
    imageColor: "#800020"
});

//$(document).ajaxStart(function () {
//    $.LoadingOverlay("show");
//});
//$(document).ajaxStop(function () {
//    $.LoadingOverlay("hide");
//});

const loading = (type) => {
    $.LoadingOverlay(type);
}