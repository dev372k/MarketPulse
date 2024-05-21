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
