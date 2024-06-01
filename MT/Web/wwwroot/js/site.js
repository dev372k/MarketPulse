
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