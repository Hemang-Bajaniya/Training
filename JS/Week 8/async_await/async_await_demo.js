// async ensures that the function returns a promise, and wraps non-promises in it.
async function getData() {
    // heavy compu
    for (let index = 0; index < 1000000000; index++) {
    }

    //ui code
    console.log('drawing banner');

    return 1;
}

getData().then(console.log);
