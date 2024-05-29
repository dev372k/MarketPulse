function html(strings, ...values) {
    const htmlString = strings.reduce((result, string, i) => {
        const value = values[i] || '';
        return result + string + value;
    }, '');
    return new DOMParser().parseFromString(htmlString, 'text/html').body.firstChild;
}


const cache = {};

async function request(url, method, data = null, iscache = false) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    if (data) {
        options.body = JSON.stringify(data);
    }

    if (iscache && method.toUpperCase() === 'GET') {
        if (cache[url]) {
            return cache[url];
        }
    }

    try {
        const response = await fetch(url, options);
        const responseData = await response.json();

        if (!response.ok) {
            throw new Error(responseData.message || 'Something went wrong');
        }

        // Cache the response if iscache is true
        if (iscache && method.toUpperCase() === 'GET') {
            cache[url] = responseData;
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
    imageColor: "var(--main)"
});

//$(document).ready(function () {
//    loading('show');
//});

//$(window).on('load', function () {
//    setTimeout(() => {
//        loading('hide');
//    }, 1000)
//});

const loading = (type) => {
    $.LoadingOverlay(type);
}
